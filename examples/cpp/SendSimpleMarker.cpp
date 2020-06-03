#include <thread>         // std::this_thread::sleep_for
#include <lsl_cpp.h>
#include <iostream>

/**
 * This is an example of how a simple data stream can be offered on the network.
 * Here, the stream is named SimpleMarker, has content-type Marker, and 1 channels.
 * The transmitted samples contain random numbers (and the sampling rate is irregular)
 */

using namespace std::chrono;

int main(int argc, char* argv[]) {
	
	try {
		// make a new stream_info
		lsl::stream_info info("SimpleMarker", "Markers", 1, lsl::IRREGULAR_RATE, lsl::channel_format_t::cf_double64, "Outlet1234");

		info.desc().append_child_value("manufacturer", "LSL");
		lsl::xml_element chns = info.desc().append_child("channels");

		// MarkerValue is value of marker
		std::vector<std::string> channels = {"MarkerValue"};
		for (int c = 0; c < 1; c++) {
			chns.append_child("channel").append_child_value("label", channels.at(c).c_str()).append_child_value("type", "Marker");
		}
		// make a new outlet
		lsl::stream_outlet outlet(info);

		while (!outlet.wait_for_consumers(120));

		int count = 0;
		while (outlet.have_consumers()) {
			// generate random data
			int markerValue = rand() % 100;

			// send it
			std::cout << "now sending: " << markerValue << std::endl;
			outlet.push_sample(&markerValue);
			std::this_thread::sleep_for(std::chrono::milliseconds(1000));
		}
	}
	catch (std::exception& e) { std::cerr << "Got an exception: " << e.what() << std::endl; }
	std::cout << "Press any key to exit. " << std::endl;
	std::cin.get();
	return 0;
}
