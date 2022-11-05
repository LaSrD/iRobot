#pragma once
/************************************************************************
*                                                                       *
*   iRobotBase.h --													    *
*                                                                       * 
************************************************************************/

//设备信息
#define SERIALNUM	"iRobot_v0.1"		// 系列号
#define PROCOTOL	"RS485-115200"		//	通讯协议
#define HWTYPE		"HT-DMR-4015/6315"	// 硬件类型
#define CTRLNUM		"beta-iRobotdll_v0.1"//驱动版本

#define PI 3.141592654
//
// 电机ID: hahahahhahaa
//
#define ID_BASER	(uint8_t)0x06
#define ID_UARMS	(uint8_t)0x05
#define ID_LARMS	(uint8_t)0x04
#define ID_LARMR	(uint8_t)0x03
#define ID_WRISTS	(uint8_t)0x02
#define ID_WRISTR	(uint8_t)0x01

//回原点的角速度rpm
#define BACKSPEED	 10
//
#define	SAMELIMIT	0.4	
//大臂减速比
#define UPARMRATIO	3.0
//运动学参数
#define A2			128.0
#define D4			147.72
#define D6			102.0
//质量参数（mm，kg）
#define GLODO		9.801

#define M5			0.320
#define M4			0.226
#define M3			0.269
#define M2			0.737
//质心位置
#define H5			56.49
#define H4			5.460
#define H3			73.70
#define H2			72.72

