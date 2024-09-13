#include <thread>
#include <lsl_cpp.h>
#include <iostream>
#include <iomanip>
#include <vector>

/**
 * This is an example of how a simple data stream can be offered on the network.
 * Here, the stream is named SimpleMarker, has content-type Marker, and 1 channels.
 * The transmitted samples contain random numbers (and the sampling rate is irregular)
 */

double getEpochNow()
{
    auto start_time = std::chrono::system_clock::now();
    double currTime = std::chrono::duration_cast<std::chrono::microseconds>(start_time.time_since_epoch()).count() / 1000000.0;
    currTime = static_cast<long long>(currTime * 1000000) / 1000000.0;
    return currTime;
}

using namespace std::chrono;

int main(int argc, char* argv[]) {

    std::cout << "Choose an option:\n";
    std::cout << "1: Send markers right away without a specific timestamp (recommended)\n";
    std::cout << "2: Send markers with a specific timestamp\n";

    int option = 1;     // Setting your option here
    int nChannel = 1;
    std::vector<std::string> channels = {"MarkerValue"};

    if (option == 1) {
        nChannel = 1;
        channels = {"MarkerValue"};
        std::cout << "Start with Option 1\n";
    } else {
        nChannel = 3;

        /// MarkerTime: is the marker timestamp. It is an epoch time.
        /// MarkerValue: is value of marker
        /// TimeSent: when marker is sent. It is an epoch time.
        channels = { "MarkerTime","MarkerValue", "TimeSent"};
        std::cout << "Start with Option 2\n";
    }

    // make a new stream_info
    lsl::stream_info info("SimpleMarker", "Markers", nChannel, lsl::IRREGULAR_RATE, lsl::channel_format_t::cf_double64, "Outlet1234");

    info.desc().append_child_value("manufacturer", "LSL");
    lsl::xml_element chns = info.desc().append_child("channels");

    for (int c = 0; c < nChannel; c++) {
        chns.append_child("channel").append_child_value("label", channels.at(c).c_str()).append_child_value("type", "Marker");
    }

    lsl::stream_outlet outlet(info);
    while (!outlet.wait_for_consumers(180));

    int markerValue = 1;
    std::vector<double> sample(nChannel);

    while (outlet.have_consumers()) {

        if(option == 1) {
            std::cout << std::fixed << std::setprecision(6)
                      << "Marker, " << markerValue << ", " << getEpochNow() << std::endl;
            outlet.push_sample(&markerValue);
        } else {
            auto timeSent = std::chrono::duration_cast<milliseconds>(system_clock::now().time_since_epoch()).count();
            sample[0] = (double)timeSent - 50;    // The marker happened 50 ms ago.
            sample[1] = (double)markerValue;
            sample[2] = (double)timeSent;

            std::cout << std::fixed << std::setprecision(6)
                      << "Marker with a timestamp, " << markerValue
                      << ", " << sample[0]
                      << ", " << lsl_local_clock()
                      << std::endl;
            outlet.push_sample(sample);
        }

        markerValue ++;
        std::this_thread::sleep_for(std::chrono::milliseconds(2000));
    }

    std::cout << "Press any key to exit. " << std::endl;
    std::cin.get();
    return 0;
}
