#pragma once
#include <iostream>
#include <boost/asio.hpp>

class User
{
public:
	User(boost::asio::io_service& io_service);
	~User();

	void setName(std::string& name) { _name = name; }

	boost::asio::ip::tcp::socket& getSocket() { return _socket; }
	std::string getName() { return _name; }

private:
	std::string _name;
	boost::asio::ip::tcp::socket _socket;

};

