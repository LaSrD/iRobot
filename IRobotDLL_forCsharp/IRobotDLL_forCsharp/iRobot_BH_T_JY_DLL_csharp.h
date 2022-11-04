#pragma once


#include "DllforIRobot_BH_T_JY.h"
#include <windows.h>
#include <string>
#include <vcclr.h>
using namespace System;
namespace IRobotDLLforCsharp {
	public ref class IROBOT
	{
		// TODO: 在此处为此类添加方法。
	public:
#pragma region 属性
		String ^ Serial_Num;
		String ^ Procotol_Type;
		String ^ HW_Type;
		String ^ Ctrl_Num;
		double BaseRangle;
		double UarmSangle;
		// double LarmSangle;
		double LarmRangle;
		double WristSangle;
		double WristRangle;
		double px;
		double py;
		double pz;
		double pitch;
		double yaw;
		double roll;
		array<float> ^ Temperature;
		array<float> ^ Current;
		array<float> ^ Vlotage;
		array<String ^> ^Motorstate;
#pragma endregion
		IROBOT(String ^ _portname); 
		~IROBOT();				  
		void Info();
		bool isConnect();
		bool isClose();
		String ^ Connect();
		void Angle();	//°
		void Pose();	//mm
		void State();
		inline void LockBaseR();
		inline void LockUarmS();
		 // inline void LockLarmS();
		inline void LockLarmR();
		inline void LockWristR();
		inline void LockWristS();
		inline void UnLockBaseR();
		inline void UnLockUarmS();
		 // inline void UnLockLarmS();
		inline void UnLockLarmR();
		inline void UnLockWristR();
		inline void UnLockWristS();
		inline void Lock();
		inline void UnLock();
		void BaseRtoPosition(double, int16_t speed);
		void UarmStoPosition(double, int16_t speed);
		 // void LarmStoPosition(double, int16_t speed);
		void LarmRtoPosition(double, int16_t speed);
		void WristStoPosition(double, int16_t speed);
		void WristRtoPosition(double, int16_t speed);
		void ToAbsoangle(double *, int16_t speed);
		void ToIncreangle(double *, int16_t speed);
		void OutputForce(double, double, double, double, double, double);
		void OutputForce();
		void BackToOrigin();
		void Close();
		// 按键
		String ^ConnectToHandle(String ^ _portname);
		bool isHandleConnect();
		String ^ GetHandleInfo();
		void CloseHandle();
	private:
		ROBOT::IROBOT * m_robot;
		wchar_t * char2wchar(const char* cchar);
	};
}
