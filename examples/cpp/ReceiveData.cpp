#include <thread>
#include <iostream>
#include <lsl_cpp.h>
#include <string>


/**
 * Example program that demonstrates how to resolve a specific stream on the lab
 * network and how to connect to it in order to receive data.
 */

void printChunk(const std::vector<double>& chunk, std::size_t n_channels) {
    for(std::size_t i=0; i < chunk.size(); ++i) {
        std::cout << chunk[i] << ' ';
        if (i % n_channels == n_channels - 1)
            std::cout << '\n';
    }
}

void printChunk(const std::vector<std::vector<double>>& chunk) {
    for(const auto& vec: chunk)
        printChunk(vec, vec.size());
}

int main(int argc, char* argv[]) {
    std::string field, value, transmitType;
    const int max_samples = argc > 3 ? std::stoi(argv[4]) : 100; // default 100 sample will be printed
    if (argc < 3) {
        std::cout << "This connects to a stream which has a particular value for a "
            "given field and receives data.\nPlease enter a field name and the desired and transmitType (sample or chunk) "
            "value (e.g. \"type EEG sample\" (without the quotes)):"
            << std::endl;
        std::cin >> field >> value >> transmitType;
    }
    else {
        field = argv[1];
        value = argv[2];
        transmitType = argv[3];
    }

    
    // resolve the stream of interet
    std::cout << "Now resolving streams... " << std::endl;
    try {
        std::vector<lsl::stream_info> results = lsl::resolve_stream(field, value);

        std::cout << "Here is what was resolved: " << std::endl;
        std::cout << results[0].as_xml() << std::endl;

        // make an inlet to get data from it
        std::cout << "Now creating the inlet..." << std::endl;
        lsl::stream_inlet inlet(results[0]);

        // get the full stream info (including custom meta-data) and dissect it
        lsl::stream_info inf = inlet.info();
        std::cout << "\nThe manufacturer is: " << inf.desc().child_value("manufacturer")
            << "\nThe channel labels are as follows:\n";
        lsl::xml_element ch = inf.desc().child("channels").child("channel");
        for (int k = 0; k < inf.channel_count(); k++) {
            std::cout << "  " << ch.child_value("label") << std::endl;
            ch = ch.next_sibling();
        }

        // start receiving & displaying the data
        std::cout << "Now pulling samples..." << std::endl;

        std::vector<double> sample;
        std::vector<std::vector<double>> chunk_nested_vector;
        for (int i = 0; i < max_samples; ++i) {
            // pull a single sample
            if (transmitType == "sample") {
                inlet.pull_sample(sample);
                printChunk(sample, inlet.get_channel_count());
                // Sleep so the outlet will have time to push some samples
                std::this_thread::sleep_for(std::chrono::milliseconds(10));
            }
            else if (transmitType == "chunk") {
                // pull a chunk into a nested vector - easier, but slower
                inlet.pull_chunk(chunk_nested_vector);
                printChunk(chunk_nested_vector);
                std::this_thread::sleep_for(std::chrono::milliseconds(1000));
            }
            else {
                std::cout << "Not support transmit type: " << transmitType << std::endl;
            }
        }

    } catch (std::exception& e) { std::cerr << "Got an exception: " << e.what() << std::endl; }
    if (argc == 1) {
        std::cout << "Press any key to exit. " << std::endl;
        std::cin.get();
    }
    return 0;
}
