/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：MouseEventMgr.cs
* 版权所有：	
* 文件编号：
* 创 建 人：Tycho
* 创建日期：2016-11-4
* 修 改 人：
* 修改日期：
* 描	述：业务逻辑类
* 版 本 号：1.0
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


public enum EMouseKey
{
	None = -1,
	Left = 0,
	Right = 1,
	Middle = 2,
}

public enum EMouseEvent
{
	None,
	Click,          //鼠标按键点下
	Moving,         //鼠标按下 但是没有任何选中 然后移动
	Draging,        //鼠标按下并且选中物体 然后移动
	Scrolling,      //鼠标中键滚动
	DoubleClick,    //双击
	Down,           //鼠标按下
}

public delegate void DelegateVoid();
public delegate void DelegateInt(int value);
public delegate void DelegateString(string value);
public delegate void DelegateObject(System.Object obj);




