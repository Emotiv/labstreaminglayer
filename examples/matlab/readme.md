# Guideline for Matlab work with Emotiv LSL Interface

The  following quick-guide provides a brief description how Matlab work with Emotiv LSL Interface.

## Prerequisites
* Login via EmotivApp and connect a headset on EmotivPro.
* Get the latest version of submodule [liblsl-Matlab](https://github.com/labstreaminglayer/liblsl-Matlab.git) 
```
       git submodule update --init
```
* Recommend use Matlab version 2017a or later.


## How Matlab receive data from Emotiv LSL Outlet stream.
Please follow below guide step by step:

1. Go to Lab streaming layer setting - Outlet on EmotivPro, choose data stream type and data format.
Click start button to start LSL Outlet data stream.
2. Open [vis_stream.m](./vis_stream.m) on Matlab. You might need to add liblsl-Matlab path to your MATLAB path recursively. Use addpath(genpath('path/to/liblsl-Matlab')).
3. Run vis_stream. In dialog box, change sampleling rate for display to sample rate of corresponding data as below:
<p align="center">
  <img width="400" height="307" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-configure-eegOutlet_400x307.jpg">
</p>
4. The data stream will display as below:
<p align="center">
  <img width="400" height="362" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-display-eegOutlet_400x362.jpg">
</p>


## How to send marker from Matlab to Emotiv LSL Inlet
1. Open [sendmarker.m](./sendmarker.m) on Matlab. You might need to add liblsl-Matlab path to your MATLAB path recursively. Use addpath(genpath('path/to/liblsl-Matlab')).
The program will create a marker event each second. The marker event is vector contain MarkerTime, MarkerValue and CurrentTime. The MarkerTime and CurrentTime are epoch time at double format.
The MarkerTime is time of marker. The MarkerValue is value of marker. The CurrentTime is current epoch time. We need both time for time synchornization between machines.
2. Run sendmarker.
3. Go to Lab streaming layer setting Inlet on EmotivPro, choose MatlabMarker in stream name. And click Connect button.
<p align="center">
  <img width="400" height="362" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-marker-Inlet_400x317.jpg">
</p>
4. Markers will be added to data stream as below:
<p align="center">
  <img width="400" height="258" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-markerResult-Inlet_400x258.jpg">
</p>

## Reference
1. [Matlab and LabStreamingLayer](https://github.com/labstreaminglayer/liblsl-Matlab/)

