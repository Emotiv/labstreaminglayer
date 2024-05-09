"""LSL example - playing audio and sending a trigger
This script shows minmimal example code that will allow a user to play and audio file and simultaneously send a a trigger through an LSL stream that can be captured (for instance, using LabRecorder software) and synchronized with other LSL data streams.

Operation:
Once run in the Terminal, this script immediately initates an LSL stream. Whenever the 'Enter' key is pressed, it sends a trigger and plays an audio file."""

import sounddevice as sd
import soundfile as sf
from pylsl import StreamInfo, StreamOutlet

def wait_for_keypress():
    print("Press ENTER to start audio playback and send an LSL marker.")

    while True: # This loop waits for a keyboard input
        input_str = input()  # Wait for input from the terminal
        if input_str == "":  # If the enter key is pressed, proceed
            break

def AudioMarker(audio_file, outlet): # function for playing audio and sending marker
    data, fs = sf.read(audio_file) # Load the audio file

    print("Playing audio and sending LSL marker...")
    marker_val = [1]
    outlet.push_sample(marker_val) # Send marker indicating the start of audio playback
    sd.play(data, fs) # play the audio
    sd.wait()  # Wait until audio is done playing
    print("Audio playback finished.")

if __name__ == "__main__": # MAIN LOOP
    # Setup LSL stream for markers
    stream_name = 'AudioMarkers'
    stream_type = 'Markers'
    n_chans = 1
    sr = 0  # Set to 0 sampling rate because markers are irregular
    chan_format = 'int32'
    marker_id = 'uniqueMarkerID12345'

    info = StreamInfo(stream_name, stream_type, n_chans, sr, chan_format, marker_id)
    outlet = StreamOutlet(info) # create LSL outlet

    # Keep the script running and wait for ENTER key to play audio and send marker
    while True:
        wait_for_keypress()
        audio_filepath = "/path/to/your/audio_file.wav"  # replace with correct path to your audio file
        AudioMarker(audio_filepath, outlet)
        # After playing audio and sending a marker, the script goes back to waiting for the next keypress
    