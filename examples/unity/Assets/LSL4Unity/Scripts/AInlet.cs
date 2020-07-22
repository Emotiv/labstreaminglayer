using UnityEngine;
using System.Collections;
using LSL;
using System;
using System.Collections.Generic;

/// <summary>
/// DO NOT CHANGE CLASSES WITHIN THESE NAMESPACE
/// 
/// These namespace provides basic implementations to quick start with simple stream inlets.
/// 
/// These implementation supporting just the simplest use case.
/// Getting all samples available in at the moment of the update call (Update/FixedUpdate).
/// Samples won't get cached or queue.
/// </summary>
namespace Assets.LSL4Unity.Scripts.AbstractInlets
{
    public abstract class AFloatInlet : MonoBehaviour
    {
        public enum UpdateMoment { FixedUpdate, Update }

        public UpdateMoment moment;

        public string StreamName;

        public string StreamType;

        liblsl.StreamInfo[] results;
        liblsl.StreamInlet inlet;
        liblsl.ContinuousResolver resolver;

        private int expectedChannels = 0;

        float[] sample;
        
        void Start()
        {
            var expectedStreamHasAName = !StreamName.Equals("");
            var expectedStreamHasAType = !StreamType.Equals("");

            if (!expectedStreamHasAName && !expectedStreamHasAType)
            {
                Debug.LogError("Inlet has to specify a name or a type before it is able to lookup a stream.");
                this.enabled = false;
                return;
            }

            if (expectedStreamHasAName)
            {
                Debug.Log("Creating LSL resolver for stream " + StreamName);

                resolver = new liblsl.ContinuousResolver("name", StreamName);
            }
            else if (expectedStreamHasAType)
            {
                Debug.Log("Creating LSL resolver for stream with type " + StreamType);
                resolver = new liblsl.ContinuousResolver("type ", StreamType);
            }
            
            StartCoroutine(ResolveExpectedStream());

            AdditionalStart();
        }
        /// <summary>
        /// Override this method in the subclass to specify what should happen during Start().
        /// </summary>
        protected virtual void AdditionalStart() 
        {
            //By default, do nothing.
        }

        IEnumerator ResolveExpectedStream()
        {
            var results = resolver.results();

            yield return new WaitUntil(() => results.Length > 0);

            Debug.Log(string.Format("Resolving Stream: {0}", StreamName));

            inlet = new liblsl.StreamInlet(results[0]);

            expectedChannels = inlet.info().channel_count();
            
            yield return null;
        }

        protected void pullSamples()
        {
            sample = new float[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0) {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch(ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }

        /// <summary>
        /// Override this method in the subclass to specify what should happen when samples are available.
        /// </summary>
        /// <param name="newSample"></param>
        protected abstract void Process(float[] newSample, double timeStamp);

        void FixedUpdate()
        {
            if (moment == UpdateMoment.FixedUpdate && inlet != null)
                pullSamples();
        }

        void Update()
        {
            if (moment == UpdateMoment.Update && inlet != null)
                pullSamples();
        }
    }

    public abstract class ADoubleInlet : MonoBehaviour
    {
        public enum UpdateMoment { FixedUpdate, Update }

        public UpdateMoment moment;
        
        liblsl.StreamInlet inlet;

        /// <summary>
        /// A continuous_resolver that resolves all streams on the network
        /// </summary>
        liblsl.ContinuousResolver resolver;

        /// <summary>
        /// number of channels of current stream
        /// </summary>
        private int _channelNum = 0;

        private double[] sample;

        void Start()
        {
            Debug.Log("ADoubleInlet: start ");
        }

        /// <summary>
        /// Init a resolver
        /// </summary>
        protected void Init() {
            Debug.Log("ADoubleInlet: Init ");
            if (resolver == null)
                resolver = new liblsl.ContinuousResolver();
        }


        /// <summary>
        /// Query LSL Streams
        /// </summary>
        public liblsl.StreamInfo[] QueryAvailStreams() 
        {
            var listStreams = resolver.results();
            if (listStreams != null)
                return listStreams;
            else
                return new liblsl.StreamInfo[]{};
        }

        /// <summary>
        /// Create an inlet for stream
        /// </summary>
        protected bool CreateInlet(liblsl.StreamInfo streamInfo) {
            
            if (inlet != null)
            {
                Debug.Log("An inlet already was created for stream: " + inlet.info().name()); 
                return false;
            }

            inlet = new liblsl.StreamInlet(streamInfo);
            if (inlet != null) {
                _channelNum = inlet.info().channel_count();
                return true;
            }
            else
            {
                Debug.Log("Can not create an inlet for stream: " + streamInfo.name()); 
                return false;
            }
        }

        /// <summary>
        /// Get channel lists of current stream
        /// </summary>
        protected List<string> GetChannelsList()
        {
            List<string> channels = new List<string>();
            if (inlet != null) {
                liblsl.XMLElement chan = inlet.info().desc().child("channels").child("channel");
                for(int i = 0; i < inlet.info().channel_count(); i++) {
                    string chanName = chan.child_value("label");
                    channels.Add(chanName);
                    chan = chan.next_sibling();
                }
            }
            return channels;
        }

        /// <summary>
        /// Close the current Inlet
        /// </summary>
        protected void CloseInlet() 
        {
            if (inlet != null) {
                // close inlet
                Debug.Log(" Close the current Inlet");
                inlet.close_stream();
                inlet = null;
                _channelNum = 0;
            }
        }

        protected void pullSamples()
        {
            sample = new double[_channelNum];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }

        /// <summary>
        /// Override this method in the subclass to specify what should happen when samples are available.
        /// </summary>
        /// <param name="newSample"></param>
        protected abstract void Process(double[] newSample, double timeStamp);

        void FixedUpdate()
        {
            if (moment == UpdateMoment.FixedUpdate && inlet != null)
                pullSamples();
        }

        void Update()
        {
            if (moment == UpdateMoment.Update && inlet != null)
                pullSamples();
        }
    }
    
    public abstract class ACharInlet : MonoBehaviour
    {
        public enum UpdateMoment { FixedUpdate, Update }

        public UpdateMoment moment;

        public string StreamName;

        public string StreamType;

        liblsl.StreamInfo[] results;
        liblsl.StreamInlet inlet;
        liblsl.ContinuousResolver resolver;

        private int expectedChannels = 0;

        char[] sample;

        void Start()
        {
            var expectedStreamHasAName = !StreamName.Equals("");
            var expectedStreamHasAType = !StreamType.Equals("");

            if (!expectedStreamHasAName && !expectedStreamHasAType)
            {
                Debug.LogError("Inlet has to specify a name or a type before it is able to lookup a stream.");
                this.enabled = false;
                return;
            }

            if (expectedStreamHasAName)
            {
                Debug.Log("Creating LSL resolver for stream " + StreamName);

                resolver = new liblsl.ContinuousResolver("name", StreamName);
            }
            else if (expectedStreamHasAType)
            {
                Debug.Log("Creating LSL resolver for stream with type " + StreamType);
                resolver = new liblsl.ContinuousResolver("type", StreamType);
            }

            StartCoroutine(ResolveExpectedStream());

            AdditionalStart();
        }
        /// <summary>
        /// Override this method in the subclass to specify what should happen during Start().
        /// </summary>
        protected virtual void AdditionalStart() 
        {
            //By default, do nothing.
        }


        IEnumerator ResolveExpectedStream()
        {
            var results = resolver.results();

            yield return new WaitUntil(() => results.Length > 0);

            inlet = new liblsl.StreamInlet(results[0]);

            expectedChannels = inlet.info().channel_count();

            yield return null;
        }

        protected void pullSamples()
        {
            sample = new char[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }

        /// <summary>
        /// Override this method in the subclass to specify what should happen when samples are available.
        /// </summary>
        /// <param name="newSample"></param>
        protected abstract void Process(char[] newSample, double timeStamp);

        void FixedUpdate()
        {
            if (moment == UpdateMoment.FixedUpdate && inlet != null)
                pullSamples();
        }

        void Update()
        {
            if (moment == UpdateMoment.Update && inlet != null)
                pullSamples();
        }
    }
    
    public abstract class AShortInlet : MonoBehaviour
    {
        public enum UpdateMoment { FixedUpdate, Update }

        public UpdateMoment moment;

        public string StreamName;

        public string StreamType;

        liblsl.StreamInfo[] results;
        liblsl.StreamInlet inlet;
        liblsl.ContinuousResolver resolver;

        private int expectedChannels = 0;

        short[] sample;

        void Start()
        {
            var expectedStreamHasAName = !StreamName.Equals("");
            var expectedStreamHasAType = !StreamType.Equals("");

            if (!expectedStreamHasAName && !expectedStreamHasAType)
            {
                Debug.LogError("Inlet has to specify a name or a type before it is able to lookup a stream.");
                this.enabled = false;
                return;
            }

            if (expectedStreamHasAName)
            {
                Debug.Log("Creating LSL resolver for stream " + StreamName);

                resolver = new liblsl.ContinuousResolver("name", StreamName);
            }
            else if (expectedStreamHasAType)
            {
                Debug.Log("Creating LSL resolver for stream with type " + StreamType);
                resolver = new liblsl.ContinuousResolver("type", StreamType);
            }

            StartCoroutine(ResolveExpectedStream());

            AdditionalStart();
        }
        /// <summary>
        /// Override this method in the subclass to specify what should happen during Start().
        /// </summary>
        protected virtual void AdditionalStart() 
        {
            //By default, do nothing.
        }

        IEnumerator ResolveExpectedStream()
        {
            var results = resolver.results();

            yield return new WaitUntil(() => results.Length > 0);

            inlet = new liblsl.StreamInlet(results[0]);

            expectedChannels = inlet.info().channel_count();

            yield return null;
        }

        protected void pullSamples()
        {
            sample = new short[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }

        /// <summary>
        /// Override this method in the subclass to specify what should happen when samples are available.
        /// </summary>
        /// <param name="newSample"></param>
        protected abstract void Process(short[] newSample, double timeStamp);

        void FixedUpdate()
        {
            if (moment == UpdateMoment.FixedUpdate && inlet != null)
                pullSamples();
        }

        void Update()
        {
            if (moment == UpdateMoment.Update && inlet != null)
                pullSamples();
        }
    }
    
    public abstract class AIntInlet : MonoBehaviour
    {
        public enum UpdateMoment { FixedUpdate, Update }

        public UpdateMoment moment;

        public string StreamName;

        public string StreamType;

        liblsl.StreamInfo[] results;
        liblsl.StreamInlet inlet;
        liblsl.ContinuousResolver resolver;

        private int expectedChannels = 0;

        int[] sample;

        void Start()
        {
            var expectedStreamHasAName = !StreamName.Equals("");
            var expectedStreamHasAType = !StreamType.Equals("");

            if (!expectedStreamHasAName && !expectedStreamHasAType)
            {
                Debug.LogError("Inlet has to specify a name or a type before it is able to lookup a stream.");
                this.enabled = false;
                return;
            }

            if (expectedStreamHasAName)
            {
                Debug.Log("Creating LSL resolver for stream " + StreamName);

                resolver = new liblsl.ContinuousResolver("name", StreamName);
            }
            else if (expectedStreamHasAType)
            {
                Debug.Log("Creating LSL resolver for stream with type " + StreamType);
                resolver = new liblsl.ContinuousResolver("type", StreamType);
            }

            StartCoroutine(ResolveExpectedStream());

            AdditionalStart();
        }
        /// <summary>
        /// Override this method in the subclass to specify what should happen during Start().
        /// </summary>
        protected virtual void AdditionalStart() 
        {
            //By default, do nothing.
        }

        IEnumerator ResolveExpectedStream()
        {
            var results = resolver.results();

            yield return new WaitUntil(() => results.Length > 0);

            inlet = new liblsl.StreamInlet(results[0]);

            expectedChannels = inlet.info().channel_count();

            yield return null;
        }

        protected void pullSamples()
        {
            sample = new int[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }

        /// <summary>
        /// Override this method in the subclass to specify what should happen when samples are available.
        /// </summary>
        /// <param name="newSample"></param>
        protected abstract void Process(int[] newSample, double timeStamp);

        void FixedUpdate()
        {
            if (moment == UpdateMoment.FixedUpdate && inlet != null)
                pullSamples();
        }

        void Update()
        {
            if (moment == UpdateMoment.Update && inlet != null)
                pullSamples();
        }
    }
    
    public abstract class AStringInlet : MonoBehaviour
    {
        public enum UpdateMoment { FixedUpdate, Update }

        public UpdateMoment moment;

        public string StreamName;

        public string StreamType;

        liblsl.StreamInfo[] results;
        liblsl.StreamInlet inlet;
        liblsl.ContinuousResolver resolver;

        private int expectedChannels = 0;

        string[] sample;

        void Start()
        {
            var expectedStreamHasAName = !StreamName.Equals("");
            var expectedStreamHasAType = !StreamType.Equals("");

            if (!expectedStreamHasAName && !expectedStreamHasAType)
            {
                Debug.LogError("Inlet has to specify a name or a type before it is able to lookup a stream.");
                this.enabled = false;
                return;
            }

            if (expectedStreamHasAName)
            {
                Debug.Log("Creating LSL resolver for stream " + StreamName);

                resolver = new liblsl.ContinuousResolver("name", StreamName);
            }
            else if (expectedStreamHasAType)
            {
                Debug.Log("Creating LSL resolver for stream with type " + StreamType);
                resolver = new liblsl.ContinuousResolver("type", StreamType);
            }

            StartCoroutine(ResolveExpectedStream());

            AdditionalStart();
        }
        /// <summary>
        /// Override this method in the subclass to specify what should happen during Start().
        /// </summary>
        protected virtual void AdditionalStart() 
        {
            //By default, do nothing.
        }

        IEnumerator ResolveExpectedStream()
        {
            var results = resolver.results();

            yield return new WaitUntil(() => results.Length > 0);

            inlet = new liblsl.StreamInlet(results[0]);

            expectedChannels = inlet.info().channel_count();

            yield return null;
        }

        protected void pullSamples()
        {
            sample = new string[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }

        /// <summary>
        /// Override this method in the subclass to specify what should happen when samples are available.
        /// </summary>
        /// <param name="newSample"></param>
        protected abstract void Process(string[] newSample, double timeStamp);

        void FixedUpdate()
        {
            if (moment == UpdateMoment.FixedUpdate && inlet != null)
                pullSamples();
        }

        void Update()
        {
            if (moment == UpdateMoment.Update && inlet != null)
                pullSamples();
        }
    }
}