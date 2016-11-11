/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：Controller.cs
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
using UnityEngine.Events;
using System;

/// <summary>
/// 文件名:控制器基类
/// 说明:
/// </summary>
public abstract class Controller
{

	/// <summary>
	/// 得到视图层
	/// </summary>
	/// <value>The view.</value>
	protected ViewPresenters View { get; set; }

	/// <summary>
	/// 得到模型层
	/// </summary>
	/// <value>The model.</value>
	protected Model Model { get; set; }

	/// <summary>
	/// 加载完标记
	/// </summary>
	public bool IsLoaded = false;

	protected int intShowFrame = 0;
	/// <summary>
	///显示标记
	/// </summary>
	public bool IsOnShow { get; protected set; }
	public bool IsOnExpand = false;
	private bool isFocused = false;
	protected bool isCloseWhenNotClickSelf = false;

	public bool IsFocused
	{
		get
		{
			return isFocused;
		}
		set
		{
			if (isFocused != value)
			{
				isFocused = value;
			}
		}
	}

	protected List<Controller> lstChildDialog = new List<Controller>();

	public DelegateVoid createViewFinishListener = null;

	//窗体点击回调事件
	public DialogClickEvent dlgClickEvent;
	protected DelegateVoid dlgOpenListener = null;
	protected DelegateVoid dlgHideListener = null;
	protected DelegateVoid dlgCloseListener = null;


	/// <summary>
	/// 
	/// </summary>
	/// <param name="active">生成view时是否是激活的</param>
	/// <param name="native">true:the asset is in Resource folder,false:the asset is in other folder </param>
	protected Controller(EViewID viewID, int viewInstID, GameObject view, bool active = true, bool native = false)
	{
		try
		{
			if (null != view)
			{
				this.InitPre(viewID, viewInstID, view);
				this.IsLoaded = true;
			}
			else
			{
				Debug.LogError(string.Format("Create the View viewID:[{0}] fail.", viewID));
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
		/*
		string viewPath = string.Format("{0}/{1}", StrDef.VIEWDIR, viewName);
		if (native)
		{
			GameObject view = AssetLoadMgr.Instance.LoadnNativeAsset<GameObject>(viewPath);
			if (null != view)
			{
				Init(view);
			}
			else
			{
				Debug.LogError(string.Format("The view [{0}] is not exists", view.name));
			}
		}
		else
		{
			ModelMgr.Instance.LoadModel(viewPath, new ModelMgr.ModelCallback((string name, GameObject view, object callbackData)=>
			{
				if(null != view)
				{
					Init(view);
				}
				else
				{
					Debug.LogError(string.Format("The view [{0}] is not exists", name));
				}
			}));
		}
		*/
	}

	/// <summary>
	/// 初始化
	/// </summary>
	private void InitPre(EViewID viewID,int viewInstID, GameObject view)
	{
		this.Model = CreateModel();
		this.View = CreateView(view);
		this.createViewFinishListener += this.InitPost;
		this.dlgOpenListener += this.AddListener;
		this.dlgCloseListener += this.RemoveListener;
		this.SetViewID(viewID);
		this.SetViewInstID(viewInstID);
	}

	/// <summary>
	///后初始化
	/// </summary>
	protected abstract void InitPost();

	/// <summary>
	///创建View
	/// </summary>
	/// <returns>The view.</returns>
	protected abstract ViewPresenters CreateView(GameObject go);

	/// <summary>
	///创建Model
	/// </summary>
	/// <returns>The model.</returns>
	protected abstract Model CreateModel();

	/// <summary>
	/// 添加监听,只对view中InitUI()里的UI有效
	/// </summary>
	protected abstract void AddListener();

	/// <summary>
	/// 移除监听
	/// </summary>
	protected abstract void RemoveListener();


	/// <summary>
	/// 更新
	/// </summary>
	/// <param name="fTime">F time.</param>
	public virtual void Update(float fTime)
	{

	}
	/// <summary>
	/// 获取窗口ID
	/// </summary>
	public EViewID GetViewID()
	{
		return this.Model.ViewID;
	}

	/// <summary>
	/// 设置窗口ID
	/// </summary>
	public void SetViewID(EViewID viewID)
	{
		this.Model.ViewID = viewID;
	}

	/// <summary>
	/// 获取窗口实例ID
	/// </summary>
	public int GetViewInstID()
	{
		return this.Model.ViewInstID;
	}

	/// <summary>
	/// 设置窗口实例ID
	/// </summary>
	public void SetViewInstID(int viewInstID)
	{
		this.Model.ViewInstID = viewInstID;
	}

	/// <summary>
	/// 显示
	/// </summary>
	public void OpenView()
	{
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Click, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Click, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.DoubleClick, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.DoubleClick, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Down, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Down, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.AddMouseEventListener(EMouseEvent.Click, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.AddMouseEventListener(EMouseEvent.Click, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.AddMouseEventListener(EMouseEvent.DoubleClick, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.AddMouseEventListener(EMouseEvent.DoubleClick, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.AddMouseEventListener(EMouseEvent.Down, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.AddMouseEventListener(EMouseEvent.Down, EMouseKey.Right, OnMouseEventHandler);
		KeyBoardEventMgr.Instance.AddKeyBoardEventListener(KeyboardEventHandler);
		if (!IsOnShow)
		{
			intShowFrame = Time.frameCount;
			IsOnShow = true;
			IsFocused = true;
			this.SetActive(true);
			this.ShowDialog(this.GetViewInstID());
			this.View.viewPanel.SetAsLastSibling();
		}
		if (dlgOpenListener != null)
		{
			dlgOpenListener();
		}
	}

	public void ShowDialog(int dialogInstID)
	{
		if (!UIMgr.Instance.dicViewOnShow.ContainsKey(dialogInstID))
		{
			Controller dialog = null;
			foreach (KeyValuePair<int, Controller> keyvalue in UIMgr.Instance.listAllViews)
			{
				if (keyvalue.Key == dialogInstID)
				{
					dialog = keyvalue.Value;
					UIMgr.Instance.dicViewOnShow.Add(dialogInstID, dialog);
				}
			}
			//设置其他窗体非聚焦状态
			if (dialog != null)
			{
				SetOtherDlgUnFocus(dialog);
			}
		}
	}

	/// <summary>
	/// 设置窗体
	/// </summary>
	/// <param name="dialogInstID"></param>
	public void SetOtherDlgUnFocus(Controller dlgExcept)
	{
		foreach (KeyValuePair<int, Controller> keyval in UIMgr.Instance.listAllViews)
		{
			Controller dlg = keyval.Value;
			if (dlg != null && dlg != dlgExcept)
			{
				dlg.IsFocused = false;
			}
		}
	}

	/// <summary>
	/// 关闭窗口 将窗口设为不可见
	/// </summary>
	public virtual void CloseDialog()
	{
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Click, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Click, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.DoubleClick, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.DoubleClick, EMouseKey.Right, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Down, EMouseKey.Left, OnMouseEventHandler);
		MouseEventMgr.Instance.RemoveMouseEventListener(EMouseEvent.Down, EMouseKey.Right, OnMouseEventHandler);
		KeyBoardEventMgr.Instance.RemoveKeyBoardEventListener(KeyboardEventHandler);
		if (IsOnShow)
		{
			for (int i = 0; i < lstChildDialog.Count; ++i)
			{
				if (lstChildDialog[i] != null)
					lstChildDialog[i].CloseDialog();
			}

			IsOnShow = false;
			IsFocused = false;
			this.SetActive(false);
			this.HideDialog(GetViewInstID());
			if (dlgCloseListener != null)
			{
				dlgCloseListener();
			}
		}
	}

	/// <summary>
	/// 隐藏窗口 将窗口从显示列表中移除  内部接口 业务层不允许调用
	/// </summary>
	/// <param name="dlg"></param>
	private void HideDialog(int dialogInstID)
	{
		if (UIMgr.Instance.dicViewOnShow.ContainsKey(dialogInstID))
		{
			UIMgr.Instance.dicViewOnShow.Remove(dialogInstID);
		}
		//隐藏窗体后设置最后一个显示窗体为聚焦状态
		Controller dialog = null;
		foreach (KeyValuePair<int, Controller> keyvalue in UIMgr.Instance.listAllViews)
		{
			if (keyvalue.Key == dialogInstID)
			{
				dialog = keyvalue.Value;
				dialog.IsFocused = false;
			}
		}
		for (int i = UIMgr.Instance.listAllViews.Count - 1; i >= 0; i--)
		{
			if (UIMgr.Instance.listAllViews[i].Value.IsOnShow)
			{
				UIMgr.Instance.listAllViews[i].Value.IsFocused = true;
				break;
			}
		}

		if (dlgHideListener != null)
		{
			dlgHideListener();
		}
	}

	/// <summary>
	/// 显示
	/// </summary>
	public void SetActive(bool active)
	{
		this.View.viewPanel.gameObject.SetActive(active);
	}

	#region 鼠标事件
	private void OnMouseEventHandler(MouseEventMgr.ResonseInfo eventinfo)
	{
		if (IsOnShow && IsFocused && eventinfo.objTarget != null)
		{
			Transform transDialog = UIMgr.Instance.GetDialogTransformByUI(eventinfo.objTarget.transform);
			if (transDialog != null && transDialog == View.viewPanel.transform)
			{
				if (!IsFocused)
				{
					IsFocused = true;
					UIMgr.Instance.SetOtherDlgUnFocus(this);
				}
			}

			dlgClickEvent.Invoke();
		}

		if (isCloseWhenNotClickSelf && IsOnShow && IsFocused)
		{
			if (eventinfo.objTarget == null || eventinfo.objTarget.layer != LayerID.LayerUI)
			{
				CloseDialog();
			}
			else
			{
				//TODO
				//if (!View.viewPanel.CheckChild(eventinfo.objTarget))
				//{
				//	CloseDialog();
				//}
			}
		}
	}

	#endregion

	#region 键盘事件
	private void KeyboardEventHandler(KeyBoardEventMgr.ResonseInfo eventinfo)
	{
		if (IsOnShow && IsFocused)
		{
			OnKeyboardEventHandler(eventinfo);
		}
	}

	protected virtual void OnKeyboardEventHandler(KeyBoardEventMgr.ResonseInfo eventinfo)
	{

	}
	#endregion

	public class DialogClickEvent : UnityEvent
	{

	}

}
