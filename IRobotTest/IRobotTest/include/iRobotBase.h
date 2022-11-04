#pragma once
/************************************************************************
*                                                                       *
*   iRobotBase.h --													    *
*                                                                       * 
************************************************************************/

//设备信息
#define SERIALNUM	"iRobot_bh_t_jy_v0.2"		// 系列号
#define PROCOTOL	"RS485-115200"		//	通讯协议
#define HWTYPE		"HT-DMR-4015/6315"	// 硬件类型
#define CTRLNUM		"beta-iRobot_t_jy_dll_v0.2"//驱动版本

#define PI 3.141592654
//
// 电机ID
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
// JY版本修改 #define A2			128.0
#define D3			259.0
#define D5			102.0
//质量参数（mm，kg）
#define GLODO		9.801

// JY版本修改 #define M5			0.320
#define M4			0.292
#define M3			0.226
#define M2			0.657
//质心位置
// JY版本修改 #define H5			56.49
#define H4			56.49
#define H3			5.46
#define H2			89.0

