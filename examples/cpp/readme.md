# Guideline for C++ to work with EMOTIV LSL Interface

The following guide describes how C++ works with EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Require C++ compiler (gcc, MinGW, etc)
* Install CMake version 3.5 or later

## How to build project

1. Clone this repository
```
git clone https://github.com/Emotiv/labstreaminglayer.git
```
    
2. Go to `examples/cpp` and create the build directory

3. Configure the project using cmake

For Windows 64-bit please add the param `-DWIN64=1`
```
mkdir build && cd build

# if you prefer to generate a project file for your IDE
# checkout https://cmake.org/cmake/help/latest/manual/cmake-generators.7.html
cmake .. -G <generator name>

For example: 
 Generates Visual Studio 15 (VS 2017) project files.
    cmake .. -G "Visual Studio 15 2017" -A x64 -DWIN64=1
 Generate Xcode project files
    cmake .. -G Xcode
```

If you use a generator, you can now open the generated project file with your IDE. Then build the `install` target.
Alternatively, you can build directly from command line: 
```
cmake --build . --config Release --target install
```

## How to receive data from EMOTIV LSL Outlet stream in C++

1. Go to **Lab Streaming Layer** page, **Outlet** tab in EmotivPRO, choose the desire **Data stream** type and **Data format** 
Click **Start** button to start streaming.

2. Build and run `ReceiveData` binary. 

If you run it from IDE, please enter a field name and the desired value and transmitType (sample or chunk) on console as below:
```
type EEG sample
```
If you run the executable directly on command line:
```
ReceiveData type EEG chunk 50
```
The last parameter is the number of sample to be printed out in console.

3. If succeeded you can see the result like this:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/cpp-receivedata-result.png">
</p>

## How to send marker from C++ to EMOTIV LSL Inlet

EMOTIV LSL Inlet supports add both simple marker and marker with desire event time.

1. Build and Run `SendMarker` or `SendSimpleMarker`.

2. Go to **Lab Streaming Layer** page, **Inlet** tab on EmotivPRO, choose **MarkerWithTimeStamp** or **SimpleMarker** in stream name. Then click the **Connect** button.

3. If succeeded you can see the result like this:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/cpp-sendmarker.png">
</p>

## Reference

* LabStreamingLayer https://labstreaminglayer.readthedocs.io/
  * Build liblsl https://labstreaminglayer.readthedocs.io/dev/lib_dev.html



