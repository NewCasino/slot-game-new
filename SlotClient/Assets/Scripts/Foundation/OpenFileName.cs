using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

//http://www.cnblogs.com/U-tansuo/archive/2012/07/10/GetOpenFileName.html

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
	public int structSize = 0;
	public IntPtr dlgOwner = IntPtr.Zero;
	public IntPtr instance = IntPtr.Zero;
	public String filter = null;
	public String customFilter = null;
	public int maxCustFilter = 0;
	public int filterIndex = 0;
	public String file = null;
	public int maxFile = 0;
	public String fileTitle = null;
	public int maxFileTitle = 0;
	public String initialDir = null;
	public String title = null;
	public int flags = 0;
	public short fileOffset = 0;
	public short fileExtension = 0;
	public String defExt = null;
	public IntPtr custData = IntPtr.Zero;
	public IntPtr hook = IntPtr.Zero;
	public String templateName = null;
	public IntPtr reservedPtr = IntPtr.Zero;
	public int reservedInt = 0;
	public int flagsEx = 0;
}

public class DllOpenFileDialog
{
	[DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
	public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

	[DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
	public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
}