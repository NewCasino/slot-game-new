/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：ViewPresenters.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using Slot.UI;

/// <summary>
/// 文件名：视图层基类
/// 说明:
/// </summary>
public class ViewPresenters : MonoBehaviour
{
	//视图的UI面板
	public RectTransform viewPanel;

	#region Unity3D messages
	void Awake()
	{
		viewPanel = gameObject.transform as RectTransform;
		viewPanel.transform.SetParentObjExt(UIMgr.Instance.viewCanvas.gameObject);
		InitUI();
		AwakeUnityMsg();
	}

	void Start()
	{

		StartUnityMsg();
	}

	void OnValidate()
	{
		OnValidateUnityMsg();
	}

	void OnDestroy()
	{
		OnDestroyUnityMsg();
	}
	#endregion

	#region Unity3D Messages propagation
	protected virtual void AwakeUnityMsg()
	{
	}

	protected virtual void StartUnityMsg()
	{
	}

	protected virtual void OnValidateUnityMsg()
	{
	}

	protected virtual void OnDestroyUnityMsg()
	{
	}
	#endregion

	protected virtual void InitUI()
	{
	}

}
