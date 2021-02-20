#pragma once
#include <vector>

#include <boost/asio.hpp>
#include <boost/bind.hpp>

#include "User.hpp"

class Server
{
public:
	Server(boost::asio::io_service& io_service, uint32_t port);

private:
	void startAccept();
	void handleAccept(boost::system::error_code ec);

	uint32_t _port;
	boost::asio::io_service& _io_service;
	boost::asio::ip::tcp::acceptor _acceptor;
	std::unique_ptr<User> _newUser;	
	std::vector<std::unique_ptr<User>> _users;
};

