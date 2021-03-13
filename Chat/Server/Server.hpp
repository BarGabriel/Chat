#pragma once
#include "User.hpp"

class Server
{
public:
	Server(boost::asio::io_service& io_service, uint32_t port);

private:
	void startAccept();
	void handleAccept(boost::system::error_code ec);
	void sendBroadcast(std::string message);
	void userDisconnected(User* disconnectedUser);

	uint32_t _port;
	boost::asio::io_service& _io_service;
	boost::asio::ip::tcp::acceptor _acceptor;
	std::unique_ptr<User> _newUser;	
	std::vector<std::unique_ptr<User>> _users;
};

