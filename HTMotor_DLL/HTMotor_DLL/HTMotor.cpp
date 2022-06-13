#include"HTMotor.h"
#include "pch.h"


#define CRC_CCITT 0xA001

#pragma region 私有方法定义
uint8_t* HT::HTMotor::FloatToBytes(float x)
{
	uint8_t data[4];
	unsigned long longdata = *(unsigned long *)&x;
	data[0] = (longdata & 0x000000FF);
	data[1] = (longdata & 0x0000FF00) << 8;
	data[2] = (longdata & 0x00FF0000) << 16;
	data[3] = (longdata & 0xFF000000) << 24;
	return data;
}
float HT::HTMotor::BytesTofloat(uint8_t * p)
{
	float float_data = 0;
	unsigned long longdata = 0;
	longdata = (*(p + 3) << 24) + (*(p + 2) << 16) + (*(p + 1) << 8) + (*p << 0);
	float_data = *(float*)&longdata;
	return float_data;
}
float HT::HTMotor::BytesTofloat(ByteVector::iterator p)
{
	float float_data = 0;
	unsigned long longdata = 0;
	longdata = (*(p + 3) << 24) + (*(p + 2) << 16) + (*(p + 1) << 8) + (*p << 0);
	float_data = *(float*)&longdata;
	return float_data;
}
//
// data[0,size)的crc16校验，校验值追加在末尾（低位在前）
//
void HT::HTMotor::CRC16(uint8_t * data, size_t size) { 
	uint16_t crc = 0xffff;
	for (size_t i = 0; i < size; i++)
	{
		crc ^= (uint16_t)data[i];
		for (int j = 0; j < 8; j++)
		{
			if (crc & 0x0001) {
				crc = crc >> 1;
				crc ^= CRC_CCITT;
			}
			else {
				crc = crc >> 1;
			}
		}
	}
	*(data + size) = LOWEIGHTBITS(crc);
	*(data + size + 1) = HIGHEIGHTBITS(crc);
}
uint16_t HT::HTMotor::CRC16(ByteVector & data, size_t size)
{
	uint16_t crc = 0xffff;
	for (size_t i = 0; i < size; i++)
	{
		crc ^= (uint16_t)data[i];
		for (int j = 0; j < 8; j++)
		{
			if (crc & 0x0001) {
				crc = crc >> 1;
				crc ^= 0xA001;
			}
			else {
				crc = crc >> 1;
			}
		}
	}
	return crc;
}
HT::HTMotorParameter HT::HTMotor::base_HTMotorParameter(uint8_t _code, serial::Serial & serial, const HTMotorParameter & hTMotorParameter = HTMotorParameter())
{
	HTMotorParameter _returnstruct;
	if (_code == HTR_MOTORPARA) {
		const unsigned SendLength = BASELENGTH + SENDLEN_R_PARAMETER;
		uint8_t data[SendLength + CRC16LENGTH] = {
			HT_MASTERHEAD,
			PACKGEINDEX(0X02),
			this->ID,
			HTR_MOTORPARA,
			SENDLEN_R_PARAMETER,
			0x00,0x00
		};
		this->CRC16(data, SendLength);
		serial.write(data, SendLength + CRC16LENGTH);
	}
	else {
		const unsigned SendLength = BASELENGTH + SENDLEN_W_PARAMETER;
		uint8_t*	anglekp = FloatToBytes(hTMotorParameter.Angle_kp);
		uint8_t*	anglespeed = FloatToBytes(hTMotorParameter.Angle_speed);
		uint8_t*	speedkp = FloatToBytes(hTMotorParameter.Speed_kp);
		uint8_t*	speedki = FloatToBytes(hTMotorParameter.Speed_ki);
		uint8_t		data[SendLength + CRC16LENGTH] = {
			HT_MASTERHEAD,
			PACKGEINDEX(0X01),
			this->ID,
			_code,
			SENDLEN_W_PARAMETER,
			this->ID,
			hTMotorParameter.CurrentThreshold / 30.0f,
			hTMotorParameter.VlotageThreshold /	0.2,
			0,
			anglekp[0],anglekp[1],anglekp[2],anglekp[3],
			anglespeed[0],anglespeed[1],anglespeed[2],anglespeed[3],
			speedkp[0],speedkp[1],speedkp[2],speedkp[3],
			speedki[0],speedki[1],speedki[2],speedki[3],
			0,0,0,0,0x10,
			0x00,0x00
		};
		this->CRC16(data, SendLength);
		serial.write(data, SendLength + CRC16LENGTH);
	}

	const unsigned ReadLength = BASELENGTH + READLEN_PARAMETER;
	std::vector<uint8_t> temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	if (temp.size() < ReadLength + CRC16LENGTH
		|| temp[0] != HT_SLAVEHEAD
		|| temp[2] != this->ID
		|| temp[3] != _code)
		return _returnstruct;

	uint16_t Recrc16 = this->CRC16(temp, ReadLength);

	if (temp[ReadLength] != LOWEIGHTBITS(Recrc16)
		|| temp[ReadLength + 1] != HIGHEIGHTBITS(Recrc16))
		return _returnstruct;

	_returnstruct.CurrentThreshold = temp[6] * 30.0f;
	_returnstruct.VlotageThreshold = temp[7] * 0.2f;
	_returnstruct.Angle_kp =	BytesTofloat(temp.begin() + 9);
	_returnstruct.Angle_speed = BytesTofloat(temp.begin() + 13);
	_returnstruct.Speed_kp =	BytesTofloat(temp.begin() + 17);
	_returnstruct.Speed_ki =	BytesTofloat(temp.begin() + 21);
	_returnstruct.power = temp[30];
#ifdef DEBUG
	DEBUG(_returnstruct.CurrentThreshold); DEBUG(_returnstruct.VlotageThreshold);
	DEBUG(_returnstruct.Angle_kp); DEBUG(_returnstruct.Angle_speed);
	DEBUG(_returnstruct.Speed_kp); DEBUG(_returnstruct.Speed_ki);
#endif // DEBUG

	return _returnstruct;
}
//
//	获取电机实时数据（单圈角度、多圈角度、速度、电源电压、系统电流、温度、故障代码）
//
void HT::HTMotor::GetHTMotorRealData(serial::Serial & serial)
{
	const unsigned SendLength = BASELENGTH + SENDLEN_MOTORDATA;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x01),
		this->ID,
		HTR_MOTORDATA,
		SENDLEN_MOTORDATA,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const unsigned ReadLength = BASELENGTH + READLEN_MOTORDATA;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	if (temp[0] != HT_SLAVEHEAD
		|| temp[2] != this->ID
		|| temp[3] != HTR_MOTORDATA)
		return;

	uint16_t Recrc16 = this->CRC16(temp, ReadLength);
	if (temp.size() < ReadLength + CRC16LENGTH
		|| temp[ReadLength] != LOWEIGHTBITS(Recrc16)
		|| temp[ReadLength + 1] != HIGHEIGHTBITS(Recrc16))
		return;

	uint16_t tempangle = TOUINT16_T(temp[5], temp[6]);
	this->OneAngle = ((double(tempangle) * 360.0) / ENCODERCOUNT);

	int32_t temploopangle = TOINT32_T(temp[7], temp[8], temp[9], temp[10]);
	this->LoopAngle = (double(temploopangle) * 360.0) / ENCODERCOUNT;

	int16_t tempspeed = TOUINT16_T(temp[11], temp[12]);
	this->Speed = 0.1 * (double)tempspeed;

	this->Vlotage = 0.2f * (float)temp[13];
	this->Current = 30.0f * (float)temp[14];
	this->Temperature = 0.4f * (float)temp[15];

	this->ErrInfo.VoltagError = (ZEROBIT(temp[16]) == 1);
	this->ErrInfo.CurrentError = (FIRSTBIT(temp[16]) == 1);
	this->ErrInfo.TempError = (SECONDEBIT(temp[16]) == 1);

	switch (temp[17]) {
	case 0:
		this->StateInfo = MotorClosed; break;
	case 1:
		this->StateInfo = MotorTorque; break;
	case 3:
		this->StateInfo = MotorSpeed; break;
	case 5:
		this->StateInfo = MotorAngle; break;
	default:
		break;
	}
#ifdef DEBUG
	DEBUG(OneAngle); DEBUG(LoopAngle); DEBUG(Speed);
	DEBUG(Vlotage); DEBUG(Current); DEBUG(Temperature);
	DEBUG(StateInfo);
#endif // DEBUG
	return;
}
//
// 获取编码器单圈角度、多圈角度、机械速度
//
void HT::HTMotor::GetHTEncoderRealData(serial::Serial &serial)
{
	const unsigned SendLength = BASELENGTH + SENDLEN_ENCODEDATA;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x05),
		this->ID,
		HTR_MOTORANGLESPED,
		SENDLEN_ENCODEDATA,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const unsigned ReadLength = BASELENGTH + READLEN_ENCODEDATA;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	if (temp.size() < ReadLength + CRC16LENGTH
		|| temp[0] != HT_SLAVEHEAD
		|| temp[2] != this->ID
		|| temp[3] != HTR_MOTORANGLESPED)
		return;

	uint16_t Recrc16 = this->CRC16(temp, ReadLength);
	if (temp.size() < ReadLength + CRC16LENGTH 
		|| temp[ReadLength] != LOWEIGHTBITS(Recrc16)
		|| temp[ReadLength + 1] != HIGHEIGHTBITS(Recrc16))
		return;

	uint16_t tempangle = TOUINT16_T(temp[5], temp[6]);
	this->OneAngle = ((double(tempangle) * 360.0) / ENCODERCOUNT);

	int32_t temploopangle = TOINT32_T(temp[7], temp[8], temp[9], temp[10]);
	this->LoopAngle = (double(temploopangle) * 360.0) / ENCODERCOUNT;

	int16_t tempspeed = TOUINT16_T(temp[11], temp[12]);
	this->Speed = 0.1 * (double)tempspeed;

	return;
}
//
//	获取电机实时状态信息（电压、电流、温度、故障码）
//
void HT::HTMotor::GetHTSystemRealData(serial::Serial &serial)
{
	const unsigned SendLength = BASELENGTH + SENDLEN_SYSTEMDATA;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x06),
		this->ID,
		HTR_SYSTEMDATA,
		SENDLEN_SYSTEMDATA,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const unsigned ReadLength = BASELENGTH + READLEN_SYSTEMDATA;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	if (temp.size() < ReadLength + CRC16LENGTH
		|| temp[0] != HT_SLAVEHEAD
		|| temp[2] != this->ID
		|| temp[3] != HTR_SYSTEMDATA)
		return;

	uint16_t Recrc16 = this->CRC16(temp, ReadLength);
	if (temp.size() < ReadLength + CRC16LENGTH 
		|| temp[ReadLength] != LOWEIGHTBITS(Recrc16)
		|| temp[ReadLength + 1] != HIGHEIGHTBITS(Recrc16))
		return;

	this->Vlotage = 0.2f * (float)temp[5];
	this->Current = 30.0f * (float)temp[6];
	this->Temperature = 0.4f * (float)temp[7];

	this->ErrInfo.VoltagError = (ZEROBIT(temp[8]) == 1);
	this->ErrInfo.CurrentError = (FIRSTBIT(temp[8]) == 1);
	this->ErrInfo.TempError = (SECONDEBIT(temp[8]) == 1);

	switch (temp[9]) {
	case 0:
		this->StateInfo = MotorClosed; break;
	case 1:
		this->StateInfo = MotorTorque; break;
	case 3:
		this->StateInfo = MotorSpeed; break;
	case 5:
		this->StateInfo = MotorAngle; break;
	default:
		break;
	}
}
//
// 设置电机转速 rpm
//
void HT::HTMotor::SetMotorSpeed(serial::Serial & serial, int16_t _speed)
{
	int16_t speed = _speed * 10;
	const unsigned SendLength = BASELENGTH + SENDLEN_ANGLESPEED;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x06),
		this->ID,
		HTW_ANGLESPEED,
		SENDLEN_ANGLESPEED,
		0x01,
		LOWEIGHTBITS(speed),
		HIGHEIGHTBITS(speed),
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const unsigned ReadLength = BASELENGTH + READLEN_ANGLESPEED;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);
}
#pragma endregion

// 
// 构造函数
//
HT::HTMotor::HTMotor(unsigned _ID, uint16_t _type):
	ID(_ID),Type(_type)
{
	if (_type == DM_R_6015)
		this->Kt = 21;
	else if (_type == DM_R_4315)
		this->Kt = 62;
	else
		this->Kt = 0;
	HTMotor();
}
HT::HTMotor::HTMotor(const HTMotor & other)
{
	this->ID = other.ID;
	this->Type = other.Type;
	this->Kt = other.Kt;
	HTMotor();
}
HT::HTMotor::HTMotor() 
{
	this->Current = 0.0f;
	this->Vlotage = 0.0f;
	this->Temperature = 0.0f;
	
	this->Speed = 0.0f;
	this->LoopAngle = 0.0;
	this->OneAngle = 0.0;
	this->StateInfo = MotorClosed;
	this->ErrInfo.CurrentError = false;
	this->ErrInfo.TempError = false;
	this->ErrInfo.CurrentError = false;

}
//
//	获取电机的型号、软件版本、硬件版本
//
HT::HTMotorInfo HT::HTMotor::GetHTMotorInfo(serial::Serial & serial)
{
	const unsigned SendLength = BASELENGTH + SENDLEN_MOTORINFO ; //基础长度 + 协议数据包长度 + 校验长度 
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x01),
		this->ID,
		HTR_MOTORINFO,
		SENDLEN_MOTORINFO,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const unsigned ReadLength = BASELENGTH + READLEN_MOTORINFO;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	if (temp.size() < ReadLength + CRC16LENGTH
		|| temp[0] != HT_SLAVEHEAD 
		|| temp[2] != this->ID 
		|| temp[3] != HTR_MOTORINFO)
		return HTMotorInfo();

	uint16_t Recrc16 = this->CRC16(temp, ReadLength);
	if(temp[ReadLength + 0] != LOWEIGHTBITS(Recrc16)|| temp[ReadLength + 1] != HIGHEIGHTBITS(Recrc16))
		return HTMotorInfo();
	
	//debug
	//std::cout <<"命令码"<< std::hex << unsigned int(temp[3])
	//			<<"ID"<<std::hex << unsigned int(temp[2]);
	
	HTMotorInfo info;
	info.Type = "DM_R_" + std::to_string(TOUINT16_T(temp[5],temp[6]));
	info.SoftwareVersion = std::to_string(TOUINT16_T(temp[9], temp[10]));
	info.HardwarwVersion = std::to_string(temp[7]);

	return info;
}

//
// 获取电机中保存的系统参数
//
HT::HTMotorParameter 
HT::HTMotor::GetHTMotorParameter(serial::Serial & serial)
{
	return this->base_HTMotorParameter(HTR_MOTORPARA, serial);
}
//
//	 写入系统参数,断电失效
//
HT::HTMotorParameter 
HT::HTMotor::WriteHTMotorParameter(serial::Serial & serial, const HTMotorParameter & hTMotorParameter)
{
	return this->base_HTMotorParameter(HTW_MOTORPARA_V0LATILE, serial, hTMotorParameter);
}
//
//	保存系统参数，断电保存
//
HT::HTMotorParameter
HT::HTMotor::SaveHTMotorParameter(serial::Serial & serial, const HTMotorParameter & hTMotorParameter)
{
	return this->base_HTMotorParameter(HTW_MOTORPARA_STABLE, serial, hTMotorParameter);
}
//
// 恢复出厂设置
//
bool HT::HTMotor::RestoreExitSetting(serial::Serial & serial)
{
	const uint8_t SendLength = BASELENGTH + SENDLEN_RESTORE;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x01),
		this->ID,
		HTW_MOTORRESET,
		SENDLEN_RESTORE,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_PARAMETER;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	return temp.size() == ReadLength + CRC16LENGTH && temp[3] == HTW_MOTORRESET;
}
//
//	校准电机编码器
//
bool HT::HTMotor::EncoderCalibration(serial::Serial &serial)
{
	const uint8_t SendLength = BASELENGTH + SENDLEN_CALIBRATE;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x03),
		this->ID,
		HTW_MOTORCALIBRATE,
		SENDLEN_CALIBRATE,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_CALIBRATE;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	return temp.size() == ReadLength + CRC16LENGTH && temp[3] == HTW_MOTORCALIBRATE;
}
//
//	设置电机当前位置为零点
//
bool HT::HTMotor::SetHTMotorOrigin(serial::Serial & serial)
{
	const uint8_t SendLength = BASELENGTH + SENDLEN_SETORIGIN;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_MOTORSETORIGIN,
		SENDLEN_SETORIGIN,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_SETORIGIN;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	return temp.size() == ReadLength + CRC16LENGTH && temp[7] == 0X01;
}
//
//	 返回电机的多圈角度
//
double HT::HTMotor::angle(serial::Serial & serial)	
{
	this->GetHTEncoderRealData(serial);
	return this->LoopAngle;
}
//
//	 返回电机的单圈角度
//
double	HT::HTMotor::single_angle(serial::Serial &serial)
{
	this->GetHTEncoderRealData(serial);
	return this->OneAngle;
}
//
//	 返回电机的机械速度
//
double HT::HTMotor::speed(serial::Serial &serial)
{
	return this->Speed;
}
//
//	 返回电机的电压
//
float HT::HTMotor::voltage(serial::Serial &serial)
{
	return this->Vlotage;
}
//
//	 返回电机的电流
//
float HT::HTMotor::current(serial::Serial &serial)
{
	return this->Current;
}
//
//	 返回电机的温度
//
float HT::HTMotor::temperture(serial::Serial &serial)
{
	return this->Temperature;
}
//
//	 返回电机的运行状态
//
std::string HT::HTMotor::state(serial::Serial &serial)
{
	return this->StateInfo;
}
//
//	清除故障信息
//
bool HT::HTMotor::ClearError(serial::Serial &serial)
{
	const uint8_t SendLength = BASELENGTH + SENDLEN_CLEARERROR;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_CLEARERRO,
		SENDLEN_CLEARERROR,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_CLEARERROR;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	return temp.size() == ReadLength + CRC16LENGTH && temp[3] == HTW_CLEARERRO;
}
//
//	关闭电机
//
bool HT::HTMotor::Close(serial::Serial & serial)
{
	const uint8_t SendLength = BASELENGTH + SENDLEN_CLOSEMOTOR;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_MOTORCLOSE,
		SENDLEN_CLOSEMOTOR,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_CLOSEMOTOR;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	return temp.size() == ReadLength + CRC16LENGTH && temp[3] == HTW_MOTORCLOSE;
}
//
//	回到电机原点
//
bool HT::HTMotor::BackToOrigin(serial::Serial &serial)
{
	const uint8_t SendLength = BASELENGTH + SENDLEN_BACKORIGIN;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_BACKORIGIN_SHORSET,
		SENDLEN_BACKORIGIN,
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_BACKORIGIN;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);

	return temp.size() == ReadLength + CRC16LENGTH && temp[3] == HTW_BACKORIGIN_SHORSET;
}
//
//	输出力矩mNm
//
void HT::HTMotor::TorqueControl(serial::Serial & serial, int16_t _torque)
{
	int16_t power = _torque * this->Kt;
	const uint8_t SendLength = BASELENGTH + SENDLEN_TORQUECTRL;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_TORQUECTRL,
		SENDLEN_TORQUECTRL,
		LOWEIGHTBITS(power),
		HIGHEIGHTBITS(power),
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_TORQUECTRL;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);
	return;
}
//
//		输出速度RPM
//
void HT::HTMotor::SpeedControl(serial::Serial & serial, int16_t _speed)
{
	int16_t speed = _speed * 10.0;
	const uint8_t SendLength = BASELENGTH + SENDLEN_SPEEDCTRL;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_SPEEDCTRL,
		SENDLEN_SPEEDCTRL,
		LOWEIGHTBITS(speed),
		HIGHEIGHTBITS(speed),
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_SPEEDCTRL;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);
	return;
}
//
//`	输出绝对位置°
//
void HT::HTMotor::AbsolControl(serial::Serial & serial,  double _angle, int16_t speed = 0)
{
	if (speed != 0)
		this->SetMotorSpeed(serial,speed);
	uint32_t angle = (uint32_t)(_angle * (ENCODERCOUNT / 360.0));
	const uint8_t SendLength = BASELENGTH + SENDLEN_ABSOLCTRL;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_ABSOLCTRL,
		SENDLEN_ABSOLCTRL,
		LOWEIGHTBITS(angle),
		HIGHEIGHTBITS(angle),
		HIGHEIGHTBITS_2(angle),
		HIGHEIGHTBITS_3(angle),
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_ABSOLCTRL;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);
	return;
}
//
//`	输出增量位置
//
void HT::HTMotor::IncreControl(serial::Serial & serial, double _angle, int16_t speed = 0)
{
	if (speed != 0)
		this->SetMotorSpeed(serial, speed);
	int16_t angle = (int16_t)(_angle * (ENCODERCOUNT / 360.0));
	const uint8_t SendLength = BASELENGTH + SENDLEN_INCRECTRL;
	uint8_t data[SendLength + CRC16LENGTH] = {
		HT_MASTERHEAD,
		PACKGEINDEX(0x04),
		this->ID,
		HTW_INCRECTRL,
		SENDLEN_INCRECTRL,
		LOWEIGHTBITS(angle),
		HIGHEIGHTBITS(angle),
		0x00,0x00
	};
	this->CRC16(data, SendLength);
	serial.write(data, SendLength + CRC16LENGTH);

	const uint8_t ReadLength = BASELENGTH + READLEN_INCRECTRL;
	ByteVector temp;
	serial.read(temp, ReadLength + CRC16LENGTH);
	return;

}