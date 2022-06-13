#pragma once
#ifdef HTMOTOR_API_DU
#define HTMOTOR_API_DU _declspec(dllimport) 
#else                                                                            
#define HTMOTOR_API_DU _declspec(dllexport) 
#endif    
#include<iostream>
#include<string>
#include<vector>
#include<algorithm>
#include"serial.h"
#include"HTBase.h"


namespace HT {
	//������ͽṹ��
	struct HTMotorInfo {
		std::string Type;
		std::string SoftwareVersion;
		std::string HardwarwVersion;
	};
	//���������Ϣ
	struct ERRPINFO
	{
		bool VoltagError;
		bool CurrentError;
		bool TempError;
	};
	//���ϵͳ����
	struct HTMotorParameter 
	{
		HTMotorParameter() :
			CurrentThreshold(0), 
			VlotageThreshold(0),
			Angle_kp(0),
			Angle_speed(0),
			Speed_kp(0),
			Speed_ki(0),
			power(0) 
		{}
		float CurrentThreshold; //mA
		float VlotageThreshold; //v
		float Angle_kp;
		float Angle_speed;		//λ�ñջ��ٶ�
		float Speed_kp;
		float Speed_ki;
		uint8_t power;			//���ʰٷֱ�
	};
	class HTMOTOR_API_DU HTMotor
	{
	public:
		~HTMotor()= default;
#pragma region �����ӿ�
		HTMotor(unsigned _ID, uint16_t _type);
		HTMotor(const HTMotor &);
		HTMotorInfo			
			GetHTMotorInfo(serial::Serial &);
		HTMotorParameter	
			GetHTMotorParameter(serial::Serial &l);
		HTMotorParameter
			WriteHTMotorParameter(serial::Serial &, const HTMotorParameter &);
		HTMotorParameter
			SaveHTMotorParameter(serial::Serial &, const HTMotorParameter &);
		bool		RestoreExitSetting(serial::Serial &);
		bool		EncoderCalibration(serial::Serial &);
		bool		SetHTMotorOrigin(serial::Serial &);
		bool		ClearError(serial::Serial &);
		bool		Close(serial::Serial &);
		bool		BackToOrigin(serial::Serial &);
		void		TorqueControl(serial::Serial &, int16_t);
		void		SpeedControl(serial::Serial &, int16_t);
		void		AbsolControl(serial::Serial &, double,int16_t);
		void		IncreControl(serial::Serial &, double,int16_t);
		double		angle(serial::Serial &);
		double		single_angle(serial::Serial &);
		double		speed(serial::Serial &);
		void		GetHTMotorRealData(serial::Serial &);
		float		voltage(serial::Serial &);
		float		current(serial::Serial &);
		float		temperture(serial::Serial &);
		std::string state(serial::Serial &);
#pragma endregion


	private:
		HTMotor();
		HTMotor &operator=(const HTMotor &);
#pragma region ˽�ó�Ա
		uint8_t		ID;			//���ID
		uint16_t	Type;		//�������
		uint8_t		Kt;			//Ť��ϵ��

		float Temperature;		//ϵͳ�¶�C
		float Vlotage;			//��ѹV
		float Current;			//����mA

		ERRPINFO	ErrInfo;	//������Ϣ
		std::string StateInfo;	//����״̬

		double LoopAngle;		//��Ȧ�Ƕȡ�
		double OneAngle;		//��Ȧ�Ƕȡ�
		double Speed;			//��е�ٶ�rpm
#pragma endregion
#pragma region ˽�з���
		void		CRC16(uint8_t * data, size_t size); // CRC16У��
		uint16_t	CRC16(ByteVector &data,size_t size); // CRC16У��
		uint8_t*	FloatToBytes(float x);
		float		BytesTofloat(uint8_t *);
		float		BytesTofloat(ByteVector::iterator);
		HTMotorParameter	
			base_HTMotorParameter(uint8_t,serial::Serial &, const HTMotorParameter &);
		void
			GetHTEncoderRealData(serial::Serial &);
		void
			GetHTSystemRealData(serial::Serial &);
		void
			SetMotorSpeed(serial::Serial &,int16_t);
#pragma endregion
	};
}





