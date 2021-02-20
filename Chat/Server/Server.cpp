#include "Server.hpp"

Server::Server(boost::asio::io_service& io_service, uint32_t port) : 
	_io_service(io_service), _port(port), _acceptor(_io_service, boost::asio::ip::tcp::endpoint(boost::asio::ip::tcp::v4(), _port))
{
	startAccept();
}

Server::~Server()
{
}

void Server::startAccept()
{	
	boost::asio::ip::tcp::socket sock(_io_service);	
	_acceptor.async_accept(sock, [&](boost::system::error_code ec) {
		if (!ec)
		{
			boost::asio::streambuf buffer;
			boost::asio::read_until(sock,buffer,'\0');
			std::string name((std::istreambuf_iterator<char>(&buffer)), std::istreambuf_iterator<char>());
			User newUser(std::move(sock), name);
			_users.emplace_back(newUser);
		}
		startAccept();
	});
}
