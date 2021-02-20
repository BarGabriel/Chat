#pragma once
#include <iostream>

#include <boost/asio.hpp>
#include <boost/bind.hpp>

class User
{
public:
	User(boost::asio::io_service& io_service);
	void doRead();
	void write(std::string message);

	void setName(std::string& name) { _name = name; }

	boost::asio::ip::tcp::socket& getSocket() { return _socket; }
	std::string getName() { return _name; }
	boost::asio::streambuf& getDataBuffer() { return _dataBuffer; }

private:
	void handleRead(boost::system::error_code error, size_t bytes_transferred);
	void handleWrite(boost::system::error_code error);

	std::string _name;
	boost::asio::ip::tcp::socket _socket;
	boost::asio::streambuf _dataBuffer;

};

