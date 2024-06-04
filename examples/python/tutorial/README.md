# Emotiv LSL tutorial
This sub-repository contains the materials that support the Emotiv LSL tutorial, which can be found on our website (https://www.emotiv.com/blogs/tutorials/emotiv-lab-streaming-layer-lsl)

## Prerequisites
1. Basic working knowledge of Python coding
2. Emotiv hardware device(s)
3. EmotivPRO license
4. PyLSL library
5. LabRecorder software
Check tutorial for details on how to obtain these

## Files
1. Jupyter notebook of tutorial
2. A script called `sendAudioMarkers.py` that plays an audio file and simultaneously sends a marker via LSL
3. A script called `analysis.py` that provides basic functions in MNE to import the data (which has previously been recorded and saved by LabRecorder), view metadata and make a basic power spectral density plot.
