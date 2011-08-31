#ifndef GENERAL_MAP_DATA_STRUCTURE_H
#define GENERAL_MAP_DATA_STRUCTURE_H

#include <map>
#include <boost/thread.hpp>
#include <boost/thread/locks.hpp>

#include "test_data_structure.h"

template <class map_t_1, class map_t_2>
class general_map_data_structure//: public test_data_structure<map_t_1, map_t_2>
{
private:

	std::map<map_t_1, map_t_2> _ds;
	mutable boost::mutex mut_;
public:
	general_map_data_structure() {}

	bool containsKey(map_t_1 key)
	{
        boost::mutex::scoped_lock lock(mut_);
        return _ds.find(key) != _ds.end();
    }

	map_t_2 get(map_t_1 key) {
		boost::mutex::scoped_lock lock(mut_);
		return _ds[key];
	}

	map_t_1 put(map_t_1 key, map_t_2 value) {
		boost::mutex::scoped_lock lock(mut_);
		_ds.insert(std::pair<map_t_1, map_t_2>(key,value));
		return key;
	}

	int remove(map_t_1 key) {
		boost::mutex::scoped_lock lock(mut_);
		return _ds.erase(key);
	}

	int size() {
		boost::mutex::scoped_lock lock(mut_);
		return _ds.size();
	}
	const char* name() {
		return "StdMap";
	}

	void print() {}
	void shutdown() {}

};

#endif // GENERAL_MAP_DATA_STRUCTURE_H