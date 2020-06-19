# Guideline for PsychoPy to work with EMOTIV LSL Interface

The following guide describes how Psychopy works with EMOTIV LSL Interface.

## Prerequisites
* [Download and install](https://www.emotiv.com/developer/) the EMOTIV App and EmotivPRO
* Get a EmotivPRO license from https://www.emotiv.com/emotivpro/
* Get and install PsychoPy from https://www.psychopy.org/ (latest version 2020.1.3)
* Install pylsl (the Python interface of LSL)
'''
pip install pylsl
''

## How to receive data from EMOTIV LSL Outlet stream in PsychoPy



## How to send marker from PsychoPy coder to EMOTIV LSL Inlet
1. Open PyschoPy Coder and load sendMarker.py. The example will create a stream marker event named 'PsychoPyMarker'

2. Hit **Run** button, the result will be shown as below:


## How to send marker from PsychoPy builder to EMOTIV LSL Inlet

## Reference

* [PsychoPy Document](https://www.psychopy.org/documentation.html)

* [PsychoPy LSL Example](https://github.com/kaczmarj/psychopy-lsl)

