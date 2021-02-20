#include "User.hpp"

User::User(boost::asio::ip::tcp::socket socket, std::string& name) : _socket(std::move(socket)), _name(name)
{
}

User::User(User& user) : _socket(std::move(user.getSocket())), _name(user.getName())
{
}

User::~User()
{
}
