# Guideline for MATLAB to work with EMOTIV LSL Interface

The following guide describes how MATLAB works with EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Install MATLAB (recommend version 2017a or later)
* Get MATLAB LSL library from https://github.com/labstreaminglayer/liblsl-Matlab.git. Currently, we are using version at commit "62761b0". You also can get the library via submodule command as below
```
    git submodule update --init
```

## How to receive data from EMOTIV LSL Outlet stream in MATLAB

1. Go to **Lab Streaming Layer** page, **Outlet** tab in EmotivPRO, choose the desire **Data stream** type and **Data format**.
Click the **Start** button to start streaming.

2. Open [vis_stream.m](./vis_stream.m) in MATLAB.

3. Run vis_stream.m. In the pop-up dialog, change the sampling rate for display to match the rate of data from the Outlet:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-vistream-config.png">
</p>

4. Hit **Ok**. The data stream will be displayed like this:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-vistream-result.png">
</p>

**Note**: You also can retrieve data via RecieveData.m example.

## How to send marker from MATLAB to EMOTIV LSL Inlet

1. Open [sendmarker.m](./sendmarker.m) in MATLAB. The program will create a marker event every second.
  * The marker event is a vector containing `MarkerTime`, `MarkerValue` and `CurrentTime`.
  * The `MarkerTime` and `CurrentTime` are epoch time in double format - we need both for the time synchornization between machines.
  * The `MarkerTime` is time of marker event. The `MarkerValue` is value of marker. `The CurrentTime` is current epoch time of processing. 

2. Run sendmarker.m.

3. Go to **Lab Streaming Layer** page, **Inlet** tab on EmotivPRO, choose **MatlabMarker** in stream name. Then click the **Connect** button.
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-inlet-config.png">
</p>

4. Markers will be added to data stream as the vertical red lines:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/marker-added.png">
</p>

## Reference

* [MATLAB and LabStreamingLayer](https://github.com/labstreaminglayer/liblsl-Matlab/)

