#include "User.hpp"

User::User(boost::asio::io_service& io_service) : _socket(io_service)
{
}

void User::doRead()
{
	boost::asio::async_read_until(_socket, _dataBuffer, '\0', boost::bind(&User::handleRead, this, boost::asio::placeholders::error, boost::asio::placeholders::bytes_transferred));
}

void User::write(std::string message)
{	
	boost::asio::async_write(_socket, boost::asio::buffer(message.c_str(), message.size()), boost::bind(&User::handleWrite, this, boost::asio::placeholders::error));
}

void User::handleRead(boost::system::error_code error, size_t bytes_transferred)
{
	if (!error)
	{
		std::string message((std::istreambuf_iterator<char>(&_dataBuffer)), std::istreambuf_iterator<char>());
		std::cout << message << std::endl;
		doRead();
	}
	else
	{
		std::cerr << "Error: " << error.message() << std::endl;
	}

}

void User::handleWrite(boost::system::error_code error)
{
	if (error) 
	{
		std::cerr << "Error: " << error.message() << std::endl;
	}
}
