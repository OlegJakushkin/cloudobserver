/*
 * Boost.Extension / multiple inheritance example (boat)
 *
 * (C) Copyright Jeremy Pack 2007
 * Distributed under the Boost Software License, Version 1.0. (See             
 * accompanying file LICENSE_1_0.txt or copy at
 * http://www.boost.org/LICENSE_1_0.txt)
 *
 * See http://www.boost.org/ for latest version.
 */

#ifndef BOOST_EXTENSION_BOAT_HPP
#define BOOST_EXTENSION_BOAT_HPP
//  See the FAQ for info about why the following is necessary
//  here, but usually isn't.
#include "../headers-interfaces/vehicle.hpp"
#include <iostream>
#include <typeinfo>

class BOOST_EXTENSION_EXPORT_DECL boat : virtual public vehicle
{
public:
  boat(void){std::cout << "\nCreated a Boat";}
  virtual ~boat(void){std::cout << "\nDestroyed a Boat";}
  virtual std::string list_capabilities(void);
};

#endif
