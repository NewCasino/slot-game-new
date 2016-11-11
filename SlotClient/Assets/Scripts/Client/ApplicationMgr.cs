/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：ApplicationMgr.cs
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
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// 应用模式
/// </summary>
public enum ApplicationMode
{
	//内测
	[Description("Internal")]
	Internal = 0,

	//公测
	[Description("Public")]
	Public = 1
}

/// <summary>
/// 文件名:应用管理器
/// 说明：总管理器，管理各个子管理器
/// </summary>
public class ApplicationMgr : SingletonWithComponent<ApplicationMgr>
{
	/// <summary>
	/// 版本模式
	/// </summary>
	private ApplicationMode appMode;

	/// <summary>
	/// 初始化
	/// </summary>
	protected override void InitPre()
	{
		this.InitMgr();
		this.InitializeAppMode();
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

	/// <summary>
	/// 注册子管理器
	/// </summary>
	private void InitMgr()
	{
		GameObject uiMgrObj = new GameObject(typeof(UIMgr).Name);
		uiMgrObj.AddComponent<UIMgr>();

		//GameObject modelMgrObj = new GameObject(typeof(ModelMgr).Name);
		//modelMgrObj.AddComponent<ModelMgr>();

		//GameObject audioMgrObj = new GameObject(typeof(AudioMgr).Name);
		//audioMgrObj.AddComponent<AudioMgr>();

		//GameObject mouseEventMgrObj = new GameObject(typeof(MouseEventMgr).Name);
		//mouseEventMgrObj.AddComponent<MouseEventMgr>();

		//GameObject keyBoardEventMgrObj = new GameObject(typeof(KeyBoardEventMgr).Name);
		//keyBoardEventMgrObj.AddComponent<KeyBoardEventMgr>();

		
	}

	/// <summary>
	/// 得到版本模式
	/// </summary>
	public ApplicationMode GetAppMode()
	{
		return appMode;
	}

	/// <summary>
	/// 是否是内测版本
	/// </summary>
	/// <returns><c>true</c> if this instance is internal; otherwise, <c>false</c>.</returns>
	public bool IsInternal()
	{
		return (GetAppMode() == ApplicationMode.Internal);
	}

	/// <summary>
	/// 是否是公测版本
	/// </summary>
	/// <returns><c>true</c> if this instance is public; otherwise, <c>false</c>.</returns>
	public bool IsPublic()
	{
		return (GetAppMode() == ApplicationMode.Public);
	}

	/// <summary>
	/// 初始化模式
	/// </summary>
	private void InitializeAppMode()
	{
		appMode = ApplicationMode.Internal;
		//if (Vars.Key("Application.Mode").GetStr("Internal") == "Public")
		//{
		//	m_eMode = ApplicationMode.PUBLIC;
		//}
	}


}
