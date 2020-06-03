#include <thread>         // std::this_thread::sleep_for
#include <chrono>         // std::chrono::seconds
#include <lsl_cpp.h>
#include <iostream>

/**
 * This is an example of how a simple data stream can be offered on the network.
 * Here, the stream is named MarkerWithTimeStamp, has content-type Marker, and 3 channels.
 * The transmitted samples contain random numbers (and the sampling rate is irregular)
 */

const int nchannels = 3;
using namespace std::chrono;

int main(int argc, char* argv[]) {
	
	try {
		// make a new stream_info
		lsl::stream_info info("MarkerWithTimeStamp", "Markers", 3, lsl::IRREGULAR_RATE, lsl::channel_format_t::cf_double64, "Outlet1234");

		info.desc().append_child_value("manufacturer", "LSL");
		lsl::xml_element chns = info.desc().append_child("channels");

		// MarkerTime is the time you want to add a marker. It is an epoch time.
		// MarkerValue is value of marker
		// CurrentTime is current time at epoch time
		std::vector<std::string> channels = { "MarkerTime","MarkerValue", "CurrentTime"};
		for (int c = 0; c < 3; c++) {
			chns.append_child("channel").append_child_value("label", channels.at(c).c_str()).append_child_value("type", "Marker");
		}
		// make a new outlet
		lsl::stream_outlet outlet(info);

		while (!outlet.wait_for_consumers(120));

		double sample[nchannels];
		int count = 0;
		while (outlet.have_consumers()) {
			// generate random data
			int markerValue = rand() % 100;
			
			// current epoc time
			auto now = std::chrono::duration_cast<milliseconds>(system_clock::now().time_since_epoch()).count();
			sample[0] = double(now);
			sample[1] = (double)markerValue;
			sample[2] = double(now);
			// send it
			std::cout << "now sending: " << markerValue << std::endl;
			outlet.push_sample(sample);
			std::this_thread::sleep_for(std::chrono::milliseconds(1000));
		}
		std::cout << "Have no consumers" << std::endl;
	}
	catch (std::exception& e) { std::cerr << "Got an exception: " << e.what() << std::endl; }
	std::cout << "Press any key to exit. " << std::endl;
	std::cin.get();
	return 0;
}
