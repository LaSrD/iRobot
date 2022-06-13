#include "pch.h"

#include "IRobotDLL_forCsharp.h"
namespace IRobotDLLforCsharp {
	IROBOT::IROBOT(String ^ _portname) {
		Temperature = gcnew array<float>(6);
		Current = gcnew array<float>(6);
		Vlotage = gcnew array<float>(6);
		Motorstate = gcnew array<String ^>(6);
		pin_ptr<const wchar_t> portname = PtrToStringChars(_portname);
		m_robot = new ROBOT::IROBOT(portname);
	}
	IROBOT::~IROBOT() {
		delete m_robot;
	}
	void IROBOT::Info() {
		auto tempinfo = m_robot->GetInfo();
		this->Serial_Num = gcnew String(tempinfo.Serial_Num.c_str());
		this->Procotol_Type = gcnew String(tempinfo.Procotol_type.c_str());
		this->HW_Type = gcnew String(tempinfo.Hw_type.c_str());
		this->Ctrl_Num = gcnew String(tempinfo.Ctrl_Num.c_str());
		
	}
	bool IROBOT::isConnect() {
		return m_robot->isConnect();
	}
	bool IROBOT::isClose() {
		return m_robot->isClose();
	}
	String ^ IROBOT::Connect() {
		return gcnew String(m_robot->Connect_forCsharrp());
	}
	void IROBOT::Angle() {
		
		ROBOT::ROBOTANGLE tempangle = m_robot->Angle();
		BaseRangle = tempangle.BaseR;
		UarmSangle = tempangle.UarmS;
		LarmSangle = tempangle.LarmS;
		LarmRangle = tempangle.LarmR;
		WristSangle = tempangle.WristS;
		WristRangle = tempangle.WristR;
	}	//бу
	void IROBOT::Pose() {
		ROBOT::ENDPOSITION _pose = m_robot->Pose();
		px = _pose.px;
		py = _pose.py;
		pz = _pose.pz;
		pitch = _pose.pitch;
		yaw= _pose.yaw;
		roll = _pose.roll;
	}	//mm
	void IROBOT::State() {
		auto tempstate = m_robot->State();
		Temperature[0] = tempstate[0].Temperature;
		Temperature[1] = tempstate[1].Temperature;
		Temperature[2] = tempstate[2].Temperature;
		Temperature[3] = tempstate[3].Temperature;
		Temperature[4] = tempstate[4].Temperature;
		Temperature[5] = tempstate[5].Temperature;

		Current[0] = tempstate[0].Current;
		Current[1] = tempstate[1].Current;
		Current[2] = tempstate[2].Current;
		Current[3] = tempstate[3].Current;
		Current[4] = tempstate[4].Current;
		Current[5] = tempstate[5].Current;

		Vlotage[0] = tempstate[0].Vlotage;
		Vlotage[1] = tempstate[1].Vlotage;
		Vlotage[2] = tempstate[2].Vlotage;
		Vlotage[3] = tempstate[3].Vlotage;
		Vlotage[4] = tempstate[4].Vlotage;
		Vlotage[5] = tempstate[5].Vlotage;

		Motorstate[0] = gcnew String(tempstate[0].wstate);
		Motorstate[1] = gcnew String(tempstate[1].wstate);
		Motorstate[2] = gcnew String(tempstate[2].wstate);
		Motorstate[3] = gcnew String(tempstate[3].wstate);
		Motorstate[4] = gcnew String(tempstate[4].wstate);
		Motorstate[5] = gcnew String(tempstate[5].wstate);
	}
	inline void IROBOT::LockBaseR() {
		m_robot->LockBaseR();
	}
	inline void IROBOT::LockUarmS() {
		m_robot->LockUarmS();
	}
	inline void IROBOT::LockLarmS() {
		m_robot->LockLarmS();
	}
	inline void IROBOT::LockLarmR() {
		m_robot->LockLarmR();
	}
	inline void IROBOT::LockWristR() {
		m_robot->LockWristR();
	}
	inline void IROBOT::LockWristS() {
		m_robot->LockWristS();
	}
	inline void IROBOT::UnLockBaseR() {
		m_robot->UnLockBaseR();
	}
	inline void IROBOT::UnLockUarmS() {
		m_robot->UnLockUarmS();
	}
	inline void IROBOT::UnLockLarmS() {
		m_robot->UnLockLarmS();
	}
	inline void IROBOT::UnLockLarmR() {
		m_robot->UnLockLarmR();
	}
	inline void IROBOT::UnLockWristR() {
		m_robot->UnLockWristR();
	}		
	inline void IROBOT::UnLockWristS(){
		m_robot->UnLockWristS();
	}
	inline void IROBOT::Lock(){
		m_robot->Lock();
	}
	inline void IROBOT::UnLock(){
		m_robot->UnLock();
	}
	void IROBOT::BaseRtoPosition(double _angle, int16_t speed){
		m_robot->BaseRtoPosition(_angle, speed);
	}
	void IROBOT::UarmStoPosition(double _angle, int16_t speed){
		m_robot->UarmStoPosition(_angle, speed);
	}
	void IROBOT::LarmStoPosition(double _angle, int16_t speed){
		m_robot->LarmStoPosition(_angle, speed);
	}
	void IROBOT::LarmRtoPosition(double _angle, int16_t speed){
		m_robot->LarmRtoPosition(_angle, speed);
	}
	void IROBOT::WristStoPosition(double _angle, int16_t speed){
		m_robot->WristStoPosition(_angle, speed);
	}
	void IROBOT::WristRtoPosition(double _angle, int16_t speed){
		m_robot->WristRtoPosition(_angle, speed);
	}
	void IROBOT::ToAbsoangle(double *  _angle, int16_t speed){
		m_robot->ToAbsoangle(_angle, speed);
	}
	void IROBOT::ToIncreangle(double * _angle, int16_t speed){
		m_robot->ToIncreangle(_angle, speed);
	}
	void IROBOT::OutputForce(double _fx, double _fy, double _fz, double _mx, double _my, double _mz){
		m_robot->OutputForce(_fx,_fy,_fz,_mx,_my,_mz);
	}
	void IROBOT::OutputForce(){
		m_robot->OutputForce();
	}
	void IROBOT::BackToOrigin(){
		m_robot->BackToOrigin();
	}
	void IROBOT::Close() {
		m_robot->Close();
	}

	void StringToWstring(std::wstring& szDst, std::string str)
	{
		std::string temp = str;
		int len = MultiByteToWideChar(CP_ACP, 0, (LPCSTR)temp.c_str(), -1, NULL, 0);
		wchar_t * wszUtf8 = new wchar_t[len + 1];
		memset(wszUtf8, 0, len * 2 + 2);
		MultiByteToWideChar(CP_ACP, 0, (LPCSTR)temp.c_str(), -1, (LPWSTR)wszUtf8, len);
		szDst = wszUtf8;
		std::wstring r = wszUtf8;
		delete[] wszUtf8;
	}
	wchar_t *  IROBOT::char2wchar(const char* cchar)
	{
		wchar_t *m_wchar;
		int len = MultiByteToWideChar(CP_ACP, 0, cchar, strlen(cchar), NULL, 0);
		m_wchar = new wchar_t[len + 1];
		MultiByteToWideChar(CP_ACP, 0, cchar, strlen(cchar), m_wchar, len);
		m_wchar[len] = '\0';
		return m_wchar;
	}
}

