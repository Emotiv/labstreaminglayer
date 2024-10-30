using UnityEngine;
using System.Collections;
using LSL;
using System;

namespace Assets.LSL4Unity.Scripts
{
    // [HelpURL("https://github.com/xfleckx/LSL4Unity/wiki#using-a-marker-stream")]
    public class LSLMarkerStream : MonoBehaviour
    {
        private const string unique_source_id = "UnityLSL123456";

        public string lslStreamName = "Unity_LSL";
        public string lslStreamType = "Markers";

        private liblsl.StreamInfo lslStreamInfo;
        private liblsl.StreamOutlet lslOutlet;
        public int lslChannelCount = 1;

        //Assuming that markers are never send in regular intervalls
        private double nominal_srate = liblsl.IRREGULAR_RATE;

        private const liblsl.channel_format_t lslChannelFormat = liblsl.channel_format_t.cf_double64;

        private double[] sample;
 
        void Awake()
        {
            UnityEngine.Debug.Log("LSLMarkerStream");

            sample = new double[lslChannelCount];

            lslStreamInfo = new liblsl.StreamInfo(
                                        lslStreamName,
                                        lslStreamType,
                                        lslChannelCount,
                                        nominal_srate,
                                        lslChannelFormat,
                                        unique_source_id);
            lslStreamInfo.desc().append_child_value("manufacturer", "UnityLSL");
            liblsl.XMLElement chns = lslStreamInfo.desc().append_child("channels");
            string[] channels = {"MarkerValue"};
            foreach(string chanName in channels) {
                chns.append_child("channel").append_child_value("label", chanName).append_child_value("type", "Marker");
            }

            lslOutlet = new liblsl.StreamOutlet(lslStreamInfo);
        }


        /// <summary>
        /// Push marker sample to LSL network
        /// </summary>
        public void Write(int value, long epochNow)
        {
            sample[0] = value;
            lslOutlet.push_sample(sample);
        }

        // public void Write(string marker, double customTimeStamp)
        // {
        //     sample[0] = marker;
        //     lslOutlet.push_sample(sample, customTimeStamp);
        // }

        // public void Write(string marker, float customTimeStamp)
        // {
        //     sample[0] = marker;
        //     lslOutlet.push_sample(sample, customTimeStamp);
        // }

        public void WriteBeforeFrameIsDisplayed(string marker)
        {
            UnityEngine.Debug.Log("WriteBeforeFrameIsDisplayed");
            StartCoroutine(WriteMarkerAfterImageIsRendered(marker));
        }

        IEnumerator WriteMarkerAfterImageIsRendered(string pendingMarker)
        {
            yield return new WaitForEndOfFrame();
            var currentMarker = UnityEngine.Random.Range(1, 100);
            long epochNow = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            Write(currentMarker, epochNow);

            yield return null;
        }

    }
}