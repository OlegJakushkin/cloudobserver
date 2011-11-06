//if service needs access to server main database it shall be presented with shared_ptr to User Controll class and service shall call request method to search thru DB.

#ifndef GENERAL_UTILITIES_H
#define GENERAL_UTILITIES_H

//STD
#include <iostream>
#include <sstream>
#include <string>
#include <map>
#include <functional>
#include <locale>
#include <assert.h>
#include <stdio.h>

//OpenSSL
#include <openssl/sha.h>

//Boost
#include <boost/filesystem.hpp>
#include <boost/date_time/posix_time/posix_time.hpp>

class general_utils
{
public:
	general_utils(){}
	~general_utils(){}

	void create_directory( boost::filesystem::path path);
	std::string get_dif_path(boost::filesystem::path base_path, boost::filesystem::path new_path);
	std::wstring get_dif_path_wstring(boost::filesystem::path base_path, boost::filesystem::path new_path);
	std::string to_lower(std::string data); // Cross lingual function to make string lowercase.
	std::string get_sha256( std::string value); // gets SHA256 from string
	void print_map_contents(std::map <std::string, std::string> data);
	void print_map_contents(std::map <std::string, std::string> data, std::string name);
	std::string get_utc_now_time();
};

#endif // GENERAL_UTILITIES_H