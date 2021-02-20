#pragma once
#include <iostream>
#include <boost/asio.hpp>

class User
{
public:
	User(boost::asio::ip::tcp::socket socket, std::string& name);
	User(User& user);
	~User();

	boost::asio::ip::tcp::socket getSocket() { return std::move(_socket); }
	std::string getName() { return _name; }

private:
	std::string _name;
	boost::asio::ip::tcp::socket _socket;

};

