#include "User.hpp"

User::User(boost::asio::io_service& io_service) : _socket(io_service)
{
}

User::~User()
{
}
