# Guideline for OpenVibe work with Emotiv LSL Interface

The  following quick-guide provides a brief description how openVibe work with Emotiv LSL Interface.

## Prerequisites
1. Login via EmotivApp and connect a headset on EmotivPro.

## How Openvibe receive data from Emotiv LSL Outlet stream.
Please follow below guide step by step:

1. Go to Lab streaming layer setting on EmotivPro, choose data stream and data format (cf_float32 because OpenVibe only support this format).
Click start button to start LSL Outlet data stream.
2. Open the OpenVibe acquisition server then select Lab Streaming Layer (LSL) option in the driver field.
With the driver selected, click the "Driver Properties" button. When the page is open, make sure EmotivDataStream-xx/xx is selected in the "Signal stream" field.
The EmotivDataStream-EEG stream is for EEG, the EmotivDataStream-Motion stream is for Motion data stream...
<p align="center">
  <img width="500" height="357" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-connect-emotivOutlet_500x357.jpg">
</p>
3. Connect to the LSL stream and click Play button to start receiving data.
<p align="center">
  <img width="400" height="265" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-startreceivingdata-emotivOutlet_400x265.jpg">
</p>
4. Open the OpenViBE designer, then open [LSLViewer.mxs](./LSLViewer.mxs) file. The program connect an "Acquisition client" block to a "Signal display" to see if both signals from EmotivPro match those in OpenVibe.
Click "Acquisition client" to change setting, make sure the acquisition server port match with connection port at Acquisition server.
<p align="center">
  <img width="400" height="350" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-designer-emotivOutlet_400x350.jpg">
</p>
5. Click play button to display data stream
<p align="center">
  <img width="500" height="400" src="https://github.com/Emotiv/labstreaminglayer/blob/emotiv-lsl/docs/images/openvibe-displayData-emotivOutlet_500x400.jpg">
</p>


## How to send marker from Openvibe to EmotivPro Inlet

TODO

## Reference
1. [OpenViBE and LabStreamingLayer](http://openvibe.inria.fr/how-to-use-labstreaminglayer-in-openvibe/)
2. [OpenViBELSL ConnectionQuick-Guide](https://bitalino.com/docs/quick_guide_OpenVIBE.pdf)



