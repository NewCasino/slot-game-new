/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：Model.cs
* 版权所有：	
* 文件编号：
* 创 建 人：Tycho
* 创建日期：2016-11-1
* 修 改 人：
* 修改日期：
* 描	述：业务逻辑类
* 版 本 号：1.0
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using UnityEngine;
using System.Collections;

/// <summary>
/// 文件名：模型层基类
/// 说明:
/// </summary>
public abstract class Model
{
	private EViewID viewID;
	public EViewID ViewID
	{
		get { return viewID; }
		set { viewID = value; }
	}

	protected int viewInstID = 0;
	public int ViewInstID
	{
		get { return viewInstID; }
		set { viewInstID = value; }
	}

	protected Model()
	{
		Init();
	}
	/// <summary>
	/// 初始化
	/// </summary>
	protected abstract void Init();

}
