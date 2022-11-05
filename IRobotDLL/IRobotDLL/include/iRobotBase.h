#pragma once
/************************************************************************
*                                                                       *
*   iRobotBase.h --													    *
*                                                                       * 
************************************************************************/

//�豸��Ϣ
#define SERIALNUM	"iRobot_v0.1"		// ϵ�к�
#define PROCOTOL	"RS485-115200"		//	ͨѶЭ��
#define HWTYPE		"HT-DMR-4015/6315"	// Ӳ������
#define CTRLNUM		"beta-iRobotdll_v0.1"//�����汾

#define PI 3.141592654
//
// ���ID: hahahahhahaa
//
#define ID_BASER	(uint8_t)0x06
#define ID_UARMS	(uint8_t)0x05
#define ID_LARMS	(uint8_t)0x04
#define ID_LARMR	(uint8_t)0x03
#define ID_WRISTS	(uint8_t)0x02
#define ID_WRISTR	(uint8_t)0x01

//��ԭ��Ľ��ٶ�rpm
#define BACKSPEED	 10
//
#define	SAMELIMIT	0.4	
//��ۼ��ٱ�
#define UPARMRATIO	3.0
//�˶�ѧ����
#define A2			128.0
#define D4			147.72
#define D6			102.0
//����������mm��kg��
#define GLODO		9.801

#define M5			0.320
#define M4			0.226
#define M3			0.269
#define M2			0.737
//����λ��
#define H5			56.49
#define H4			5.460
#define H3			73.70
#define H2			72.72

