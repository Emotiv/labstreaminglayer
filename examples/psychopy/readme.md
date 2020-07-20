# Guideline for PsychoPy to work with EMOTIV LSL Interface

The following guide describes how Psychopy send marker to EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Get and install PsychoPy from https://www.psychopy.org/ (latest version 2020.1.3)
* Install python3 (version 3.6.8 recommended)
* Install pylsl (the Python interface of LSL) 
```
   pip3.6 install pylsl
```

## How to send marker from PsychoPy builder to EMOTIV LSL Inlet

1. Open **PsychoPy Builder** then load marker_builder.psyexp.

2. Hit **Run** button then connect to 'PsychoPySimpleMarker' stream on EmotivPro Inlet setting as below:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/psychopy_builder_inlet_config.png">
</p>

The example will create a simple marker event stream. The value of marker will be loaded from marker_data.csv randomly. A marker event will be sent every 1000ms.


## How to send marker from PsychoPy coder to EMOTIV LSL Inlet
1. Open **PsychoPy Coder** and load sendMarker.py. The example will create a marker event stream named 'PsychoPyMarker'

2. Hit **Run** button then connect to 'PsychoPyMarker' stream on EmotivPro Inlet setting. The result will be shown as below:
<p align="center">
  <img src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/sendmarker_PsychoPyCoder.png">
</p>

The example will create a LSL Outlet stream have 3 channels. Each event is array containing MarkerTime, MarkerValue, CurrentTime. A marker is sent to LSL every 1500ms.
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

## Reference

* [PsychoPy Document](https://www.psychopy.org/documentation.html)

* [PsychoPy LSL Example](https://github.com/kaczmarj/psychopy-lsl)

