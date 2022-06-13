#pragma once
#ifdef IROBOT_API_DU
#define IROBOT_API_DU _declspec(dllimport) 
#else                                                                            
#define IROBOT_API_DU _declspec(dllexport) 
#endif    

/*
	�ṩ�ֿ�������Ŀ��Ʒ�������Ϣ��ʾ
*/
#include<iostream>
#include<string>
#include<vector>
#include<algorithm>
#include <cmath>
#include"HTMotor.h"
#include"iRobotBase.h"
using HT::HTMotor;

namespace ROBOT {

	struct STATE;
	using StateVector = std::vector<STATE>;
	struct iRobotInfo {
		iRobotInfo(std::string _a, std::string _b, std::string _c, std::string _d):
		Serial_Num(_a), Procotol_type(_b), Hw_type(_c), Ctrl_Num(_d)
		{}
		std::string Serial_Num;
		std::string Procotol_type;
		std::string Hw_type;
		std::string Ctrl_Num;
	};
	struct  ENDPOSITION
	{
		ENDPOSITION() : px(0),py(0),pz(0),roll(0), pitch(0),yaw(0){}
		double px;
		double py;
		double pz;
		double roll;
		double pitch;
		double yaw;
	};
	struct  ROBOTANGLE
	{
		ROBOTANGLE() :
			BaseR(0),
			UarmS(0),
			LarmS(0),
			LarmR(0),
			WristS(0),
			WristR(0)
		{}
		double	BaseR;
		double	UarmS;
		double	LarmS;
		double	LarmR;
		double	WristS;
		double	WristR;
	};
	struct STATE
	{
		STATE() :
			Temperature(0),
			Vlotage(0),
			Current(0)
		{}
		float Temperature;		//ϵͳ�¶�C
		float Vlotage;			//��ѹV
		float Current;			//����mA
		std::string state;		//����״̬
		wchar_t * wstate;		//����״̬forC#
	};
	class IROBOT_API_DU IROBOT
	{
	public:
#pragma region �����ӿ�
		IROBOT(const std::string portname);
		IROBOT(const wchar_t * portname);
		~IROBOT();
		iRobotInfo  GetInfo();
		inline bool isConnect() const { return this->is_connect;}
		inline bool isClose() const { return this->is_close; }
		std::string Connect();	
		wchar_t *  Connect_forCsharrp();
		ROBOTANGLE & Angle();	//��
		ENDPOSITION & Pose();	//mm
		StateVector & State();
		//���ؽ�����\\����
		inline void LockBaseR() { this->is_BaseLock = true;  }
		inline void LockUarmS() { this->is_UarmSLock = true; }
		inline void LockLarmS() { this->is_LarmSLock = true; }
		inline void LockLarmR() { this->is_LarmRLock = true; }
		inline void LockWristR(){ this->is_WristRLock = true;}
		inline void LockWristS(){ this->is_WristSLock = true;}
		inline void UnLockBaseR() { this->is_BaseLock = false; }
		inline void UnLockUarmS() { this->is_UarmSLock = false; }
		inline void UnLockLarmS() { this->is_LarmSLock = false; }
		inline void UnLockLarmR() { this->is_LarmRLock = false; }
		inline void UnLockWristR() { this->is_WristRLock = false; }
		inline void UnLockWristS() { this->is_WristSLock = false; }
		inline void Lock() {
			is_BaseLock = true;
			is_UarmSLock = true;
			is_LarmSLock = true;
			is_LarmRLock = true;
			is_WristRLock = true;
			is_WristSLock = true;
		}
		inline void UnLock() {
			is_BaseLock = false;
			is_UarmSLock = false;
			is_LarmSLock = false;
			is_LarmRLock = false;
			is_WristRLock = false;
			is_WristSLock = false;
		}
		//���ؽ�λ�ÿ���
		void BaseRtoPosition(double, int16_t speed = 0);
		void UarmStoPosition(double, int16_t speed = 0);
		void LarmStoPosition(double, int16_t speed = 0);
		void LarmRtoPosition(double, int16_t speed = 0);
		void WristStoPosition(double, int16_t speed = 0);
		void WristRtoPosition(double, int16_t speed = 0);

		void ToAbsoangle(double *, int16_t speed = 0);
		void ToAbsoangle(std::vector<double>, int16_t speed = 0);
		void ToIncreangle(double *, int16_t speed = 0);
		void ToIncreangle(std::vector<double>, int16_t speed = 0);

		//�����ά��
		void OutputForce(double *);
		void OutputForce(double, double, double, double _mx = 0, double _my = 0, double _mz = 0);
		void OutputForce();
		void BackToOrigin();

		void Close();
#pragma endregion

	private:
		IROBOT();
		IROBOT & operator=(const IROBOT &);
#pragma region ˽�г�Ա
		serial::Serial* m_serial;		// ����
		iRobotInfo m_info;				//�豸��Ϣ
		ROBOTANGLE m_angle;				// �ؽڽ�
		ENDPOSITION m_endpose;			// ĩ��λ��
		StateVector m_state;			// ���״̬
		HTMotor *MotorBaseR;			// �ؽ�6
		HTMotor *MotorUarmS;			// �ؽ�5
		HTMotor *MotorLarmS;			// �ؽ�4
		HTMotor *MotorLarmR;			// �ؽ�3
		HTMotor *MotorWristS;			// �ؽ�2
		HTMotor *MotorWristR;			// �ؽ�1

		bool is_connect;	
		bool is_close;

		//�ؽ�����״̬
		bool is_BaseLock;
		bool is_UarmSLock;
		bool is_LarmSLock;
		bool is_LarmRLock;
		bool is_WristSLock;
		bool is_WristRLock;
#pragma endregion
#pragma region ˽�з���
		void	GetAngle();
		void	GetPosition();
		STATE	GetState(HTMotor &);
		void	base_toangle(double, int16_t, HTMotor &);
		void	base_increangle(double, int16_t, HTMotor &);
		bool	isSame(double * _angle,size_t _size);
		bool	isSame(const std::vector<double> & _angle);
		void Wchar_tToString(std::string& szDst,const wchar_t*wchar);
		wchar_t * char2wchar(const char* cchar);
#pragma endregion

	};
}	 