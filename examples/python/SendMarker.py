"""Minimal example of how to send event markers with LabStreamingLayer.
In this example, an event marker with marker value is random number [1-99] is sent to LSL with the appearance of each marker.
"""
from pylsl import StreamInfo, StreamOutlet
import random
import time
import csv


def main():

    # Set up LabStreamingLayer stream.
    info = StreamInfo(name='PyMarker', type='Markers', channel_count=3,
                      channel_format='double64', source_id='unique113')

    # Broadcast the stream.       
    outlet = StreamOutlet(info)

    print("Now sending data...")

    # create output markers csv file
    outfile = open('sending_marker.csv', 'w', newline='')
    out = csv.writer(outfile)
    out.writerow(['Timestamp'])

    while (True):
        markerValue = random.randint(1, 99)
        print("Marker value: ", markerValue)

        now = time.time()
        print("Marker time: ", now)

        # save time to csv file
        outfile = open('sending_marker.csv', 'a+', newline='')
        out = csv.writer(outfile)
        out.writerow([now])

        # data: MarkerTime, MarkerValue, Current EpochTime
        data = [now, markerValue, now]
        
        # push data 
        outlet.push_sample(data)

        time.sleep(1)
    

if __name__ == "__main__":
    main()



