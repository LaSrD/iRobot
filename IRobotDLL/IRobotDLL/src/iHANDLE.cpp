#include"pch.h"
#include"iHANDLE.h"


ROBOT::iHANDLE::iHANDLE(const std::string _portname)
{
	m_serial = new serial::Serial(_portname,9600U);
	is_connect = false;
}
ROBOT::iHANDLE::~iHANDLE()
{
	if (m_serial != nullptr)
		delete m_serial;
	m_serial = nullptr;
}
// 连接手柄
std::string ROBOT::iHANDLE::Connect()
{
	std::string errormsg;
	try {
		this->m_serial->open();
	}
	catch (const std::exception & error) {
		errormsg = error.what();
	}
	if (errormsg.empty())
		this->is_connect = true;
	return errormsg;
}
// 连接状态
bool ROBOT::iHANDLE::isConnect()
{
	return this->m_serial->isOpen();
}
// 获取按键信息
std::string ROBOT::iHANDLE::Getinfo()
{
	if (!this->is_connect)
		return std::string();
	return m_serial->read();
}
void ROBOT::iHANDLE::Close()
{
	if (isConnect())
		this->m_serial->close();
}