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
	//电机类型结构体
	struct HTMotorInfo {
		std::string Type;
		std::string SoftwareVersion;
		std::string HardwarwVersion;
	};
	//电机故障信息
	struct ERRPINFO
	{
		bool VoltagError;
		bool CurrentError;
		bool TempError;
	};
	//电机系统参数
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
		float Angle_speed;		//位置闭环速度
		float Speed_kp;
		float Speed_ki;
		uint8_t power;			//功率百分比
	};
	class HTMOTOR_API_DU HTMotor
	{
	public:
		~HTMotor()= default;
#pragma region 方法接口
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
#pragma region 私用成员
		uint8_t		ID;			//电机ID
		uint16_t	Type;		//电机类型
		uint8_t		Kt;			//扭矩系数

		float Temperature;		//系统温度C
		float Vlotage;			//电压V
		float Current;			//电流mA

		ERRPINFO	ErrInfo;	//故障信息
		std::string StateInfo;	//运行状态

		double LoopAngle;		//多圈角度°
		double OneAngle;		//单圈角度°
		double Speed;			//机械速度rpm
#pragma endregion
#pragma region 私有方法
		void		CRC16(uint8_t * data, size_t size); // CRC16校验
		uint16_t	CRC16(ByteVector &data,size_t size); // CRC16校验
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





