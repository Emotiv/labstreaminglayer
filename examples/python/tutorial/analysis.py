"""Example EEG preprocessing of EMOTIV data
This is example script demonstrates a few basic functions to import and annotate EEG data collected from EmotivPRO software. It uses MNE to load an XDF file, print some of its basic metadata, create an info object and plot the power spectrum."""

import pyxdf
import mne
import matplotlib.pyplot as plt
import numpy as np

# Path to your XDF file
data_path = '/Users/lucas/Desktop/Emotiv/LSL/sub-P001/ses-S001/eeg/sub-P001_ses-S001_task-Default_run-001_eeg.xdf'

# Load the XDF file
streams, fileheader = pyxdf.load_xdf(data_path)
print("XDF File Header:", fileheader)
print("Number of streams found:", len(streams))

for i, stream in enumerate(streams):
    print("\nStream", i + 1)
    print("Stream Name:", stream['info']['name'][0])
    print("Stream Type:", stream['info']['type'][0])
    print("Number of Channels:", stream['info']['channel_count'][0])
    sfreq = float(stream['info']['nominal_srate'][0])
    print("Sampling Rate:", sfreq)
    print("Number of Samples:", len(stream['time_series']))
    print("Print the first 5 data points:", stream['time_series'][:5])

    channel_names = [chan['label'][0] for chan in stream['info']['desc'][0]['channels'][0]['channel']]
    print("Channel Names:", channel_names)
    channel_types = 'eeg'

# Create MNE info object
info = mne.create_info(channel_names, sfreq, channel_types)
data = np.array(stream['time_series']).T # Data needs to be transposed: channels x samples
raw = mne.io.RawArray(data, info)
raw.plot_psd(fmax=50) # plot a simple spectrogram (power spectral density)
