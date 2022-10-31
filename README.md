



# iRobot 开发手册

> 版本 v1.0																											

<div style="page-break-after:always;"></div>

[TOC]



<div style="page-break-after:always;"></div>

## 1.概述

### 1.1 文件结构

​	*iRobot力反馈设备* 的开发中，需要直接使用到的库文件位于文件夹`.../iRobot_SDK`中，目前只提供64位开发包，需要再32位机器上进行开发的用户，可以利用提供的动态链接库源码编译生成32位开发包。

​	`.../iRobot_SDK/include/`目录下的文件是	*iRobot力反馈设备* 开发中需要用到的头文件，包含设备的全部接口函数；	`.../iRobot_SDK/dll/` 目录下的`IRobotDLL.dll` 是力反馈设备的SDK 动态链接库，对外提供设备的所有的接口函数，`HTMotor.dll` 是设备驱动电机控制动态链接库，需要包含进开发项目中，一般来说，开发者无需直接调用`HTMotor.dll`中的函数，有特殊需要，请见[HTMotor驱动库说明]()；`IRobotDLL_forCsharp.dll` 是针对 *C#*开发环境封装的动态链接库。

​	开发例程见Github仓库：[LaSrD/iRobot: iRobot(ibuaav2.0) 项目源码 (github.com)](https://github.com/LaSrD/iRobot)

- `HTMotor_DLL:` 设备驱动电机控制动态链接库的源码；
- `IRobotDLL:`设备动态链接库的源码；

- `iRobotTest：` 基于VC++开发的例程，提供VS2017的工程文件。
- `iRobot_上位机_v0_1:` 基于winform开发的例程，提供VS2017的工程文件。

### 1.2 开发环境

​	SDK 是windos系统下开发的动态链接库，可以被C++、C# 等语言环境载入。目前，我们提供了WIN10系统下，基于VS2017的两个例程。对于使用其他版本的`visual studio`，可以直接打开例程，按照提示进行工程转换。

<div style="page-break-after:always;"></div>

## 2.使用指南

### 2.1硬件连接

​		设备采用12V直流开关电源供电，开关电源需要接到220V民用电源上；`USB2.0`与`蓝牙`通过与上位机连接；具体的接口如下图所示。

​	![image-20221006152248484](http://picgo.wangeyi.ink/image-20221006152248484.png)

### 2.2设备驱动

​	 设备采用`USB转RS-485转换器`实现与上位机的通信，`win8`及以上免安装驱动（网络自动更新驱动），`win7`需要安装驱动，前往[绿联官网](https://www.lulian.cn/download/list-108-cn.html)下载。

### 2.3 上位机安装与调试

- iRobot上位机简介

  iRobot上位机是针对同构式六自由度力反馈设备iRobot开发的一款PC端调试工具软件，可以运行在Win7及以上系统中，操作界面如下：

  ![image-20221006153302161](http://picgo.wangeyi.ink/image-20221006153302161.png)

- 上位机连接

  iRobot和上位机之间通过USB转RS-485模块连接。

  上位机连接前的设置如下（其中COM口根据实际情况选择），点击打开设备后，连接iRobot。

  ![image-20221007104527195](http://picgo.wangeyi.ink/image-20221007104527195.png)

  - 设备状态

    连接成功后，在设备状态窗口中，能够实时监视设备的各个关节转角、电流以及末端位姿以及输出力的大小情况。

    <img src="http://picgo.wangeyi.ink/image-20221007104614914.png" alt="image-20221007104614914" style="zoom: 67%;" />

  - 位置控制

    提供力反馈设备的单关节位置控制、示教控制模式；关节控制时，滑动各个关节转角的滑条后，点击确定，即可控制设备主动得运动到对应位姿；示教模式时，手动操作设备运动，在关键位置点击Record，可以记录多条位置信息，最后点击Play。

    ​								 <img src="http://picgo.wangeyi.ink/image-20221007105514380.png" alt="image-20221007105514380" style="zoom:80%;" />                    

  - 力反馈演示

    直接控制力反馈设备的末端输出力方式与虚拟场景的力反馈演示；在拉动相应力（Fx/Fy/Fz）的滑条，可以控制设备末端输出相应大小的力；在虚拟场景中可以进行虚拟墙的力反馈演示。

    ![image-20221007110111623](http://picgo.wangeyi.ink/image-20221007110111623.png)

<div style="page-break-after:always;"></div>

## 3.快速开发指南

建议您按照如下的流程操作力反馈设备

注：设备的D-H坐标系如下图

![image-20221019160026679](http://picgo.wangeyi.ink/image-20221019160026679.png)

### 3.1 载入动态链接库

- `使用 C++进行开发`：在工程项目中包含头文件（`.../iRobot_SDK/include/`中的所有文件）；包含开发需要用到的动态链接库（`.../iRobot_SDK/dll/`中的所有文件），然后就可以直接在工程中引用SDK中的接口函数了。操作如下：

  ![image-20220728171144295](http://picgo.wangeyi.ink/image-20220728171144295.png)

  并在工程中包含头文件

  ```c++
  #include"IRobot_DLL.h"
  ```

- `使用C# 开发`： 在工程项目中引用需要用到的动态链接库`IRobotDLL_forCsharp.dll`，添加下面的代码。以包含所需的名称空间； 使用C# 开发的一个例程是 我们基于winform开发的手控器上位机界面，源码见`iRobot_上位机_v0_1` 

  ```c++
  using IRobotDLLforCsharp;
  ```

### 3.2 初始化设备

完成了载入动态链接库之后，在使用其他接口之前，需要创建一个iRobot设备对象，并进行初始化，参数为设备连接至PC端得COM口（COM口按照实际情况选择）。

### 3.3 连接设备

创建iRobot设备对象后，通过`Connect()` 方法连接至设备。

- 通过类得方法接口，获取设备的关节转角、位姿；进行力控、位置控制等。

  示例如下：

  ```c++
  #include<ctime>
  #include <windows.h>
  #include"IRobot_DLL.h"
  using namespace std;
  double force[6] = { 0,1, 0, 0, 0, 0 };
  int main()
  {
  	ROBOT::IROBOT test2("COM6");        // 创建irobot对象并进行初始化，COM口按照实际情况选择
  	char str;
  	cin >> str;
  	if (str == 'v') {
  		string msg = test2.Connect();   // 连接至设备
  		if (!msg.empty())
  			cout << msg;
  	}
  	cin >> str;
  	if (test2.isConnect()) {
  		if (str == 'a')
  			test2.LockBaseR();         // 锁定关节1
  		if (str == 'c')
  			test2.BackToOrigin();		// 回到原点
  		if (str == 'k')
  			test2.Close();             // 关闭设备
  	} 
  	while (true)
  	{
  		ROBOT::ROBOTANGLE _angle = test2.Angle();  // 读出设备关节转角
  		ROBOT::ENDPOSITION _pose = test2.Pose(); // 获取设备末端位姿
  		test2.OutputForce(force);             // 控制设备末端输出力。
  	}
  }
  ```

<div style="page-break-after:always;"></div>

## 4.数据类型定义

> DLL 所有类型均位于命名空间`ROBOT`内

- `ROBOT::iRobotInfo`： 设备的信息

  **原型**：

  ```c++
  struct iRobotInfo {
  		iRobotInfo(std::string _a, std::string _b, 	std::string _c, std::string _d):
  		Serial_Num(_a), Procotol_type(_b), Hw_type(_c), Ctrl_Num(_d){}
  		std::string Serial_Num;    // 序列号
  		std::string Procotol_type; // 通信协议
  		std::string Hw_type;       // 硬件类型
  		std::string Ctrl_Num;      // DLL版本
  	};
  ```

- ` ROBOT::ENDPOSITION` :  末端位姿

  原型：

  ```c++
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
  ```

- ` ROBOT::ROBOTANGLE` ： 关节转角

  原型：

  ```c++
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
      double	BaseR;   // 根部关节
      double	UarmS;   // 大臂关节
      double	LarmS;   // 小臂摆动关节
      double	LarmR;   // 小臂自转关节
      double	WristS;  // 腕摆动关节
      double	WristR;  // 腕自转关节
  };
  ```

- `ROBOT::STATE`： 设备状态信息

- 原型：

  ```c++
  struct STATE
  {
  	STATE() :
  		Temperature(0),
  		Vlotage(0),
  		Current(0)
  	{}
  	float Temperature;		//系统温度C
  	float Vlotage;			//电压V
  	float Current;			//电流mA
  	std::string state;		//运行状态
  	wchar_t * wstate;		//运行状态forC#
  };
  ```

<div style="page-break-after:always;"></div>

## 5. C++接口函数说明

> 本章节描述的各接口的原型及说明，对于C++，可直接按照本手册中的函数名调用。

### 5.1 构造函数 

**原型：**

```c++
IROBOT::IROBOT
(
	const std::string portname
);
```

**功能说明：** 创建设备对象

**参数说明：** `portname` ： 连接至PC端的端口号，例如：`COM6`

**返  回  值：** 无

**示        例：** 

```c++
// C++ 
ROBOT::IROBOT m_irobot("COM6");
ROBOT::IROBOT *m_irobot2 = new ROBOT::IROBOT("COM6");  //指针类型
```

### 5.2 连接设备

#### 5.2.1 `Connect()`

**原型：**

```c++
std::string 
ROBOT::IROBOT::Connect();
```

**功能说明：** 连接至设备

**参数说明：** 无

**返  回  值：** 成功时，返回空串，否则返回的错误信息。

**示        例：**

```c++
// 连接至设备，如果出错，打印出错误信息
string msg =  m_irobot.Connect();
if (!msg.empty())
	cout << msg;
```

#### 5.2.2 `isConnect()`

**原型：**

```c++
bool 
ROBOT::IROBOT::isConnect() const; 
```

**功能说明：** 返回设备是否连接成功

**参数说明：** 无

**返  回  值：** 成功时，返回true，否则返回false。  

### 5.3 设备信息状态

#### **5.3.1** `GetInfo()`

**原型：**

```c++
ROBOT::iRobotInfo  
ROBOT::IROBOT::GetInfo();
```

**功能说明：** 获取设备的信息

**参数说明：** 无

**返  回  值：** 成功时，返回设备的信息结构体（请参考第4章中的`iRobotInfo`），否					则返回的结构体中各字符串都为空。

**示        例：** 

```c++
/* 获取m_irobot的 信息，并打印出来 */
ROBOT::iRobotInfo m_info = m_irobot.GetInfo();
cout << m_info.Serial_Num << endl;
cout << m_info.Procotol_type << endl;
cout << m_info.Hw_type << endl;
cout << m_info.Ctrl_Num << endl;
```

#### 5.3.2 `Angle()`

**原型：**

```c++
ROBOTANGLE & 
ROBOT::IROBOT::Angle();
```

**功能说明：** 获取设备的各个关节的转角

**参数说明：** 无

**返  回  值：** 成功时，返回设备的转角结构体（请参考第4章中的`ROBOTANGLE`）

**示        例：** 

```c++
/* 获取m_irobot的各个关节转角，并打印出来 */
ROBOT::ROBOTANGLE _angle = m_irobot.Angle();
cout << _angle.BaseR << "\t";
cout << _angle.UarmS << "\t" ;
cout << _angle.LarmS << "\t" ;
cout << _angle.LarmR << "\t" ;
cout << _angle.WristS << "\t";
cout << _angle.WristR << "\n" << ends;
```

#### 5.3.3 `Pose()`

**原型：**

```c++
ROBOTANGLE & 
ROBOT::IROBOT::Pose();
```

**功能说明：** 获取设备的末端位姿

**参数说明：** 无

**返  回  值：** 成功时，返回设备的位姿结构体（请参考第4章中的`ENDPOSITION`).

**示        例：** 

```c++
/* 获取m_irobot的位姿，并打印出来 */
ROBOT::ENDPOSITION _pose = m_irobot.Pose();
cout << _pose.px << "\t";
cout << _pose.pz << "\t";
cout << _pose.py << "\n";
```

#### 5.3.4 `State()`

**原型：**

```c++
using StateVector = std::vector<STATE>;
StateVector & 
ROBOT::IROBOT::State();
```

**功能说明：** 获取设备的运行状态

**参数说明：** 无

**返  回  值：** 成功时，返回设备的状态结构体（请参考第4章中的`STATE`)，其中StateVector 的大小为6， 分别是六个驱动电机的状态，否则返回空向量

**示        例：** 

```c++
/* 获取m_irobot的电机运行状态，并打印出来 */
ROBOT::StateVector _state = m_irobot.State();
cout << _state[0].Temperature << "\t";  // 电机0 ，根部旋转电机的温度
cout << _state[0].Vlotage << "\t"; // 电机0 ，根部旋转电机的电压
cout << _state[0]. Current << "\n";// 电机0 ，根部旋转电机的电流
```

### 5.4 位置控制

#### 5.4.1 单关节位置控制

原型：

```c++
void 
ROBOT::IROBOT::BaseRtoPosition(double angle, int16_t speed = 0);    // 底座旋转关节
void 
ROBOT::IROBOT::UarmStoPosition(double angle, int16_t speed = 0);    // 大臂摆动关节
void LarmStoPosition(double angle, int16_t speed = 0);    // 小臂摆动关节
void 
ROBOT::IROBOT::LarmRtoPosition(double angle, int16_t speed = 0);    //小臂旋转关节
void 
ROBOT::IROBOT::WristStoPosition(double angle, int16_t speed = 0);   // 腕部旋转关节
void
ROBOT::IROBOT::WristRtoPosition(double angle, int16_t speed = 0);    //腕部自转关节
```

**功能说明：** 控制设备各个关节分别运动到给定绝对角度（度）

**参数说明：** angle ： 目标角度（度）； speed ： 运动目标角度的速度大小（）

**返  回  值：** 无

#### 5.4.2 绝对位置控制

原型：

```c++
void 
ROBOT::IROBOT::ToAbsoangle(double * angle, int16_t speed = 0);
void 
ROBOT::IROBOT::ToAbsoangle(std::vector<double>, int16_t speed = 0);
```

**功能说明：** 控制设备关节分别运动到给定角度（度）

**参数说明：** angle ： 给定目标角度（度）数据，； speed ： 运动目标角度的速度大小（）

```c++
// 给定目标角度数组详细：
BaseRtoPosition(angle[5],speed);	// 第5个角度控制设备底部旋转关节
UarmStoPosition(angle[4], speed);
LarmStoPosition(angle[3], speed);
LarmRtoPosition(angle[2], speed);
WristStoPosition(angle[1], speed);
WristRtoPosition(angle[0], speed);   // 第0个角度 控制设备腕部旋转关节
```

**返  回  值：** 无

**示        例：** 

```c++
// 
double angle[6] = {10,10,10,10,10,10};
m_irobot.ToAbsoangle(angle,10);  // 设备各关节以10 的速度运动到指定位置
```

#### 5.4.3 增量位置控制

原型：

```c++
void 
ROBOT::IROBOT::ToIncreangle(double * angle, int16_t speed = 0);
void 
ROBOT::IROBOT::ToIncreangle(std::vector<double>, int16_t speed = 0);
```

**功能说明：** 控制设备关节分别运动到给定角度（度）

**参数说明：** angle ： 给定目标角度（度）数据，； speed ： 运动目标角度的速度大小（）

```c++
// 给定目标角度数组详细：
base_increangle(_angle[5], speed, *MotorBaseR); // 第5个角度控制设备底部旋转关节
base_increangle(_angle[4], speed, *MotorUarmS);
base_increangle(_angle[3], speed, *MotorLarmS);
base_increangle(_angle[2], speed, *MotorLarmR);
base_increangle(_angle[1], speed, *MotorWristS);
base_increangle(_angle[0], speed, *MotorWristR);// 第0个角度 控制设备腕部旋转关节
```

**返  回  值：** 无

**示        例：** 

```c++
// 
double angle[6] = {10,10,10,10,10,10};
m_irobot.ToIncreangle(angle,10);  // 设备各关节以10的速度旋转10度
```

### 5.5 关节锁定

> 备注： 关节锁定只有在设备运行在重量补偿状态才会起效

#### 5.5.1 单关节锁定

原型：

```c++
void ROBOT::IROBOT::LockBaseR();    // 锁定关节1
void ROBOT::IROBOT::LockUarmS();	// 锁定关节2
void ROBOT::IROBOT::LockLarmS();	// 锁定关节3
void ROBOT::IROBOT::LockLarmR();	// 锁定关节4
void ROBOT::IROBOT::LockWristR();	// 锁定关节5
void ROBOT::IROBOT::LockWristS();	// 锁定关节6
```

**参数说明：** 无

**返  回  值：** 无

#### 5.5.2 单关节解锁

原型：

```c++
void ROBOT::IROBOT::UnLockBaseR();    // 解锁关节1
void ROBOT::IROBOT::UnLockUarmS();	// 解锁关节2
void ROBOT::IROBOT::UnLockLarmS();	// 解锁关节3
void ROBOT::IROBOT::UnLockLarmR();	// 解锁关节4
void ROBOT::IROBOT::UnLockWristR();	// 解锁关节5
void ROBOT::IROBOT::UnLockWristS();	// 解锁关节6
```

**参数说明：** 无

**返  回  值：** 无

**示        例：** 

### 5.6 输出力

#### 5.6.1 重载1

原型：

```c++
void ROBOT::IROBOT::OutputForce(double *);
```

**参数说明：** 包含设备末端六维力的大小为6的数组；（使用时确保数组大小为6）

**返  回  值：** 无

#### 5.6.2 重载2

原型：

```c++
void ROBOT::IROBOT::OutputForce(double fx, double fy, double fz, double _mx = 0, double _my = 0, double _mz = 0);  //
```

**参数说明：** 

| 参数  | 说明                          |
| ----- | ----------------------------- |
| `fx`  | 末端输出力在x轴方向上的分量   |
| `fy`  | 末端输出力在y轴方向上的分量   |
| `fz`  | 末端输出力在z轴方向上的分量   |
| `_mx` | 末端输出力矩在x轴方向上的分量 |
| `_my` | 末端输出力矩在y轴方向上的分量 |
| `_mz` | 末端输出力矩在z轴方向上的分量 |

**返  回  值：** 无

#### 5.6.3 重载3

> 调用此重载函数表示，设备末端输出力为0，即设备运行在重力补偿模式中

原型：

```c++
void ROBOT::IROBOT::OutputForce();
```

**参数说明：** 无

**返  回  值：** 无

### 5.7 回到零点

原型：

```c++
void ROBOT::IROBOT::BackToOrigin();
```

**参数说明：** 无

**返  回  值：** 无

### 5.8 关闭

原型：

```c++
void ROBOT::IROBOT::Close();
```

**参数说明：** 无

**返  回  值：** 无

### 5.9 按键部分

> 按键部分与PC机之间采用蓝牙通信，基于 Bluetooth Specification V2.0 带 EDR 蓝牙协议的 数传模块。无线工作频段为2.4GHz ISM，调制方式是GFSK。模块最大发射功率为 4dBm， 接收灵敏度-85dBm，板载PCB天线，可以实现 10 米距离通信

#### 5.9.1 连接

原型：

```cpp
std::string ROBOT::IROBOT::ConnectToHandle(std::string _portname);
```

**参数说明：**  `portname` ： 连接至PC端的端口号，例如：`COM11`

**返  回  值：** 连接成功时，返回空的字符串，否则，返回连接的错误信息

原型：

```C++
bool  ROBOT::IROBOT::isHandleConnect();
```

**功能说明：** 返回设备是否连接成功

**参数说明：** 无

**返  回  值：** 成功时，返回true，否则返回false。  

#### 5.9.2 按键消息

原型：

```cpp
std::string ROBOT::IROBOT::GetHandleInfo();
```

**参数说明：** 无

**返  回  值：** 返回按键的消息，见下表

![image-20221019154703582](http://picgo.wangeyi.ink/image-20221019154703582.png)

#### 5.9.3 关闭

原型：

```c++
void ROBOT::IROBOT::CloseHandle()；
```

**参数说明：** 无

**返  回  值：** 无

#### 5.9.4 示例

```C++
# include<ctime>
#include <windows.h>
#include"IRobot_DLL.h"
using namespace std;

int main()
{
	ROBOT::IROBOT test2("COM6");
	char str;
	cin >> str;
	if (str == 'o') {
		string msg = test2.ConnectToHandle("COM11");  // 连接至手柄
		if (!msg.empty())
			cout << msg;
	}
	string info;
	while (true)
	{
		if (test2.isHandleConnect())
			cout << (info = test2.GetHandleInfo());
		if (info == "10000")
			test2.CloseHandle();
	}
}
```

<div style="page-break-after:always;"></div>

## 6. C#接口函数说明

> `using IRobotDLLforCsharp;`

### 6.0 属性

```c#
class IROBOT {
    // info();
    String ^ Serial_Num;  // 序列号
    String ^ Procotol_Type; // 通信协议
    String ^ HW_Type;
    String ^ Ctrl_Num;
    // angle();
    double BaseRangle;
    double UarmSangle;
    double LarmSangle;
    double LarmRangle;
    double WristSangle;
    double WristRangle;
    // pose();
    double px;
    double py;
    double pz;
    double pitch;
    double yaw;
    double roll;
    // state();
    array<float> ^ Temperature;
    array<float> ^ Current;
    array<float> ^ Vlotage;
    array<String ^> ^Motorstate;
}
```

### 6.1 构造函数 

**原型：**

```c++
IROBOT(String ^ _portname); 
```

**功能说明：** 创建设备对象

**参数说明：** `_portname` ： 连接至PC端的端口号，例如：`COM6`

**返  回  值：** 无

**示        例：** 

```c#
// C#
IROBOT m_iRobot = new IROBOT(comboport.Text);
```

### 6.2 连接设备

#### 6.2.1 `Connect()`

**原型：**

```c#
String ^ Connect();
```

**功能说明：** 连接至设备

**参数说明：** 无

**返  回  值：** 成功时，返回空串，否则返回的错误信息。

**示        例：**

```c#
// 连接至设备，如果出错，打印出错误信息
 string msg = m_iRobot.Connect();
if(msg == String.Empty)
{
    labColorconnect.ForeColor = Color.Green;
    labInfoconnect.Text = "Connected";
    deviceEnable = true;

}
```

#### 6.2.2 `isConnect()`

**原型：**

```c#
bool isConnect();
```

**功能说明：** 返回设备是否连接成功

**参数说明：** 无

**返  回  值：** 成功时，返回true，否则返回false。  

### 6.3 设备信息状态

#### **6.3.1** `Info()`

**原型：**

```c++
void Info();
```

**功能说明：** 获取设备的信息

**参数说明：** 无

**返  回  值：** 无； 调用函数后，通过类的属性获取，见示例

**示        例：** 

```c#
m_iRobot.Info();
txtseries.Text = m_iRobot.Serial_Num;
txtprocotol.Text = m_iRobot.Procotol_Type;
txthwtype.Text = m_iRobot.HW_Type;
txtcrtlnum.Text = m_iRobot.Ctrl_Num;
```

#### 6.3.2 `Angle()`

**原型：**

```c++
void Angle();	//°
```

**功能说明：** 获取设备的各个关节的转角

**参数说明：** 无

**返  回  值：** 无； 调用函数后，通过类的属性获取，见示例

**示        例：** 

```c#
m_iRobot.Angle();
Log(txtjointangle1, m_iRobot.BaseRangle);  //在文本框控件显示设备的底部关节转角
Log(txtjointangle2, m_iRobot.UarmSangle);
Log(txtjointangle3, m_iRobot.LarmSangle);
Log(txtjointangle4, m_iRobot.LarmRangle);
Log(txtjointangle5, m_iRobot.WristSangle);
Log(txtjointangle6, m_iRobot.WristRangle);
```

#### 6.3.3 `Pose()`

**原型：**

```c++
void Pose();	//mm
```

**功能说明：** 获取设备的末端位姿

**参数说明：** 无

**返  回  值：** 无； 调用函数后，通过类的属性获取，见示例

**示        例：** 

```c#
m_iRobot.Angle();
Log(txtpx, iBuaaDebug.m_iRobot.px);
Log(txtpy, iBuaaDebug.m_iRobot.py);
Log(txtpz, iBuaaDebug.m_iRobot.pz);
Log(txtroll, iBuaaDebug.m_iRobot.roll);
Log(txtpitch, iBuaaDebug.m_iRobot.pitch);
Log(txtyaw, iBuaaDebug.m_iRobot.yaw);
```

#### 6.3.4 `State()`

**原型：**

```c++
void State();
```

**功能说明：** 获取设备的运行状态

**参数说明：** 无

**返  回  值：**  无； 调用函数后，通过类的属性获取，见示例

**示        例：** 

```c#
 m_iRobot.State();
Log(txttemp1, m_iRobot.Temperature[5]);  // 在控件上显示关节1 的电机温度
Log(txttemp2, m_iRobot.Temperature[4]);  // 关节2 
Log(txttemp3, m_iRobot.Temperature[3]);  // 关节3
Log(txttemp4, m_iRobot.Temperature[2]);  // 关节4
Log(txttemp5, m_iRobot.Temperature[1]);  // 关节5
Log(txttemp6, m_iRobot.Temperature[0]);  // 关节6
```

### 6.4 位置控制

#### 6.4.1 单关节位置控制

原型：

```c++
void BaseRtoPosition(double angle , int16_t speed);
void UarmStoPosition(double angle , int16_t speed);
void LarmStoPosition(double angle , int16_t speed);
void LarmRtoPosition(double angle , int16_t speed);
void WristStoPosition(double angle , int16_t speed);
void WristRtoPosition(double angle , int16_t speed);
```

**功能说明：** 控制设备各个关节分别运动到给定绝对角度（度）

**参数说明：** angle ： 目标角度（度）； speed ： 运动目标角度的速度大小（）

**返  回  值：** 无

#### 6.4.2 绝对位置控制

原型：

```c++
void ToAbsoangle(double * angle , int16_t speed);
```

**功能说明：** 控制设备关节分别运动到给定角度（度）

**参数说明：** angle ： 给定目标角度（度）数据，； speed ： 运动目标角度的速度大小（）

```c++
// 给定目标角度数组详细：
BaseRtoPosition(angle[5],speed);	// 第5个角度控制设备底部旋转关节
UarmStoPosition(angle[4], speed);
LarmStoPosition(angle[3], speed);
LarmRtoPosition(angle[2], speed);
WristStoPosition(angle[1], speed);
WristRtoPosition(angle[0], speed);   // 第0个角度 控制设备腕部旋转关节
```

**返  回  值：** 无

**示        例：** 

```c++
// 
double angle[6] = {10,10,10,10,10,10};
m_irobot.ToAbsoangle(angle,10);  // 设备各关节以10 的速度运动到指定位置
```

#### 6.4.3 增量位置控制

原型：

```c++
void ToIncreangle(double * angle , int16_t speed);
```

**功能说明：** 控制设备关节分别运动到给定角度（度）

**参数说明：** angle ： 给定目标角度（度）数据，； speed ： 运动目标角度的速度大小（）

```c++
// 给定目标角度数组详细：
base_increangle(_angle[5], speed, *MotorBaseR); // 第5个角度控制设备底部旋转关节
base_increangle(_angle[4], speed, *MotorUarmS);
base_increangle(_angle[3], speed, *MotorLarmS);
base_increangle(_angle[2], speed, *MotorLarmR);
base_increangle(_angle[1], speed, *MotorWristS);
base_increangle(_angle[0], speed, *MotorWristR);// 第0个角度 控制设备腕部旋转关节
```

**返  回  值：** 无

**示        例：** 

```c++
// 
double angle[6] = {10,10,10,10,10,10};
m_irobot.ToIncreangle(angle,10);  // 设备各关节以10的速度旋转10度
```

### 6.5 关节锁定

> 备注： 关节锁定只有在设备运行在重量补偿状态才会起效

#### 6.5.1 单关节锁定

原型：

```c++
void LockBaseR();    // 锁定关节1
void LockUarmS();	// 锁定关节2
void LockLarmS();	// 锁定关节3
void LockLarmR();	// 锁定关节4
void LockWristR();	// 锁定关节5
void LockWristS();	// 锁定关节6
```

**参数说明：** 无

**返  回  值：** 无

#### 6.5.2 单关节解锁

原型：

```c++
void UnLockBaseR();    // 解锁关节1
void UnLockUarmS();	// 解锁关节2
void UnLockLarmS();	// 解锁关节3
void UnLockLarmR();	// 解锁关节4
void UnLockWristR();	// 解锁关节5
void UnLockWristS();	// 解锁关节6
```

**参数说明：** 无

**返  回  值：** 无

**示        例：** 

### 6.6 输出力

#### 6.6.1 重载1

原型：

```c++
void OutputForce(double fx, double fy, double fz, double _mx = 0, double _my = 0, double _mz = 0);  //
```

**参数说明：** 

| 参数  | 说明                          |
| ----- | ----------------------------- |
| `fx`  | 末端输出力在x轴方向上的分量   |
| `fy`  | 末端输出力在y轴方向上的分量   |
| `fz`  | 末端输出力在z轴方向上的分量   |
| `_mx` | 末端输出力矩在x轴方向上的分量 |
| `_my` | 末端输出力矩在y轴方向上的分量 |
| `_mz` | 末端输出力矩在z轴方向上的分量 |

**返  回  值：** 无

#### 6.6.2 重载2

> 调用此重载函数表示，设备末端输出力为0，即设备运行在重力补偿模式中

原型：

```c++
void OutputForce();
```

**参数说明：** 无

**返  回  值：** 无

### 6.7 回到零点

原型：

```c++
void BackToOrigin();
```

**参数说明：** 无

**返  回  值：** 无

### 6.8 关闭

原型：

```c++
void Close();
```

**参数说明：** 无

**返  回  值：** 无

### 6.9 按键部分

> 按键部分与PC机之间采用蓝牙通信，基于 Bluetooth Specification V2.0 带 EDR 蓝牙协议的 数传模块。无线工作频段为2.4GHz ISM，调制方式是GFSK。模块最大发射功率为 4dBm， 接收灵敏度-85dBm，板载PCB天线，可以实现 10 米距离通信

#### 6.9.1 连接

原型：

```cpp
String ^ConnectToHandle(String ^ _portname);
```

**参数说明：**  `portname` ： 连接至PC端的端口号，例如：`COM11`

**返  回  值：** 连接成功时，返回空的字符串，否则，返回连接的错误信息

原型：

```C++
bool isHandleConnect();
```

**功能说明：** 返回设备是否连接成功

**参数说明：** 无

**返  回  值：** 成功时，返回true，否则返回false。  

#### 6.9.2 按键消息

原型：

```cpp
String ^ GetHandleInfo();
```

**参数说明：** 无

**返  回  值：** 返回按键的消息，见下表

![image-20221019154703582](http://picgo.wangeyi.ink/image-20221019154703582.png)

#### 6.9.3 关闭

原型：

```c++
void CloseHandle();
```

**参数说明：** 无

**返  回  值：** 无

#### 6.9.4 示例

```C++
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRobotDLLforCsharp;
using System.Threading;

namespace Csharp_iRobotTest
{
    class Program
    {
        static IROBOT m_irobot = new IROBOT("COM6");
        static Thread thread;
        static void Main(string[] args)
        {
            int str = Console.Read();
            if (str == 'a')
            {
                string msg = m_irobot.Connect();
                if (msg != string.Empty)
                    Console.Write(msg);
            }
            if(m_irobot.isConnect())
            {
                m_irobot.Info();
                Console.Write(m_irobot.Serial_Num);
                Console.Write(m_irobot.Procotol_Type);
                Console.Write(m_irobot.HW_Type);
                Console.Write(m_irobot.Ctrl_Num);
                thread = new Thread(new ThreadStart(intance));
                thread.Start();
            }
            while(true)
            {
                int button = Console.Read();
                if (button == 'l')
                    m_irobot.LockBaseR(); // 
            }
        }
        static void intance()
        {
            while (true)
            {
                m_irobot.OutputForce();
            }
        }
    }
}

```

## 

