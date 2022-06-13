// IRobotTest.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>

# include<ctime>
#include <windows.h>
#include"IRobot_DLL.h"
using namespace std;

int main()
{
	ROBOT::IROBOT test2("COM6");

	double time = 0;
	double counts = 0;
	LARGE_INTEGER nFreq;
	LARGE_INTEGER nBeginTime;
	LARGE_INTEGER nEndTime;


	double angle[6] = {
	0,90,120,90,200,0 };
	double angl2[6] = {
0,90,0,0,0,0 };

	double force[6] = { 0,1, 0, 0, 0, 0 };
	char str;
	cin >> str;
	if (str == 'v') {
		string msg = test2.Connect();
		if (!msg.empty())
			cout << msg;
	}
	cin >> str;
	if (test2.isConnect()) {
		if (str == 'a')
			test2.LockBaseR();
		if (str == 'c')
			test2.BackToOrigin();
		if (str == 'k')
			test2.Close();
	}
	while (true)
	{
		QueryPerformanceFrequency(&nFreq);
		QueryPerformanceCounter(&nBeginTime);//开始计时 
		ROBOT::ROBOTANGLE _angle = test2.Angle();
		ROBOT::ENDPOSITION _pose = test2.Pose();

		test2.OutputForce(force);
		/*cout << _angle.BaseR << "\t";
		cout << _angle.UarmS << "\t" ;
		cout << _angle.LarmS << "\t" ;
		cout << _angle.LarmR << "\t" ;
		cout << _angle.WristS << "\t";
		cout << _angle.WristR << "\n" << ends;*/
		/*cout << _pose.px << "\t";
		cout << _pose.pz << "\t";
		cout << _pose.py << "\n";*/
		//cout << test.Angle().UarmS << "\t" << ends;;
		//cout << test.Angle().LarmS << "\t" << ends;;0
		//cout << test.Angle().LarmR << "\t" << ends;;
		//cout << test.Angle().WristR << "\t" << ends;;
		//cout << test.Angle().WristS << "\n" << ends;;
		 QueryPerformanceCounter(&nEndTime);//停止计时  
		 time = (double)(nEndTime.QuadPart - nBeginTime.QuadPart) / (double)nFreq.QuadPart;//计算程序执行时间单位为s  
		cout << "程序执行时间：" << time * 1000 << "ms" << endl;
		//cin >> str;
		//cout << test.State()[0].state << "\n";
		//system("cls");
	}
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
