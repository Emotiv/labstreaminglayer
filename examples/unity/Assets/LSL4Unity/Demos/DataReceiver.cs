using UnityEngine;
using LSL;
using System.Collections;
using UnityEngine.UI;

// Don't forget the Namespace import
using Assets.LSL4Unity.Scripts.AbstractInlets;
using System.Collections.Generic;

public class DataReceiver :ADoubleInlet {

    /// <summary>
    /// number of channels
    /// </summary>
    public Text NumChans;

    /// <summary>
    /// unique id of sender
    /// </summary>
    public Text DeviceID;

    /// <summary>
    /// header of data
    /// </summary>
    public Text DataHeaderTxt;

    /// <summary>
    /// data stream
    /// </summary>
    public Text DataStreamTxt;

    /// <summary>
    /// list of lsl streams
    /// </summary>
    public Dropdown DropdownStreams;

    private string _currStreamName = "";
    private float _waitTime = 2.0f; // 2 seconds
    private float timer = 0.0f; // timer to query streams
    private List<string> listStreams = new List<string>() {};
    private  bool _inletCreated = false;

    void Start()
    {
        Debug.Log("DataReceiver: Start ");
        Init();
    }

    void Update()
    {
        if (_inletCreated == false) {
            // query streams each 2 seconds if have not connected streams
            timer += Time.deltaTime;
            if ( timer > _waitTime) {
                timer = timer - _waitTime;
                StartCoroutine(QueryStreams());
            }
        }
        
    }

    /// <summary>
    /// Query LSL Streams
    /// </summary>
    public IEnumerator QueryStreams() {
        DropdownStreams.ClearOptions();
        listStreams.Clear();
        foreach ( var stream in QueryAvailStreams()) {
            string streamName = stream.name();
            if (!listStreams.Contains(streamName))
                listStreams.Add(streamName);
        }
        // add to drop down
        DropdownStreams.AddOptions(listStreams);

        // check the current stream removed from list streams
        if (!string.IsNullOrEmpty(_currStreamName) && !listStreams.Contains(_currStreamName)) {
            // the current stream is not available
            Debug.Log(" The current stream is not available.");
            _currStreamName = "";
            // TODO: reset button
        }
        yield return null;
    }

    /// <summary>
    /// Handle when choose stream name
    /// </summary>
    public void Dropdown_IndexChanged(int index) 
    {
        if (listStreams.Count > 0) {
            _currStreamName = listStreams[index];
            foreach ( var stream in QueryAvailStreams()) {
                string name = stream.name();
                if (name == _currStreamName) {
                    DeviceID.text    = stream.source_id();
                    NumChans.text    = stream.channel_count().ToString();
                    return;
                }
            }
        }
            
    }

    /// <summary>
    /// Connect to a LSL streams
    /// </summary>
    public void OnConnectClick() {
        if (listStreams.Count == 0) {
            Debug.LogWarning(" No stream available.");
            return;
        }
        // selected streams
        string streamName = listStreams[DropdownStreams.value];
        // create a inlet
        liblsl.StreamInfo info = GetStreamInfo(streamName);
        if (!string.IsNullOrEmpty(info.name())) {
            _inletCreated = CreateInlet(info);
            
            if (_inletCreated) {
                Debug.Log("Create an inlet successfully for stream: " + streamName);
                DeviceID.text    = info.source_id();
                NumChans.text    = info.channel_count().ToString();
                DataHeaderTxt.text = string.Join("; ", GetChannelsList().ToArray());

                // disable dropdown
                DropdownStreams.enabled = false;
            }
            // TODO:  disable connect button

        }
        else
            Debug.Log(" Can not get stream information of stream " + streamName);
    }

    /// <summary>
    /// Handle disconnect button clicked
    /// </summary>
    public void OnDisconnectClick() 
    {
        if (_inletCreated) {
            CloseInlet();
            _inletCreated           = false;
            DataHeaderTxt.text      = "";
            DataStreamTxt.text      = "";
            _currStreamName         = "";
            DropdownStreams.enabled = true;

        }
    }

    /// <summary>
    /// Process data. Show data on UI
    /// </summary>
    protected override void Process(double[] newSample, double timeStamp)
    {
        // Show data on UI
        DataStreamTxt.text = string.Join("; ", newSample);
    }

    /// <summary>
    /// Get stream information from stream name
    /// </summary>
    private liblsl.StreamInfo GetStreamInfo(string streamName) {
        foreach ( var stream in QueryAvailStreams()) {
            string name = stream.name();
            if (name == streamName)
                return stream;
        }
        return new liblsl.StreamInfo("","");
    }


}