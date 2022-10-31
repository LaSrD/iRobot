#pragma once
#include"serial.h"
#include<string>
namespace ROBOT {
	class iHANDLE
	{
	public:
		iHANDLE(const std::string _portname);
		~iHANDLE();
		std::string Connect();  //���Ӱ����ֱ�
		bool isConnect();		// ����״̬
		std::string Getinfo();	// ��ȡ������Ϣ
		void Close(); 
	private:
		iHANDLE();
		serial::Serial* m_serial;		// ͨ�Ŵ���
		bool is_connect;
	};
}