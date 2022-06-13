#pragma once
/************************************************************************
*                                                                       *
*   HTBase.h -- This module defines the 64-Bit Windows Base APIs       *
*                                                                       *
*   Copyright (c) Microsoft Corp. All rights reserved.                  *
*                                                                       *
************************************************************************/

using ByteVector = std::vector<uint8_t>;
//
//电机类型
//
#define DM_R_4315 4315
#define DM_R_6015 6015
//
// 电机运行状态
//
#define	MotorClosed		"Closed State "
#define MotorTorque		"Torque control State"
#define MotorSpeed		"Speed control State"
#define MotorAngle		"Angle control State"
//
//	HT电机协议头
//
#define HT_MASTERHEAD				(uint8_t)0x3E
#define HT_SLAVEHEAD				(uint8_t)0x3C
//
//	HT电机协议命令码
//
#define HTR_MOTORINFO				(uint8_t)0x0A
#define HTR_MOTORDATA				(uint8_t)0x0B
#define HTR_MOTORPARA				(uint8_t)0x0C
#define HTW_MOTORPARA_V0LATILE		(uint8_t)0x0D
#define HTW_MOTORPARA_STABLE		(uint8_t)0x0E
#define HTW_MOTORRESET				(uint8_t)0x0F
#define HTW_MOTORCALIBRATE			(uint8_t)0x20
#define HTW_MOTORSETORIGIN			(uint8_t)0x21
#define HTR_MOTORANGLESPED			(uint8_t)0x2F
#define HTR_SYSTEMDATA				(uint8_t)0x40
#define HTW_CLEARERRO				(uint8_t)0x41
#define HTW_MOTORCLOSE				(uint8_t)0x50
#define HTW_MOTORTOORIGIN_LOOPANG	(uint8_t)0x51
#define HTW_BACKORIGIN_SHORSET		(uint8_t)0x52
#define HTW_TORQUECTRL				(uint8_t)0x53
#define HTW_SPEEDCTRL				(uint8_t)0x54
#define HTW_ABSOLCTRL				(uint8_t)0x55
#define HTW_INCRECTRL				(uint8_t)0x56
#define HTW_ANGLESPEED				(uint8_t)0x57


//
//协议基础长度
//
#define BASELENGTH	(uint8_t)0x05
#define CRC16LENGTH	(uint8_t)0x02
//
//	包序号
//
#define PACKGEINDEX(x) (uint8_t)x
//
//	数据包长度
//
#define SENDLEN_MOTORINFO	(uint8_t)0x00
#define READLEN_MOTORINFO	(uint8_t)0x14

#define SENDLEN_MOTORDATA	(uint8_t)0x00
#define READLEN_MOTORDATA	(uint8_t)0x0D

#define SENDLEN_R_PARAMETER	(uint8_t)0x00
#define SENDLEN_W_PARAMETER	(uint8_t)0x1A

#define READLEN_PARAMETER	(uint8_t)0x1A
#define	SENDLEN_RESTORE		(uint8_t)0x00

#define SENDLEN_CALIBRATE	(uint8_t)0x00
#define READLEN_CALIBRATE	(uint8_t)0x00

#define SENDLEN_SETORIGIN	(uint8_t)0x00
#define READLEN_SETORIGIN	(uint8_t)0x03

#define SENDLEN_ENCODEDATA	(uint8_t)0x00
#define READLEN_ENCODEDATA	(uint8_t)0x08

#define SENDLEN_SYSTEMDATA	(uint8_t)0x00
#define READLEN_SYSTEMDATA	(uint8_t)0x05

#define SENDLEN_CLEARERROR	(uint8_t)0x00
#define READLEN_CLEARERROR	(uint8_t)0x00

#define SENDLEN_CLOSEMOTOR	(uint8_t)0x00
#define READLEN_CLOSEMOTOR	(uint8_t)0x00

#define SENDLEN_BACKORIGIN	(uint8_t)0x00
#define READLEN_BACKORIGIN	(uint8_t)0x08

#define SENDLEN_TORQUECTRL	(uint8_t)0x02
#define READLEN_TORQUECTRL	(uint8_t)0x08

#define SENDLEN_SPEEDCTRL	(uint8_t)0x02
#define READLEN_SPEEDCTRL	(uint8_t)0x08

#define SENDLEN_ABSOLCTRL	(uint8_t)0x04
#define READLEN_ABSOLCTRL	(uint8_t)0x08

#define SENDLEN_INCRECTRL	(uint8_t)0x02
#define READLEN_INCRECTRL	(uint8_t)0x08

#define SENDLEN_ANGLESPEED	(uint8_t)0x03
#define READLEN_ANGLESPEED	(uint8_t)0x02
//
// 位运算
//
#define ZEROBIT(x)				(x & 0x01)
#define FIRSTBIT(x)				(x & 0x02)
#define SECONDEBIT(x)			(x & 0x04)

#define LOWEIGHTBITS(x)			(x & 0x00FF)
#define HIGHEIGHTBITS(x)		((x >> 8) & 0x00FF)
#define HIGHEIGHTBITS_2(x)		((x >> 16) & 0x00FF)
#define HIGHEIGHTBITS_3(x)		((x >> 24) & 0x00FF)
#define TOUINT16_T(x,y)			(((y << 8) & 0xFF00) | x)
#define TOINT32_T(a,b,c,d)		(((d << 24) & 0xFF000000) | ((c << 16) & 0xFF0000) | ((b << 8) & 0xFF00)| a)
#define DEBUG(x)				std::cout << x << std::endl;

//
// 编码器线数
//

#define ENCODERCOUNT (double)16384