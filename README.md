# Emotiv Lab Streaming Layer Interface

Here are guidelines and some examples to use LSL with EMOTIV Brainwear&reg;.

## Prerequisites

* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO.
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/

## How to configure LSL in EmotivPRO

After connecting your EMOTIV Brainwear&reg; headset, go to LSL settings menu in EmotivPRO. There are separate tabs for LSL Outlet and Inlet functionality.
### Outlet
Configure for data streams (EEG, Motions, Performance Metrics) as LSL Outlet.
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/config-outlet.png">
</p>

* **Stream name:** Show the name of LSL stream. The actual stream name on the other side (Inlet) will be *EmotivDataStream-EEG*, *EmotivDataStream-Motion* or *EmotivDataStream-Performance-Metrics*, depending on the type of data stream.

* **Data stream:** There are 3 types of data streams: EEG, Motion, Performance Metrics. Each one will create an individual LSL stream.

* **Data format:** Currently, 2 types are supported: *cf_float32* or *cf_double64*.

* **Transmit type:** We support both *Sample* and *Chunk*. A chunk contains certain number of samples, depending on the *Chunk size*.

### Inlet
To support sending data from a LSL Outlet to Emotiv data streams. 

<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/config-inlet.png">
</p>

Currently we support sending markers to the Inlet with 2 options:

1. A simple marker value - Double / integer type is expected but EmotivPRO will extract only the integer part before adding into data stream.
  * Expected format of data: `{"MarkerValue"}`

2. Marker with time for timing synchronization - The marker event is a vector with 3 elements:
  * `MarkerTime` is the epoch time of the event in double type.
  * `MarkerValue` is value of marker. Double / integer type is expected but EmotivPRO will extract only the integer part before adding into data stream.
  * `CurrentTime` is current epoch time (double type) when the marker is being pushed to the Inlet. It is usually later than the `MarkerTime`.
  * Expected format of data: `{"MarkerTime", "MarkerValue", "CurrentTime"}`


After sending marker via LSL, You can see the stream name in the Inlet page. Choose one then click the Connect button. After that, you will see the marker being added to the data stream as below:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/marker-added.png">
</p>


## How to use

There are some guidelines and examples on 3rd party applications:

* Guidelines for <a href="examples/matlab/readme.md">MATLAB</a>.

* Guidelines for <a href="examples/openvibe/readme.md">OpenViBE</a>.

* Guidelines for <a href="examples/cpp/readme.md">C++</a> project.

* Guidelines for <a href="examples/psychopy/readme.md">PsychoPy</a> project.

* Guidelines for <a href="examples/python/readme.md">Python</a> project.



## Reference
* [LSL on Github](https://github.com/sccn/labstreaminglayer)

* [LSL Documentation](https://labstreaminglayer.readthedocs.io/)

## Release Notes

See <a href="docs/ReleaseNotes.md">here</a>.



