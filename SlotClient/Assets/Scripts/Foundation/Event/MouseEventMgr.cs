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


using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// 文件名:鼠标事件管理器
/// 说明：子管理器
/// </summary>
public class MouseEventMgr : SingletonWithComponent<MouseEventMgr>
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


	public delegate void DlgtMouseEventListener(ResonseInfo eventinfo);
	public struct MouseEventKey
	{
		public GameObject target;
		public EMouseKey mousekey;
		public EMouseEvent eventtype;
		public MouseEventKey(GameObject obj, EMouseEvent type, EMouseKey key)
		{
			target = obj;
			eventtype = type;
			mousekey = key;
		}
	}

	public class ListenerInfo
	{
		public object objListener;
		public DlgtMouseEventListener listener;
		public ListenerInfo() { }
		public ListenerInfo(DlgtMouseEventListener listener, object objListener = null)
		{
			this.listener = listener;
			this.objListener = objListener;
		}
	}

	public struct ResonseInfo
	{
		public EMouseEvent eventtype;
		public EMouseKey mousekey;
		public GameObject objTarget;
		public Vector2 delta;
		public bool isEventBegin;
		public bool isEventEnd;
		public Vector3 worldPosition;
		public Vector2 upPosition;  //鼠标抬起时的屏幕位置
		public float clickTime;
		public Vector2 curPostion;//当前位置

		public override string ToString()
		{
			return string.Format("type:{0}    key:{1}   target:{3}    delta:{4}    isBegin:{5}    isEnd:{6}", eventtype, mousekey, objTarget, delta, isEventBegin, isEventEnd);
		}
	}

	private bool IsIntercept = false;
	private object objIntercept = null;
	private EMouseEvent interceptEvnet = EMouseEvent.None;
	private Dictionary<MouseEventKey, List<ListenerInfo>> m_dicMouseEvents = new Dictionary<MouseEventKey, List<ListenerInfo>>();

	public void GetControl(object obj, EMouseEvent eventtype)
	{
		if (obj == null)
			return;
		objIntercept = obj;
		IsIntercept = true;
		interceptEvnet = eventtype;
	}

	public void ReleaseControl()
	{
		IsIntercept = false;
		objIntercept = null;
	}

	/// <summary>
	/// 添加鼠标监听
	/// </summary>
	/// <param name="objTarget">监听对象</param>
	/// <param name="eventtype">监听事件类型</param>
	/// <param name="mousekey">鼠标键</param>
	/// <param name="newListener">监听者接口</param>
	public void AddMouseEventListener(GameObject objTarget, EMouseEvent eventtype, EMouseKey mousekey, DlgtMouseEventListener newListener, object objListener = null, bool insertFirst = false)
	{
		List<ListenerInfo> listListeners = null;
		MouseEventKey eventkey = new MouseEventKey(objTarget, eventtype, mousekey);
		m_dicMouseEvents.TryGetValue(eventkey, out listListeners);
		if (listListeners == null)
		{
			listListeners = new List<ListenerInfo>();
			ListenerInfo listenerinfo = new ListenerInfo(newListener, objListener);
			listListeners.Add(listenerinfo);
			m_dicMouseEvents[eventkey] = listListeners;
		}
		else
		{
			bool bFound = false;
			for (int i = 0; i < listListeners.Count; i++)
			{
				if (listListeners[i].listener == newListener && listListeners[i].objListener == objListener)
				{
					break;
				}
			}

			if (!bFound)
			{
				ListenerInfo listenerinfo = new ListenerInfo(newListener, objListener);
				if (insertFirst)
				{
					listListeners.Insert(0, listenerinfo);
				}
				else
				{
					listListeners.Add(listenerinfo);
				}
			}
		}
	}

	public void AddMouseEventListener(EMouseEvent eventtype, EMouseKey mousekey, DlgtMouseEventListener newListener, object objListener = null, bool insertFirst = false)
	{
		AddMouseEventListener(null, eventtype, mousekey, newListener, objListener, insertFirst);
	}

	public void RemoveMouseEventListener(GameObject objTarget, EMouseEvent eventtype, EMouseKey mousekey, DlgtMouseEventListener listener, object objListener = null)
	{
		List<ListenerInfo> listListeners = null;
		MouseEventKey eventkey = new MouseEventKey(objTarget, eventtype, mousekey);
		m_dicMouseEvents.TryGetValue(eventkey, out listListeners);
		if (listListeners != null)
		{
			for (int i = 0; i < listListeners.Count; i++)
			{
				if (listListeners[i].listener == listener && listListeners[i].objListener == objListener)
				{
					listListeners.RemoveAt(i);
					break;
				}
			}
			if (listListeners.Count == 0)
				m_dicMouseEvents.Remove(eventkey);
		}
	}

	public void RemoveMouseEventListener(EMouseEvent eventtype, EMouseKey mousekey, DlgtMouseEventListener listener, object objListener = null)
	{
		RemoveMouseEventListener(null, eventtype, mousekey, listener, objListener);
	}

	public void DispachEvent(ResonseInfo eventinfo)
	{
		MouseEventKey eventkey = new MouseEventKey(eventinfo.objTarget, eventinfo.eventtype, eventinfo.mousekey);
		List<ListenerInfo> listListeners = null;

		m_dicMouseEvents.TryGetValue(eventkey, out listListeners);

		List<ListenerInfo> allIntercepts = new List<ListenerInfo>();
		List<ListenerInfo> allNormals = new List<ListenerInfo>();
		if (listListeners != null)       //指定对象先检查是否有监听
		{
			List<ListenerInfo> intercepts;
			List<ListenerInfo> normals;
			GetInteceptListener(eventinfo.eventtype, listListeners, out intercepts, out normals);
			allIntercepts.AddRange(intercepts);

			if (allIntercepts.Count == 0)
			{
				allNormals.AddRange(normals);
			}
		}

		if (eventinfo.objTarget != null)       //不指定对象是否有监听
		{
			eventkey.target = null;
			m_dicMouseEvents.TryGetValue(eventkey, out listListeners);
			if (listListeners != null)
			{
				List<ListenerInfo> intercepts;
				List<ListenerInfo> normals;
				GetInteceptListener(eventinfo.eventtype, listListeners, out intercepts, out normals);
				allIntercepts.AddRange(intercepts);
				if (allIntercepts.Count == 0)
				{
					allNormals.AddRange(normals);
				}
			}
		}

		if (allIntercepts.Count > 0)
		{
			for (int i = 0; i < allIntercepts.Count; i++)
			{
				if (allIntercepts[i].listener != null)
				{
					allIntercepts[i].listener(eventinfo);
				}
			}
			return;
		}
		for (int i = 0; i < allNormals.Count; i++)
		{
			if (allNormals[i].listener != null)
			{
				allNormals[i].listener(eventinfo);
			}
		}
	}

	private void GetInteceptListener(EMouseEvent eventtype, List<ListenerInfo> listListeners, out List<ListenerInfo> intercepts, out List<ListenerInfo> normal)
	{
		intercepts = new List<ListenerInfo>();
		normal = new List<ListenerInfo>();
		for (int i = 0; i < listListeners.Count; i++)
		{
			if (IsIntercept && eventtype == interceptEvnet && listListeners[i].objListener == objIntercept)
			{
				intercepts.Add(listListeners[i]);
			}
			else
			{
				normal.Add(listListeners[i]);
			}
		}
	}

}
