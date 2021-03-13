#include "Server.hpp"

Server::Server(boost::asio::io_service& io_service, uint32_t port) :
	_io_service(io_service), _port(port), _acceptor(io_service, boost::asio::ip::tcp::endpoint(boost::asio::ip::tcp::v4(), port))
{
	startAccept();
}

void Server::startAccept()
{
	_newUser = std::make_unique<User>(_io_service, std::bind(&Server::sendBroadcast, this, std::placeholders::_1), std::bind(&Server::userDisconnected, this, std::placeholders::_1));
	_acceptor.async_accept(_newUser->getSocket(), boost::bind(&Server::handleAccept, this, boost::asio::placeholders::error));
}

void Server::handleAccept(boost::system::error_code ec)
{
	if (!ec)
	{
		boost::asio::streambuf buffer;
		boost::asio::read_until(_newUser->getSocket(), buffer, '\0');
		std::istream is(&buffer);
		std::string name;
		std::getline(is, name, '\0');
		_newUser->setName(std::move(name));
		_newUser->doRead();		
		_users.emplace_back(std::move(_newUser));		
	}
	else
	{
		std::cout << ec.message() << std::endl;
	}
	startAccept();
}

void Server::sendBroadcast(std::string message)
{
	std::cout << message << std::endl;

	for each (const auto& user in _users)
	{
		user->write(message);
	}
}

void Server::userDisconnected(User* disconnectedUser)
{
	std::cout << disconnectedUser->getName() << "has disconnected." << std::endl;
	_users.erase(std::remove_if(_users.begin(), _users.end(),
		[&](std::unique_ptr<User>& userPtr) { return (userPtr.get() == disconnectedUser); }));
}
