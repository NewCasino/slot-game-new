/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：SingletonWithComponentTemp.cs
* 版权所有：	
* 文件编号：
* 创 建 人：Tycho
* 创建日期：2016/11/1
* 修 改 人：
* 修改日期：
* 描	述：业务逻辑类
* 版 本 号：1.0.0
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using UnityEngine;
using System.Collections;

/// <summary>
/// 文件名:线程不安全组件单例模板
/// 说明：
/// </summary>
public class SingletonWithComponentTemp : SingletonWithComponent<SingletonWithComponentTemp>
{
	/// <summary>
	/// 初始化
	/// </summary>
	protected override void InitPre()
	{

	}

	/// <summary>
	/// 后初始化
	/// </summary>
	protected override void InitPost()
	{

	}

	/// <summary>
	/// 清理（多次）
	/// </summary>
	protected override void Clear()
	{

	}

	/// <summary>
	/// 结束（一次）
	/// </summary>
	protected override void Finish()
	{

	}

}
