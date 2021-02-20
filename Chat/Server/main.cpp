#include <iostream>

#include <boost/asio.hpp>

#include "Server.hpp"

static constexpr int port = 1234;

int main() 
{
	boost::asio::io_service io_service;
	Server myServer(io_service, port);
	io_service.run();

	system("pause");
	return 0;
}