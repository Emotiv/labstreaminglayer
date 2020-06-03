# Emotiv Lab Streaming Layer Interface

Here are guidelines and some examples to an application work with Emotiv LSL.

## Prerequisites

1. [Download and install](https://www.emotiv.com/developer/) the EmotivApp and EmotivPro. The EmotivApp is for login process while EmotivPro for configuring LSL both Inlet and Outlet.

## How to configure LSL on EmotivPro

After headset connected, go to LSL settings menu on EmotivPro. There are 2 tabs Outlet and Inlet.
1. **Outlet:** Configure for data streams (EEG, Motions, Performance metrics) as LSL Outlet.
<p align="center">
  <img width="600" height="592" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/outlet-configuration_600x592.jpg">
</p>
**The Stream name:** Show the name of lsl stream. But the actual stream name will be added a suffix such as: "- EEG" for eeg data stream, " - Motion" for motion stream...
**Data stream:** There are 3 types of data streams: EEG, Motion, Performance metrics. Each choosen one will be created a lsl stream.
**Data format:** Currently, We only support 2 types: cf_float32 and cf_double64. Notes that, Openvibe only support cf_float32 and cf_int32.
**Transmit type:** We support both type sample and chunk. A chunk contains number of samples.

2. **Inlet:** To support add marker from a outside LSL Outlet to Emotiv data streams.

<p align="center">
  <img width="500" height="358" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/inlet-configuration_500x358.jpg">
</p>

We only support marker adding to Emotiv data stream. The marker event can have one channel or 3 channels for timing synchornization as below:
```
// MarkerValue is value of marker
std::vector<std::string> channels = {"MarkerValue"};

or for timing synchornization
// MarkerTime is the time you want to add a marker. It is an epoch time.
// MarkerValue is value of marker
// CurrentTime is current time at epoch time
std::vector<std::string> channels = { "MarkerTime","MarkerValue", "CurrentTime"};
```
After send marker to LSL, You can see the stream name in Inlet tab. Choose one then click to connect button. Afterthat, you can see the marker added to data stream as below
<p align="center">
  <img width="400" height="258" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/matlab-markerResult-Inlet_400x258.jpg">
</p>


## How to use

There are guidelines and examples work with Cortex data streams:

1.Guidelines for <a href="examples/matlab/readme.md">matlab</a>.

2.Guidelines for <a href="examples/openvibe/readme.md">openvibe</a>.

3.Guidelines for <a href="examples/cpp/readme.md">cpp</a> project.


## Reference
1. [labstreaminglayer github ](https://github.com/sccn/labstreaminglayer)

## Release Notes

See <a href="docs/ReleaseNotes.md">here</a>.



