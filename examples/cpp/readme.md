# Cpp Examples work Emotiv LSL Interface

The examples will show how to get data from Eotiv LSL Outlet and send marker to Emotiv LSL Inlet.

## Quick start
1. Get latest liblsl from [github](https://github.com/sccn/liblsl/releases). Currently, we use version v1.14.0b1. 
2. Build examples

## How to use

1. Setup clientId, clientSecret, appName, appVersion for identifying application.
1. Start authorization procedure: start connecting Cortex then authorize to get token to work with Cortex. After authorizing successfully, the plugin will find headsets automatically.
1. Start data streaming: create and activate a session with a headset and subscribe to particular data streams.
1. You can subscribe or unsubscribe different data streams, and perform other tasks such as recording and training.

For more details please refer to [Unity example](https://github.com/Emotiv/cortex-v2-example/tree/master/unity) 

## Release Notes

See <a href="Documentation/ReleaseNotes.md">here</a>.



