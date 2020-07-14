# Guideline for Python to work with EMOTIV LSL Interface

The following guide describes how Psychopy send marker to EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Install python3 (version 3.7 or later)
* Install pylsl (the Python interface of LSL) 
```
   pip install pylsl
```

## How to send marker from Python to EMOTIV LSL Inlet

1. Open SendMarker.py file and run

2. Open Emotiv Pro app then connect to 'PyMarker' stream on EmotivPro Inlet setting as below:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/py_example_inlet_config.PNG">
</p>

The example will create a LSL Outlet stream have 3 channels. Each event is array containing MarkerTime, MarkerValue, CurrentTime. The markertime of marker will be loaded from sending_marker.csv randomly.
A marker is sent to LSL every 1000ms.
In the example, a marker value is a random number from 1-99.
```python
# Set up LabStreamingLayer stream.
info = StreamInfo(name='PsychoPyMarker', type='Markers', channel_count=3,
                  channel_format='double64', source_id='unique012345')
chns = info.desc().append_child("channels")
#MarkerTime is the time you want to add a marker. It is an epoch time.
#MarkerValue is value of marker.
#CurrentTime is current time at epoch time
for label in ["MarkerTime", "MarkerValue", "CurrentTime"]:
    ch = chns.append_child("channel")
    ch.append_child_value("label", label)
    ch.append_child_value("type", "Marker")
info.desc().append_child_value("manufacturer", "PsychoPy")

outlet = StreamOutlet(info)  # Broadcast the stream.
```

## How to receive data from EMOTIV LSL Outlet

1. Go to **Lab Streaming Layer** page, **Outlet** tab in EmotivPRO, choose the desire **Data stream** type and **Data format** 
Click **Start** button to start streaming.

2. Open ReceiveData.py file and run


## Reference

* [LSL Document]( https://labstreaminglayer.readthedocs.io/ )

* [Py LSL Example] ( https://github.com/labstreaminglayer/liblsl-Python/tree/master/pylsl )

