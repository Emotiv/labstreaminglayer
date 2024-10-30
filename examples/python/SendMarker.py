import random
import time
import csv
from pylsl import StreamInfo, StreamOutlet, local_clock

def get_epoch_now():
    return time.time()

def main():
    print("Choose an option:")
    print("1: Send markers right away without a specific timestamp (recommended)")
    print("2: Send markers with a specific timestamp")

    # User input for option
    while True:
        try:
            option = int(input("Enter your choice (1 or 2): "))
            if option == 1 or option == 2:
                break
            else:
                print("Invalid choice. Please enter 1 or 2.")
        except ValueError:
            print("Invalid input. Please enter a number (1 or 2).")
    
    if option == 1:
        nChannel = 1
        channels = ['MarkerValue']
        print("Start with Option 1")
    else:
        nChannel = 3
        channels = ['MarkerTime', 'MarkerValue', 'TimeSent']
        print("Start with Option 2")

    # Set up LabStreamingLayer (LSL) stream
    info = StreamInfo(name='SimpleMarker', type='Markers', channel_count=nChannel,
                      channel_format='double64', source_id='Outlet1234')

    chns = info.desc().append_child("channels")
    for c in channels:
        chns.append_child("channel").append_child_value("label", c).append_child_value("type", "Marker")

    outlet = StreamOutlet(info)
    print("Now sending data...")

    marker_value = 1
    while True:
        if option == 1:
            now = get_epoch_now()
            print(f"Marker, {marker_value}, {now}")
            outlet.push_sample([marker_value])  # Send just the marker value

        else:
            # Send markers with a specific timestamp
            current_time = get_epoch_now()
            marker_time = current_time - 0.05  # The marker happened 50 ms ago
            sample = [marker_time, marker_value, current_time]
            print(f"Marker with a timestamp, {marker_value}, {sample[0]}, {local_clock()}")
            outlet.push_sample(sample)  # Send marker with a timestamp and value

        marker_value += 1
        time.sleep(2)  # Wait for 2 seconds

if __name__ == "__main__":
    main()
