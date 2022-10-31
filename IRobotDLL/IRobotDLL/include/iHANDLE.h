#pragma once
#include"serial.h"
#include<string>
namespace ROBOT {
	class iHANDLE
	{
	public:
		iHANDLE(const std::string _portname);
		~iHANDLE();
		std::string Connect();  //连接按键手柄
		bool isConnect();		// 连接状态
		std::string Getinfo();	// 获取按键信息
		void Close(); 
	private:
		iHANDLE();
		serial::Serial* m_serial;		// 通信串口
		bool is_connect;
	};
}