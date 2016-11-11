/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：UIMgr.cs
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
using System;
using Slot.Utils;

/// <summary>
/// 文件名:UI管理器
/// 说明：子管理器，生成视图
/// </summary>
public class UIMgr : SingletonWithComponent<UIMgr>
{
	//视图父节点
	public Canvas viewCanvas;

	public List<KeyValuePair<int, Controller>> listAllViews = new List<KeyValuePair<int, Controller>>();
	public Dictionary<int, Controller> dicViewOnShow = new Dictionary<int, Controller>();

	private Sequence seqView = new Sequence(100, 1);

	/// <summary>
	/// 初始化
	/// </summary>
	protected override void InitPre()
	{
		GameObject ObjCanvas = GameObject.Find(StrDef.VIEWCANVAS);
		if (ObjCanvas == null)
		{
			Debug.LogError(string.Format("The gameobject [ViewCanvas] is not exist"));
			enabled = false;
			return;
		}
		viewCanvas = ObjCanvas.GetComponent<Canvas>();
		if (viewCanvas == null)
		{
			Debug.LogError(string.Format("The component [ViewCanvas] is not exist"));
			enabled = false;
			return;
		}

		if (viewCanvas == null)
		{
			Debug.LogError(string.Format("The [ViewCanvas] is not exist"));
			enabled = false;
			return;
		}
	}

	/// <summary>
	/// 后初始化
	/// </summary>
	protected override void InitPost()
	{
		this.CreateView(EViewID.MainMenu);
		this.CreateView(EViewID.Symbol);
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
		dicViewOnShow.Clear();
	}

	/// <summary>
	/// 更新
	/// </summary>
	private void Update()
	{
		foreach (Controller controller in dicViewOnShow.Values)
		{
			if (controller.IsOnShow && controller.IsLoaded)
			{
				controller.Update(Time.deltaTime);
			}
		}
	}

	/// <summary>
	/// 获取窗口操作，所有窗口都通过这里打开
	/// </summary>
	public Controller GetView(EViewID viewID)
	{
		Controller controller = null;
		EViewInstType viewInstType = ViewConfig.Instance.GetViewInstType(viewID);
		foreach (KeyValuePair<int, Controller> keyval in listAllViews)
		{
			if (keyval.Value.GetViewID() == viewID)  //已经打开的窗口 并且不允许多开的窗口  直接用这个实例返回 
			{
				if (viewInstType == EViewInstType.Single)
				{
					controller = keyval.Value;
					return controller;
				}
				else
				{
					//TODO
					break;
				}
			}
		}
		controller = this.CreateView(viewID);
		return controller;
	}


	/// <summary>
	/// 创建窗口操作
	/// </summary>
	private Controller CreateView(EViewID viewID)
	{
		Controller controller = null;
		try
		{
			EViewInstType viewInstType = ViewConfig.Instance.GetViewInstType(viewID);
			foreach (KeyValuePair<int, Controller> keyval in listAllViews)
			{
				if (keyval.Value.GetViewID() == viewID)  //已经打开的窗口 并且不允许多开的窗口  直接用这个实例返回 
				{
					if (viewInstType == EViewInstType.Single)
					{
						Debug.LogWarning(string.Format("The View viewID:[{0}] only allowed to be created one and it already exists ", viewID));
						return controller;
					}
					else
					{
						//TODO
						break;
					}
				}
			}

			GameObject viewPrefab = ViewConfig.Instance.GetViewPrefab(viewID);//待修改
			if (viewPrefab != null)
			{
				GameObject viewObj = GameObject.Instantiate<GameObject>(viewPrefab);
				int viewInstID = seqView.nextval;
				controller = ViewConfig.Instance.CreateViewInstance(viewID, viewInstID, viewObj); //创建窗口控制层

				if (null != controller)
				{
					listAllViews.Add(new KeyValuePair<int, Controller>(viewInstID, controller));
				}
				controller.OpenView();
				controller.createViewFinishListener();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("Create the View viewID:[{0}] fail,Msg:{1} exception:{2}", viewID, ex.Message, ex.StackTrace));
		}
		return controller;
	}


	/// <summary>
	/// 根据ui对应的Transform获取对应dialog的transform
	/// </summary>
	/// <param name="transformUI"></param>
	/// <returns></returns>
	public Transform GetDialogTransformByUI(Transform transformUI)
	{
		Transform transformDialog = null;
		if (transformUI != null)
		{
			ViewPresenters root = transformUI.GetComponent<ViewPresenters>();
			if (root != null)
			{
				transformDialog = root.transform;
			}
			else if (transformUI.parent != null)
			{
				transformDialog = GetDialogTransformByUI(transformUI.parent);
			}
		}
		return transformDialog;
	}

	/// <summary>
	/// 设置窗体
	/// </summary>
	/// <param name="dialogInstID"></param>
	public void SetOtherDlgUnFocus(Controller dlgExcept)
	{
		foreach (KeyValuePair<int, Controller> keyval in listAllViews)
		{
			Controller dlg = keyval.Value;
			if (dlg != null && dlg != dlgExcept)
			{
				dlg.IsFocused = false;
			}
		}
	}
}

