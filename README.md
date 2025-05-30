# EmotivPRO's Lab Streaming Layer (LSL) Feature

## Overview

**Lab Streaming Layer (LSL)** is a robust protocol designed for the efficient and precise synchronized collection of time series measurements across diverse machines and applications.

With the **integrated LSL feature in EmotivPRO**, you can fully unlock the potential of your **EMOTIV Brainwear®** to meet the stringent demands of scientific research:

* **High-Precision EEG Data Synchronization:** LSL enables the collection and synchronization of raw EEG data streams from multiple devices and applications with sub-millisecond timing precision – a critical requirement for accurate scientific experiments.
* **Real-Time Raw EEG Data Processing:** EmotivPRO's LSL capability allows for real-time processing of raw EEG data within third-party applications, supporting immediate analysis and feedback during your experiments.
* **Efficient Two-Way Communication:** LSL facilitates seamless, two-way communication between EmotivPRO and other third-party software and devices, enabling complex and versatile research setups.
* **Precise Event Marker Synchronization:** Send distinct markers on different computing devices and synchronize these markers across all connected devices and applications, ensuring accurate event logging within your experimental data.

**EmotivPRO LSL** is an indispensable tool for optimizing your EEG data acquisition workflows, enhancing precision, and boosting interoperability in your brain science projects and brain-computer interface (BCI) research.

Explore our guidelines and examples to learn how to leverage LSL with your EMOTIV Brainwear® and EmotivPRO today!

## Prerequisites

* [Download and install](https://www.emotiv.com/developer/) the EMOTIV Launcher and EmotivPRO.
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/

## How to work with LSL Outlet

### Configuration
After connecting your EMOTIV Brainwear&reg; headset on EmotivPro, go to Settings > LSL > Outlet as below:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/config-outlet.png">
</p>

* **Stream name:** Set the stream name for transmission. The stream name consists of the prefix "EmotivDataStream-"  combined with data types such as: *EEG*, *Motion*, *Performance-Metrics*, *Contact-Quality*, *EEG-Quality* or *Band-Power*

* **Data stream:** Choose the data stream for transmission and select between supported EEG, Motion, Performance Metrics, Contact Quality, EEG Quality, Band Power Data. An individual LSL stream will be created for each one.

* **Data format:** Currently, 2 types are supported: *cf_float32* or *cf_double64*. But if you choose type *cf_float32*, the timestamp might not be correct as expected because the timestamp value is out of range(-16777216 to 16777216) which mentioned in [LSL doc](https://labstreaminglayer.readthedocs.io/projects/liblsl/ref/enums.html).

* **Transmit type:** Set the transmission type by selecting between *Sample* or *Chunk*. For Chunk, you can set the Chunk size to be 4/16/32/64/128/256.

### Data Output format
The data output format as below table:

Stream type | Data output format | Sample rate | Notes
----------- | ------------------ | ------------ | ---------------
EEG | {"Timestamp", "Counter", "Interpolate", <EEG sensors>, "HardwareMarker", "Markers" } | 128Hz/ 256 Hz | Description of  EEG channels at [link](https://emotiv.gitbook.io/cortex-api/data-subscription/data-sample-object#eeg). The Markers channel  is supported from V3.0 and it is marker value of marker object included on EEG data.
Motion | {"Timestamp", "Counter", "Interpolate", "Q0","Q1","Q2","Q3", "ACCX","ACCY","ACCZ", "MAGX","MAGY","MAGZ"} |32 Hz / 64 Hz / 128 Hz | Description of Motion channels at [link](https://emotiv.gitbook.io/cortex-api/data-subscription/data-sample-object#motion)
Performance-Metrics | {"Timestamp", "Engagement","Excitement","Focus","Interest ", "Relaxation","Stress"} | 2 Hz for high resolution / 0.1 Hz for low resolution | Description of Performance metric at [link](https://emotiv.gitbook.io/cortex-api/data-subscription/data-sample-object#performance-metric)
Contact-Quality | {"Timestamp", "BatteryPercent", "Overall", "Signal", <EEG sensors> } | 2 Hz | Description of Contact Quality at [link](https://emotiv.gitbook.io/cortex-api/data-subscription/data-sample-object#device-information) but the order of channels changes a bit.
EEG-Quality | {"Timestamp","BatteryPercent","Overall","SampleRateQuality", <EEG sensors>} | 2 Hz | Description of EEG Quality at [link](https://emotiv.gitbook.io/cortex-api/data-subscription/data-sample-object#eeg-quality)
Band-Power | {"Timestamp",<EEG sensors/Band>} | 8 Hz | Description of Band Power at [link](https://emotiv.gitbook.io/cortex-api/data-subscription/data-sample-object#band-power)


## How to work with LSL Inlet

### Configuration
After connecting your EMOTIV Brainwear&reg; headset on EmotivPro, go to Settings > LSL > Inlet as below:

<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/config-inlet.png">
</p>

After sending marker via LSL, You can see the stream name in the Inlet page. Choose one then click the Connect button. After that, you will see the marker being added to the data stream as below:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/marker-added.png">
</p>

### Data Input format

Currently we only support sending markers to the Inlet with 2 options:

1. **A simple marker value** - Double / integer type is expected but EmotivPRO will extract only the integer part before adding into data stream.
  * Expected format of data: `{"MarkerValue"}`

2. **Marker with time for timing synchronization** - The marker event is a vector with 3 elements:
  * `MarkerTime` is the epoch time of the event in double type.
  * `MarkerValue` is value of marker. Double / integer type is expected but EmotivPRO will extract only the integer part before adding into data stream.
  * `CurrentTime` is current epoch time (double type) when the marker is being pushed to the Inlet. It is usually later than the `MarkerTime`.
  * Expected format of data: `{"MarkerTime", "MarkerValue", "CurrentTime"}`

## Examples

There are some guidelines and examples on 3rd party applications:

* Guidelines for <a href="examples/matlab/readme.md">MATLAB</a>.

* Guidelines for <a href="examples/openvibe/readme.md">OpenViBE</a>.

* Guidelines for <a href="examples/cpp/readme.md">C++</a> project.

* Guidelines for <a href="examples/psychopy/readme.md">PsychoPy</a> project.

* Guidelines for <a href="examples/python/readme.md">Python</a> project.

* Guidelines for <a href="examples/unity/readme.md">Unity</a> project.



## Reference
* [LSL on Github](https://github.com/sccn/labstreaminglayer)

* [LSL Documentation](https://labstreaminglayer.readthedocs.io/)
* [EmotivPRO LSL guideline](https://emotiv.gitbook.io/emotivpro-v2-0/lab-streaming-layer-lsl)

## Release Notes

See <a href="docs/ReleaseNotes.md">here</a>.
