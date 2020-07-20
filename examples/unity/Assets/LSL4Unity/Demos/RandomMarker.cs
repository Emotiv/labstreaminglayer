using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// Don't forget the Namespace import
using Assets.LSL4Unity.Scripts;

public class RandomMarker : LSLMarkerStream {
    public Button btnStart;
    public Button btnStop;
    public Text markerValue;
    public Text timeStamp;
    public Text streamName;
    public Text chanCount;

    // keep a copy of the executing script
    private IEnumerator coroutine;
    private bool isMarkerSending = false; // detect markers are been sending to LSL
    
    void Start () {
        Debug.Log("RandomMarker: Start ");
        Button butStart = btnStart.GetComponent<Button>();
        Button butStop 	= btnStop.GetComponent<Button>();
        butStart.onClick.AddListener(OnbtnStartClicked);
        butStop.onClick.AddListener(OnbtnStopClicked);

        string streamNameStr 	= "";
        string chanCountStr 	= "";
        streamNameStr  		+= lslStreamName;
        chanCountStr    	+= lslChannelCount.ToString();
        streamName.text    	= streamNameStr;
        chanCount.text      = chanCountStr;
    }

    /// <summary>
    /// Handle start button clicked. Start sending markers
    /// </summary>
    public void OnbtnStartClicked() {
        if (!isMarkerSending) {
            isMarkerSending = true;
            coroutine = WriteContinouslyMarkerEachSecond();
            StartCoroutine(coroutine);
        }
    }

    /// <summary>
    /// Handle stop button clicked. Stop sending markers to LSL
    /// </summary>
    public void OnbtnStopClicked() {
        Debug.Log ("You have clicked the button stop!");
        isMarkerSending = false;
        markerValue.text    = "";
        timeStamp.text      = "";
        StopCoroutine(coroutine);
    }
    
    /// <summary>
    /// Write marker each second continously.
    /// </summary>
    IEnumerator WriteContinouslyMarkerEachSecond()
    {
        while (isMarkerSending)
        {
            // an example for demonstrating the usage of marker stream
            // current time
            int currentMarker = UnityEngine.Random.Range(1, 100);
            long epochNow = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            string markerValStr     = "";
            string timestampStr     = "";
            markerValStr            += currentMarker.ToString();
            timestampStr            += epochNow.ToString();
            markerValue.text     = markerValStr;
            timeStamp.text       = timestampStr;
            UnityEngine.Debug.Log(" send marker has value " + currentMarker.ToString());
            Write(currentMarker, epochNow);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
