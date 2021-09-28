"""Example program to show how to read a multi-channel time series from LSL."""
import time
from pylsl import StreamInlet, resolve_stream

# first resolve an EEG stream on the lab network
print("looking for a stream...")
streams = resolve_stream('type', 'Motion')
print(streams)

# create a new inlet to read from the stream
inlet = StreamInlet(streams[0])

while True:
    # get a new sample (you can also omit the timestamp part if you're not
    # interested in it)
    sample, timestamp = inlet.pull_sample()
    if timestamp != None:
        print(timestamp, sample)


