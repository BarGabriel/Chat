#include "Server.hpp"

Server::Server(boost::asio::io_service& io_service, uint32_t port) :
	_io_service(io_service), _port(port), _acceptor(io_service, boost::asio::ip::tcp::endpoint(boost::asio::ip::tcp::v4(), port))
{
	startAccept();
}

Server::~Server()
{
	for each (User* user in _users)
	{
		delete user;
	}
}

void Server::startAccept()
{
	User* newUser = new User(_io_service);
	_acceptor.async_accept(newUser->getSocket(), boost::bind(&Server::handleAccept, this, std::move(newUser), boost::asio::placeholders::error));
}

void Server::handleAccept(User* newUser, boost::system::error_code ec)
{
	if (!ec)
	{
		boost::asio::streambuf buffer;
		boost::asio::read_until(newUser->getSocket(), buffer, '\0');
		std::string name((std::istreambuf_iterator<char>(&buffer)), std::istreambuf_iterator<char>());
		newUser->setName(name);
		_users.emplace_back(std::move(newUser));
		std::cout << "New User: " << newUser->getName() << std::endl;
	}
	else
	{
		std::cout << ec.message() << std::endl;
	}
	startAccept();
}
