"""Minimal example of how to send event triggers in PsychoPy with
LabStreamingLayer.

In this example, an event marker with marker value is random number [1-99] is sent to LSL with the appearance of each marker.

TO RUN: open in PyschoPy Coder and press 'Run'. Or if you have the psychopy
Python package in your environment, run `python sendMarker.py` in command line.

"""
from psychopy import core, visual, event
from pylsl import StreamInfo, StreamOutlet
import random
import datetime


def main():
    """Alternate printing 'Hello' and 'World' and send a trigger each time."""
    # Set up LabStreamingLayer stream.
    info = StreamInfo(name='PsychoPyMarker', type='Markers', channel_count=3,
                      channel_format='double64', source_id='unique012345')
    chns = info.desc().append_child("channels")
    #MarkerTime is the time you want to add a marker. It is an epoch time.
    #MarkerValue is value of marker
    #CurrentTime is current time at epoch time
    for label in ["MarkerTime", "MarkerValue", "CurrentTime"]:
        ch = chns.append_child("channel")
        ch.append_child_value("label", label)
        ch.append_child_value("type", "Marker")
    info.desc().append_child_value("manufacturer", "PsychoPy")
    
    outlet = StreamOutlet(info)  # Broadcast the stream.

    # Instantiate the PsychoPy window and stimuli.
    win = visual.Window([800, 600], allowGUI=False, monitor='testMonitor',
                        units='deg')

    while (True):
        markerValue = random.randint(1, 99)
        print("Marker value: ", markerValue)
        displayText = visual.TextStim(win, text = "Send Marker has value: " + str(markerValue))
        displayText.draw()
        win.flip()

        now = datetime.datetime.now().timestamp()
        print(now)
        # data: MarkerTime, MarkerValue, Current EpochTime
        data = [now, markerValue, now]
        
        outlet.push_sample(data)
        
        if 'escape' in event.getKeys():  # Exit if user presses escape.
            break
        core.wait(1.0)  # Display text for 1.0 second.
        win.flip()
        core.wait(0.5)  # ISI of 0.5 seconds.

    win.close()
    core.quit()

if __name__ == "__main__":
    main()
