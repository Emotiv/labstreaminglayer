"""Example program to show how to read a multi-channel time series from LSL."""
import time
from pylsl import StreamInlet, resolve_stream

print("looking for a stream...")
# first resolve a Motion stream on the lab network
streams = resolve_stream('type', 'Motion') # You can try other stream types such as: EEG, EEG-Quality, Contact-Quality, Performance-Metrics, Band-Power
print(streams)

# create a new inlet to read from the stream
inlet = StreamInlet(streams[0])

while True:
    # Returns a tuple (sample,timestamp) where sample is a list of channel values and timestamp is the capture time of the sample on the remote machine,
    # or (None,None) if no new sample was available
    sample, timestamp = inlet.pull_sample()
    if timestamp != None:
        print(timestamp, sample)


