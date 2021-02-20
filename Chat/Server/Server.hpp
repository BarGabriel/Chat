#pragma once
#include <vector>

#include <boost/asio.hpp>
#include <boost/bind.hpp>

#include "User.hpp"

class Server
{
public:
	Server(boost::asio::io_service& io_service, uint32_t port);
	~Server();

private:
	void startAccept();
	void handleAccept(User* newUser, boost::system::error_code ec);

	boost::asio::io_service& _io_service;
	boost::asio::ip::tcp::acceptor _acceptor;

	uint32_t _port;

	std::vector<User*> _users;
};

