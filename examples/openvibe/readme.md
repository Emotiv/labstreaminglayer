# Guideline for OpenViBE to work with EMOTIV LSL Interface

The following guide describes how OpenViBE works with EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Get and install OpenViBE from http://openvibe.inria.fr (latest version 2.2.0)

## How to receive data from EMOTIV LSL Outlet stream in OpenViBE

1. Go to **Lab Streaming Layer** page, **Outlet** tab in EmotivPRO, choose the desire **Data stream** type and **cf_float32**  for **Data format** (as OpenViBE only supports this format for the moment).
Click **Start** button to start streaming.

2. Open the OpenViBE Acquisition Server, select **LabStreamingLayer (LSL)** option in the **Driver** field. Click the **Driver Properties** button. In the **Device Configuration**, make sure **EmotivDataStream-xx/xx** is selected in the **Signal stream** field.
The **EmotivDataStream-EEG** stream is for EEG data, the **EmotivDataStream-Motion** stream is for motion data, etc.
<p align="center">
  <img width="779" height="711" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-config.png">
</p>

3. Connect to the LSL stream and then hit **Play** to start receiving data:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-play-receivedata.png">
</p>

4. Open the OpenViBE designer, then open the file [LSLViewer.mxs](./LSLViewer.mxs). You can see the program connect an "Acquisition client" block to a "Signal display".
Click on the "Acquisition client" block to change the settings, and make sure the **Acquisition server port** is matching with the connection port on the Acquisition server.
Hit **Apply** to save and close the dialog.
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-config-designer.png">
</p>

5. New hit **Play** button to display data streams:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-data-graph.png">
</p>


**Notes:** Currently, OpenViBE only supports cf_float32 and cf_int32. So the data will be casted to cf_float32. Almost data still is same after casting but "Timestamp. Because the Timestamp is large number, lost precision after casting.


## How to send marker from OpenViBE to EMOTIV LSL Inlet

> Coming soon...

## Reference

* [OpenViBE and LabStreamingLayer](http://openvibe.inria.fr/how-to-use-labstreaminglayer-in-openvibe/)

* [OpenViBE-LSL Connection Quick-Guide](https://bitalino.com/docs/quick_guide_OpenVIBE.pdf)

