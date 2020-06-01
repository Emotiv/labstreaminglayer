# Emotiv Lab Streaming Layer Interface

Here are guidelines and some examples to an application work with Emotiv Cortex Service(shortly called Cortex) via LSL.

## Prerequisites

1. [Download and install](https://www.emotiv.com/developer/) the EmotivApp and EmotivPro. The EmotivApp is for login process while EmotivPro for configuring LSL both Inlet and Outlet.
2. Download lsl libary.

3. Install an application supporting LSL to work with Cortex such as:
  - Matlab: recommend use Matlab R2017a or later (because some functions of MatlabViewer do not support old version)
  - OpenVibe:

## How to configure LSL on EmotivPro

After headset connected, go to LSL settings menu on EmotivPro. There are 2 tabs Outlet and Inlet.
- Outlet: Configure for data streams (EEG, Motions, Performance metrics) as LSL Outlet.
- Inlet: To support add marker from a outside LSL Outlet to Emotiv data streams.

Please see the below images for details:



## How to use

There are guidelines and examples work with Cortex data streams:

  1.Guideline for <a href="examples/matlab/readme.md">matlab</a>.

  2.Guideline for <a href="examples/openvibe/readme.md">openvibe</a>.

  3.Guideline for <a href="examples/cpp/readme.md">cpp</a> project.

  4.Guideline for <a href="examples/python/readme.md">python</a> project.

## Release Notes

See <a href="docs/release_notes.md">here</a>.



