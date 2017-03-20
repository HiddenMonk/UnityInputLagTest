using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class ForceSetMouse
{
	//Code taken from here http://stackoverflow.com/questions/18378142/simulate-mouse-move/18379218#18379218

	[DllImport("User32.dll",
               EntryPoint = "mouse_event",
               CallingConvention = CallingConvention.Winapi)]
    internal static extern void Mouse_Event(int dwFlags,
                                            int dx,
                                            int dy,
                                            int dwData,
                                            int dwExtraInfo);

    [DllImport("User32.dll",
               EntryPoint = "GetSystemMetrics",
               CallingConvention = CallingConvention.Winapi)]
    internal static extern int InternalGetSystemMetrics(int value);


	public static void MoveMouse(int to_x, int to_y, bool absolute)
	{
		int mouseEvent = 0x0001;
		if(absolute) mouseEvent |= 0x8000;
		
		Mickey mickey = GetMickeys(to_x, to_y, absolute);
		
		// 0x0001 | 0x8000: Move + Absolute position
		Mouse_Event(mouseEvent, mickey.x, mickey.y, 0, 0);
	}

	public static void PressMouseLeft(int to_x, int to_y, bool absolute)
	{
		Mickey mickey = GetMickeys(to_x, to_y, absolute);

		// 0x0002: Left button down
		Mouse_Event(0x0002 | 0x8000, mickey.x, mickey.y, 0, 0);

		// 0x0004: Left button up
		Mouse_Event(0x0004 | 0x8000, mickey.x, mickey.y, 0, 0);
	}

	static Mickey GetMickeys(int to_x, int to_y, bool absolute)
	{
		Mickey mickey = new Mickey(to_x, to_y);

		if(absolute)
		{
			int screenWidth = InternalGetSystemMetrics(0);
			int screenHeight = InternalGetSystemMetrics(1);

			// Mickey X coordinate
			mickey.x = (int)System.Math.Round(to_x * 65536.0 / screenWidth);
			// Mickey Y coordinate
			mickey.y = (int)System.Math.Round(to_y * 65536.0 / screenHeight);
		}

		return mickey;
	}

	struct Mickey
	{
		public int x;
		public int y;

		public Mickey(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}