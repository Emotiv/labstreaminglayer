# Guideline for Unity to work with EMOTIV LSL Interface

The following guide describes how Unity works with EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Install Unity (recommend version 2018.4.22f1 or later)

## How to receive data from EMOTIV LSL Outlet stream in Unity

1. Go to **Lab Streaming Layer** page, **Outlet** tab in EmotivPRO, choose the desire **Data stream** type and **Data format**.
Click the **Start** button to start streaming.

2. Open DataReceiver.unity in Demos folder (`.\Assets\LSL4Unity\Demos\`).

3. Build a standalone application or run on Editor directly. Choose a stream name then hit **Connect** button to connect to the selected LSL stream. The header and data will be displayed like this:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/unity_data_receiver.png">
</p>

4. Hit **Disconnect** to disconnect to the stream.

## How to send marker from Unity to EMOTIV LSL Inlet

1. Open MarkerSender.unity in Demos folder (`.\Assets\LSL4Unity\Demos\`). The demo will create markers event every one second.
  * Each marker event contains 3 channels `MarkerTime`, `MarkerValue` and `CurrentTime`.
  * The `MarkerTime` and `CurrentTime` are epoch time in double format - we need both for the time synchornization between machines.
  * The `MarkerTime` is time of marker event. The `MarkerValue` is value of marker. `The CurrentTime` is current epoch time of processing.

2. Build a standalone application or run on Editor directly. Then hit **Start** button to start sending markers to LSL network.
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/unity-send-marker.png">
</p>

3. Go to **Lab Streaming Layer** page, **Inlet** tab on EmotivPRO, choose **Unity_LSL** in stream name. Then click the **Connect** button.

4. Markers will be added to data stream as the vertical red lines:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/marker-added.png">
</p>

## Reference

* [LSL for Unity](https://github.com/labstreaminglayer/LSL4Unity/wiki)

