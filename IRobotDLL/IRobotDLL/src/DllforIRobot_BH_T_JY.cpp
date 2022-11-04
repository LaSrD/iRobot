#include"pch.h"
#include"DllforIRobot_BH_T_JY.h"


//
//	构造函数
//
ROBOT::IROBOT::IROBOT(const std::string _portname) :IROBOT()
{
	this->m_serial = new serial::Serial(_portname, 115200U);
}
ROBOT::IROBOT::IROBOT(const wchar_t * portname) : IROBOT()
{
	std::string temp;
	Wchar_tToString(temp, portname);
	this->m_serial = new serial::Serial(temp, 115200U);
}
//
//默认构造函数
//
ROBOT::IROBOT::IROBOT(): 
	m_info(SERIALNUM, PROCOTOL, HWTYPE, CTRLNUM),
	is_connect(false),
	is_handleconnect(false),
	is_close(true),
	is_BaseLock(false),
	is_UarmSLock(false),
	 // is_LarmSLock(false),
	is_LarmRLock(false),
	is_WristSLock(false),
	is_WristRLock(false),
	m_endpose(), 
	m_angle(), 
	m_state(StateVector(6))
{
	this->MotorBaseR = new HTMotor(ID_BASER, DM_R_6015);
	this->MotorUarmS = new HTMotor(ID_UARMS, DM_R_6015);
	//this->MotorLarmS = new HTMotor(ID_LARMS, DM_R_6015);  Jy版本的没有小臂摆动电机，20221103改
	this->MotorLarmR = new HTMotor(ID_LARMR, DM_R_4315);
	this->MotorWristS = new HTMotor(ID_WRISTS, DM_R_4315);
	this->MotorWristR = new HTMotor(ID_WRISTR, DM_R_4315);
}
ROBOT::IROBOT::~IROBOT()
{
	if (m_serial != nullptr)
		delete m_serial;
	if (MotorBaseR)
		delete MotorBaseR;
	if (MotorUarmS)
		delete MotorUarmS;
	// if (MotorLarmS)			
		// delete MotorLarmS;  //Jy版本的没有小臂摆动电机，20221103改
	if (MotorLarmR)
		delete MotorLarmR;
	if (MotorWristS)
		delete MotorWristS;
	if (MotorWristR)
		delete MotorWristR;
	if (m_handle)
		delete m_handle;

	m_serial = nullptr;
	MotorBaseR = nullptr;
	MotorUarmS = nullptr;
	// MotorLarmS = nullptr; //Jy版本的没有小臂摆动电机，20221103改
	MotorLarmR = nullptr;
	MotorWristS = nullptr;
	MotorWristR = nullptr;
	m_handle = nullptr;
}
//
// 返回设备的信息
//
ROBOT::iRobotInfo  ROBOT::IROBOT::GetInfo()
{
	return this->m_info;
}
//
//	连接至设备
//	返回: empty | 错误信息
//
std::string ROBOT::IROBOT::Connect()
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
wchar_t *   ROBOT::IROBOT::Connect_forCsharrp()
{
	wchar_t * ptr = nullptr;
	try {	
		this->m_serial->open();
	}
	catch (const std::exception & error) {
		ptr = char2wchar(error.what());
	}
	if (ptr == nullptr)
		this->is_connect = true;
	return ptr;
}
//
//	返回手控器关节角
//
ROBOT::ROBOTANGLE & ROBOT::IROBOT::Angle()
{
	GetAngle();
	return this->m_angle;
}
//
//	返回末端姿态
//
ROBOT::ENDPOSITION & ROBOT::IROBOT::Pose()
{
	GetPosition();
	return this->m_endpose;
}
//
//	
//
ROBOT::StateVector & ROBOT::IROBOT::State()
{
	this->m_state[0] = GetState(*MotorWristS);
	this->m_state[1] = GetState(*MotorWristR);
	this->m_state[2] = GetState(*MotorLarmR);
	// this->m_state[3] = GetState(*MotorLarmS); //Jy版本的没有小臂摆动电机，20221103改
	this->m_state[4] = GetState(*MotorUarmS);
	this->m_state[5] = GetState(*MotorBaseR);
	return this->m_state;
}
//
//	控制电机在指定位置
//
void ROBOT::IROBOT::BaseRtoPosition(double _angle,int16_t _speed)
{
	base_toangle(_angle, _speed, *MotorBaseR);
}
void ROBOT::IROBOT::UarmStoPosition(double _angle, int16_t _speed)
{
	base_toangle(_angle, _speed, *MotorUarmS);
}
//void ROBOT::IROBOT::LarmStoPosition(double _angle,int16_t _speed)
//{
//	// base_toangle(_angle, _speed, *MotorLarmS); //Jy版本的没有小臂摆动电机，20221103改
//}
void ROBOT::IROBOT::LarmRtoPosition(double _angle,int16_t _speed)
{
	base_toangle(_angle, _speed, *MotorLarmR);
}
void ROBOT::IROBOT::WristStoPosition(double _angle,int16_t _speed)
{
	base_toangle(_angle, _speed, *MotorWristS);
}
void ROBOT::IROBOT::WristRtoPosition(double _angle,int16_t _speed)
{
	base_toangle(_angle, _speed, *MotorWristR);
}
//
//	控制手控器到绝对位置（D-H建模的关节角度）
//
void ROBOT::IROBOT::ToAbsoangle(double * _angle, int16_t speed )
{
	// 关节角映射到电机转角。
	// 20221013 待添加关节角映射到电机转角

	BaseRtoPosition(_angle[4],speed);
	UarmStoPosition(_angle[3], speed);
	 // LarmStoPosition(_angle[3], speed);
	LarmRtoPosition(_angle[2], speed);
	WristStoPosition(_angle[1], speed);
	WristRtoPosition(_angle[0], speed);
}
void ROBOT::IROBOT::ToAbsoangle(std::vector<double> _angle, int16_t speed)
{
	BaseRtoPosition(_angle[4], speed);
	UarmStoPosition(_angle[3], speed);
	 // LarmStoPosition(_angle[3], speed);
	LarmRtoPosition(_angle[2], speed);
	WristStoPosition(_angle[1], speed);
	WristRtoPosition(_angle[0], speed);
}
void ROBOT::IROBOT::ToIncreangle(double * _angle, int16_t speed)
{
	base_increangle(_angle[4], speed, *MotorBaseR);
	base_increangle(_angle[3], speed, *MotorUarmS);
	// base_increangle(_angle[3], speed, *MotorLarmS);
	base_increangle(_angle[2], speed, *MotorLarmR);
	base_increangle(_angle[1], speed, *MotorWristS);
	base_increangle(_angle[0], speed, *MotorWristR);
}
void ROBOT::IROBOT::ToIncreangle(std::vector<double>_angle, int16_t speed)
{
	base_increangle(_angle[4], speed, *MotorBaseR);
	base_increangle(_angle[3], speed, *MotorUarmS);
	// base_increangle(_angle[3], speed, *MotorLarmS);
	base_increangle(_angle[2], speed, *MotorLarmR);
	base_increangle(_angle[1], speed, *MotorWristS);
	base_increangle(_angle[0], speed, *MotorWristR);
}
void ROBOT::IROBOT::OutputForce(double * _force)
{
	this->GetAngle();

	double theta1 = this->m_angle.BaseR * PI / 180.0;
	double theta2 = this->m_angle.UarmS * PI / 180.0;
	double theta3 = this->m_angle.LarmR * PI / 180.0;
	double theta4 = this->m_angle.WristS * PI / 180.0;
	double theta5 = this->m_angle.WristR * PI / 180.0;

	double c1 = std::cos(theta1), c2 = std::cos(theta2), c3 = std::cos(theta3), c4 = std::cos(theta4), c5 = std::cos(theta5);
	double s1 = std::sin(theta1), s2 = std::sin(theta2), s3 = std::sin(theta3), s4 = std::sin(theta4), s5 = std::sin(theta5);

	// 1104 更改

	// 重力补偿输出力矩
	double Torque_1 = 0;

	double Torque_2 = s3 * (GLODO*H3*M3*s2*s3 - c4 * GLODO*H4*M4*s2*s3) - c3 * (GLODO*H4*M4*(c2*s4 + c3 * c4*s2) - c3 * GLODO*H3*M3*s2) - D3 * (s3*(GLODO*M3*s2*s3 + GLODO * M4*s2*s3) + c3 * (c3*GLODO*M3*s2 + c4 * GLODO*M4*(c2*s4 + c3 * c4*s2) - GLODO * M4*s4*(c2*c4 - c3 * s2*s4))) - GLODO * H2*M2*s2;
	
	double Torque_3 = -GLODO * H4 * M4 * s2 * s3 * s4;
	double Torque_4 = - GLODO * H4 * M4 *(c2 * s4 + c3 * c4*s2);
	double Torque_5 = 0.0;
	
	//以上不包含外力

	double fx_wcs = _force[0];
	double fy_wcs = _force[1];
	double fz_wcs = _force[2];
	double mx_wcs = _force[3];
	double my_wcs = _force[4];
	double mz_wcs = _force[5];
	double fx = fz_wcs * (c5*(c2*s4 + c3 * c4*s2) - s2 * s3*s5) - fx_wcs * (c5*(c4*(s1*s3 - c1 * c2*c3) + c1 * s2*s4) + s5 * (c3*s1 + c1 * c2*s3)) + fy_wcs * (c5*(c4*(c1*s3 + c2 * c3*s1) - s1 * s2*s4) + s5 * (c1*c3 - c2 * s1*s3)); 
	double fy = fx_wcs * (s5*(c4*(s1*s3 - c1 * c2*c3) + c1 * s2*s4) - c5 * (c3*s1 + c1 * c2*s3)) - fz_wcs * (s5*(c2*s4 + c3 * c4*s2) + c5 * s2*s3) - fy_wcs * (s5*(c4*(c1*s3 + c2 * c3*s1) - s1 * s2*s4) - c5 * (c1*c3 - c2 * s1*s3));
	double fz = fx_wcs * (s4*(s1*s3 - c1 * c2*c3) - c1 * c4*s2) - fy_wcs * (s4*(c1*s3 + c2 * c3*s1) + c4 * s1*s2) + fz_wcs * (c2*c4 - c3 * s2*s4);

	double mx = mz_wcs * (c5*(c2*s4 + c3 * c4*s2) - s2 * s3*s5) - mx_wcs * (c5*(c4*(s1*s3 - c1 * c2*c3) + c1 * s2*s4) + s5 * (c3*s1 + c1 * c2*s3)) + my_wcs * (c5*(c4*(c1*s3 + c2 * c3*s1) - s1 * s2*s4) + s5 * (c1*c3 - c2 * s1*s3));
	double my = mx_wcs * (s5*(c4*(s1*s3 - c1 * c2*c3) + c1 * s2*s4) - c5 * (c3*s1 + c1 * c2*s3)) - mz_wcs * (s5*(c2*s4 + c3 * c4*s2) + c5 * s2*s3) - my_wcs * (s5*(c4*(c1*s3 + c2 * c3*s1) - s1 * s2*s4) - c5 * (c1*c3 - c2 * s1*s3));
	double mz = mx_wcs * (s4*(s1*s3 - c1 * c2*c3) - c1 * c4*s2) - my_wcs * (s4*(c1*s3 + c2 * c3*s1) + c4 * s1*s2) + mz_wcs * (c2*c4 - c3 * s2*s4);

	Torque_5 += mz;
	Torque_4 += c5 * my + mx * s5 - D5 * (c5*fx - fy * s5);
	Torque_3 += c4 * mz + c5 * mx*s4 - my * s4*s5 + D5 * fx*s4*s5 + c5 * D5*fy*s4;
	Torque_2 += c3 * c5*my + c3 * mx*s5 - mz * s3*s4 + c4 * c5*mx*s3 + D3 * fx*s3*s5 - c4 * my*s3*s5 - c3 * c5*D5*fx + c5 * D3*fy*s3 + c3 *D5*fy*s5 + c3 * D3*fz*s4 - c3 * c4*c5*D3*fx + c3 * c4*D3*fy*s5 + c4 * c5*D5*fy*s3 + c4 * D5*fx*s3*s5;

	Torque_1 += c5 * my*s2*s3 - c2 * c5*mx*s4 - c2 * c4*mz + c2 * my*s4*s5 + c3 * mz*s2*s4 + mx * s2*s3*s5 - c3 * c5*D3*fy*s2 - c2 * c5*D5*fy*s4 - c3 * c4*c5*mx*s2 - c3 * D3*fx*s2*s5 - c5 * D5*fx*s2*s3 - c2 * D5*fx*s4*s5 + c3 * c4*my*s2*s5 + D5 * fy*s2*s3*s5 + D3 * fz*s2*s3*s4 - c3 * c4*c5*D5*fy*s2 - c4 * c5*D3*fx*s2*s3 - c3 * c4*D5*fx*s2*s5 + c4 * D3*fy*s2*s3*s5;

#ifdef DEBUG
	std::cout << Torque_1 << "\t"
		<< Torque_2 * 20 / 3 << "\t"
		<< Torque_3 * 62<< "\t"
		<< Torque_4 * 62<< "\t"
		<< Torque_5 * 62 << "\n";
#endif // debug

	if (!this->is_BaseLock)
		this->MotorBaseR->TorqueControl(*m_serial, Torque_1);
	else
		this->MotorBaseR->IncreControl(*m_serial, 0, 10);

	if(!this->is_UarmSLock)
		this->MotorUarmS->TorqueControl(*m_serial, Torque_2 / UPARMRATIO);
	else
		this->MotorUarmS->IncreControl(*m_serial, 0, 10);

	/*if(!this->is_LarmSLock)
		this->MotorLarmS->TorqueControl(*m_serial, Torque_3);
	else
		this->motorlarms->IncreControl(*m_serial, 0, 10);*/ //jy版本的没有小臂摆动电机，20221103改
	 
	if(!this->is_LarmRLock)
		this->MotorLarmR->TorqueControl(*m_serial, Torque_3);
	else
		this->MotorLarmR->IncreControl(*m_serial, 0, 10);

	if(!this->is_WristSLock)
		this->MotorWristS->TorqueControl(*m_serial, Torque_4);
	else
		this->MotorWristS->IncreControl(*m_serial, 0, 10);

	if (!this->is_WristRLock)
		this->MotorWristR->TorqueControl(*m_serial, Torque_5);
	else
		this->MotorWristR->IncreControl(*m_serial, 0, 10);
}
void ROBOT::IROBOT::OutputForce(double _fx, double _fy, double _fz, double _mx, double _my, double _mz)
{
	double _force[6] = { _fx,_fy,_fz,_mx,_my,_mz };
	this->OutputForce(_force);
}
void ROBOT::IROBOT::OutputForce()
{
	double force[6] = { 0,0, 0, 0, 0, 0 };
	this->OutputForce(force);
}
void ROBOT::IROBOT::BackToOrigin()
{
	if (!is_connect)
		return;
	BaseRtoPosition(0, BACKSPEED);
	UarmStoPosition(0, BACKSPEED);
	 // LarmStoPosition(0, BACKSPEED);
	LarmRtoPosition(0, BACKSPEED);
	WristStoPosition(0, BACKSPEED);
	WristRtoPosition(0, BACKSPEED);
	Close();
}
void ROBOT::IROBOT::Close()
{
	if (!is_connect)
		return;
	MotorBaseR->Close(*m_serial);
	MotorUarmS->Close(*m_serial);
	// MotorLarmS->Close(*m_serial); //Jy版本的没有小臂摆动电机，20221103改
	MotorLarmR->Close(*m_serial);
	MotorWristS->Close(*m_serial);
	MotorWristR->Close(*m_serial);
	this->is_close = true;
}
std::string ROBOT::IROBOT::ConnectToHandle(std::string _portname)
{
	this->m_handle = new iHANDLE(_portname);
	std::string errmsg = this->m_handle->Connect();
	if (errmsg.empty())
		is_handleconnect = true;
	return errmsg;
}
wchar_t * ROBOT::IROBOT::ConnectToHandle(const wchar_t * _portname)
{
	std::string temp;
	Wchar_tToString(temp, _portname);
	std::string errmsg = ConnectToHandle(temp);
	wchar_t * ptr = char2wchar(errmsg.c_str());
	if (ptr == nullptr)
		this->is_handleconnect = true;
	return ptr;
}
std::string ROBOT::IROBOT::GetHandleInfo()
{
	if (!is_handleconnect)
		return std::string();
	return m_handle->Getinfo();
}
wchar_t * ROBOT::IROBOT::GetHandleInfo(int i)
{
	if (!is_handleconnect)
		return nullptr;
	return char2wchar(m_handle->Getinfo().c_str());
}
bool ROBOT::IROBOT::isHandleConnect()
{
	return this->m_handle->isConnect();
}
void ROBOT::IROBOT::CloseHandle()
{
	if (isHandleConnect())
		this->m_handle->Close();
}
#pragma region private
void ROBOT::IROBOT::GetAngle()
{
	if (!is_connect)
		return;

	this->m_angle.BaseR = MotorBaseR->angle(*m_serial);
	this->m_angle.UarmS = ((0 - MotorUarmS->angle(*m_serial)) / UPARMRATIO);
 	 // this->m_angle.LarmS = 90.0 - MotorLarmS->angle(*m_serial);
	this->m_angle.LarmR = MotorLarmR->angle(*m_serial);
	this->m_angle.WristS = - MotorWristS->angle(*m_serial);
	this->m_angle.WristR = MotorWristR->angle(*m_serial);
}
void ROBOT::IROBOT::GetPosition()
{
	this->Angle();
	double theta1 = this->m_angle.BaseR * PI / 180.0;
	double theta2 = this->m_angle.UarmS * PI / 180.0;
	// 20221104 修改
	double theta3 = this->m_angle.LarmR * PI / 180.0;
	double theta4 = this->m_angle.WristS * PI / 180.0;
	double theta5 = this->m_angle.WristR * PI / 180.0;

	double c1 = std::cos(theta1), c2 = std::cos(theta2), c3 = std::cos(theta3), c4 = std::cos(theta4), c5 = std::cos(theta5);
	double s1 = std::sin(theta1), s2 = std::sin(theta2), s3 = std::sin(theta3), s4 = std::sin(theta4), s5 = std::sin(theta5);


	// 1104 更改
	this->m_endpose.px = c1 * D3*s2 - D5 * (s4*(s1*s3 - c1 * c2*c3) - c1 * c4*s2);
	this->m_endpose.py = D5 * (s4*(c1*s3 + c2 * c3*s1) + c4 * s1*s2) + D3 * s1*s2;
	this->m_endpose.pz = -c2 * D3 - D5 * (c2*c4 - c3 * s2*s4);

}
ROBOT::STATE ROBOT::IROBOT::GetState(HTMotor & _motor)
{
	STATE _state;
	if (!is_connect)
		return _state;
	_motor.GetHTMotorRealData(*m_serial);
	_state.Temperature = _motor.temperture(*m_serial);
	_state.Current = _motor.current(*m_serial);
	_state.Vlotage = _motor.voltage(*m_serial);
	_state.state = _motor.state(*m_serial);
	_state.wstate = char2wchar(_state.state.c_str());
	return _state;
}
void ROBOT::IROBOT::base_toangle(double _angle,int16_t _speed, HTMotor & _motor)
{
	if (!this->is_connect)
		return;
	if(this->is_close)
		this->is_close = false;
	_motor.AbsolControl(*m_serial,_angle, _speed);
	return;
}
void ROBOT::IROBOT::base_increangle(double _angle, int16_t _speed, HTMotor & _motor)
{
	if (!this->is_connect)
		return;
	if (this->is_close)
		this->is_close = false;
	_motor.IncreControl(*m_serial, _angle, _speed);
	return;
}
void ROBOT::IROBOT::Wchar_tToString(std::string& szDst, const wchar_t*wchar)
{
	const wchar_t * wText = wchar;
	DWORD dwNum = WideCharToMultiByte(CP_OEMCP, NULL, wText, -1, NULL, 0, NULL, FALSE);//WideCharToMultiByte的运用
	char *psText;  // psText为char*的临时数组，作为赋值给std::string的中间变量
	psText = new char[dwNum];
	WideCharToMultiByte(CP_OEMCP, NULL, wText, -1, psText, dwNum, NULL, FALSE);//WideCharToMultiByte的再次运用
	szDst = psText;// std::string赋值
	delete[]psText;// psText的清除
}
wchar_t *  ROBOT::IROBOT::char2wchar(const char* cchar)
{
	wchar_t *m_wchar;
	int len = MultiByteToWideChar(CP_ACP, 0, cchar, strlen(cchar), NULL, 0);
	m_wchar = new wchar_t[len + 1];
	MultiByteToWideChar(CP_ACP, 0, cchar, strlen(cchar), m_wchar, len);
	m_wchar[len] = '\0';
	return m_wchar;
}
#pragma endregion
