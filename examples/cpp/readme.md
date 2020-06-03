# Cpp Examples work with Emotiv LSL Interface

The  following quick-guide provides a brief description how a Cpp Examples work with Emotiv LSL Interface.

## Prerequisites
* Login via EmotivApp and connect a headset on EmotivPro.
* Install Cmake version 3.5 or later.

## How to build project
1.Clone the repository

```
git clone https://github.com/Emotiv/labstreaminglayer.git
```
    
2.Go to examples/cpp and create the build directory

3.Configure the project using cmake

```
mkdir build && cd build
cmake .. -G <generator name>
```
if windows64 bit add a definition -DWIN64=1

If you used a generator, you can now open the IDE project file. Then build the install target.
Alternatively, you can build directly from command line as below: 
```
cmake --build . --config Release --target install
```

## How to receive data from Emotiv LSL Outlet stream.
Please follow below guide step by step:

1. Go to Labs streaming layer setting-Outlet on EmotivPro, choose data stream type and data format.
Click start button to start LSL Outlet data stream.
2. Build and run ReceiveData executable. 

If you run executable from IDE, please enter a field name and the desired value and transmitType (sample or chunk) on console as below:
```
type EEG sample
```
If you run the executable directly on terminal or commandline:
```
ReceiveData type EEG chunk 50
```
The last field is maximum number of sample will be printed out to console.

3. The result will be shown as below:
<p align="center">
  <img width="500" height="407" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/cpp-result-eegOutlet-500x407.jpg">
</p>

## How to send marker from a Cpp Example to Emotiv LSL Inlet

Emotiv LSL Inlet support add both simple marker and marker with time (for timing synchronization between machine).
1. Build and Run SendMarker or SendSimpleMarker.
2. Go to Labs streaming layer setting-Inlet on EmotivPro, choose a outlet stream in stream name (MarkerWithTimeStamp or SimpleMarker). And click Connect button.
<p align="center">
  <img width="400" height="286" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/cpp-config-Inlet-400x286.jpg">
</p>
3. The result will be shown as below 
<p align="center">
  <img width="500" height="286" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/cpp-markerResult-Inlet-500x286.jpg">
</p>

## Reference
1. [Lab streaming layer]https://labstreaminglayer.readthedocs.io/info/getting_started.html
2. [build liblsl](https://labstreaminglayer.readthedocs.io/dev/lib_dev.html)



