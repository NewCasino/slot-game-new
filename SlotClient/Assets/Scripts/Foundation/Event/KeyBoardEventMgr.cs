/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：KeyBoardEventMgr.cs
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
using System;

/// <summary>
/// 文件名:键盘事件管理器
/// 说明：子管理器
/// </summary>
public class KeyBoardEventMgr : SingletonWithComponent<KeyBoardEventMgr>
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

	public class ResonseInfo
	{
		public List<KeyCode> keys;
		public string strKey;
		public bool IfDown;
		public bool isEventBegin = false;
		public bool isEventEnd = false;
	}

	protected class EventListenerInfo
	{
		public DlgtKeyBoardEventListener listener;
		public float eventResponseInterval;
	}

	public delegate void DlgtKeyBoardEventListener(ResonseInfo eventinfo);

	private List<KeyCode> listControlKeys = new List<KeyCode>();//控制类型的按键
	private List<KeyCode> listListenerKeys = new List<KeyCode>();//除控制类型外的监听的按键

	private List<EventListenerInfo> listKeyBoardEvents = new List<EventListenerInfo>();
	private List<KeyCode> m_lstCurrPressed = new List<KeyCode>();

	private string m_strLastPressedKey = string.Empty;
	private float m_fLastPressedTime = 0;

	public KeyBoardEventMgr()   //必须是控制键
	{
		//设置控制键集合
		listControlKeys.Add(KeyCode.LeftControl);
		listControlKeys.Add(KeyCode.RightControl);
		listControlKeys.Add(KeyCode.LeftShift);
		listControlKeys.Add(KeyCode.RightShift);
		listControlKeys.Add(KeyCode.LeftAlt);
		listControlKeys.Add(KeyCode.RightAlt);
		listControlKeys.Add(KeyCode.LeftCommand);
		listControlKeys.Add(KeyCode.RightCommand);
		//设置普通监听的按键集合
		KeyCode[] keyCodes = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
		foreach (KeyCode keyCode in keyCodes)
		{
			bool isControlKey = false;
			foreach (KeyCode controlKey in listControlKeys)
			{
				if (controlKey == keyCode)
				{
					isControlKey = true;
					break;
				}
			}
			if (!isControlKey)
			{
				if (!listListenerKeys.Contains(keyCode))
				{
					listListenerKeys.Add(keyCode);
				}
			}
		}
	}


	#region KeyBoard event public function

	public void AddKeyBoardEventListener(DlgtKeyBoardEventListener newlistener)
	{
		AddKeyBoardEventListener(newlistener, 0.1f);
	}

	public void AddKeyBoardEventListener(DlgtKeyBoardEventListener newlistener, float responseInterval)
	{
		//是否已经存在这个键的监听
		EventListenerInfo listenerinfo = new EventListenerInfo();
		listenerinfo.listener = newlistener;
		listenerinfo.eventResponseInterval = responseInterval;
		listKeyBoardEvents.Add(listenerinfo);
	}

	public void RemoveKeyBoardEventListener(DlgtKeyBoardEventListener listener)
	{
		//从监听字典中移除
		for (int i = listKeyBoardEvents.Count - 1; i >= 0; i--)
		{
			EventListenerInfo listenerInfo = listKeyBoardEvents[i];
			if (listenerInfo != null && listenerInfo.listener != null && listenerInfo.listener == listener)
			{
				listenerInfo.listener -= listener;
				listKeyBoardEvents.RemoveAt(i);
			}
		}
	}
	#endregion

	protected string GenKey(List<KeyCode> lstkeys)
	{
		string strKey = "";
		lstkeys.Sort();
		for (int i = 0; i < lstkeys.Count; i++)
		{
			if (i > 0)
				strKey += "+";

			string strCurr = lstkeys[i].ToString();
			if (listControlKeys.Contains(lstkeys[i]))
			{
				strCurr = strCurr.Replace("Left", "");
				strCurr = strCurr.Replace("Right", "");
			}
			strKey += strCurr;
		}
		return strKey;
	}

	public void DealKeyBoardEvent()
	{
		m_lstCurrPressed.Clear();
		if (Input.anyKey)
		{
			//检测组合件是否按下
			foreach (KeyCode controlKey in listControlKeys)     //控制类型键只判断摁下状态
			{
				if (Input.GetKey(controlKey))
				{
					m_lstCurrPressed.Add(controlKey);
				}
			}
			bool ifDown = false;
			foreach (KeyCode listenerKey in listListenerKeys)        //其他按键 按下的时候生效 不允许组合
			{
				if (Input.GetKeyDown(listenerKey))
				{
					m_lstCurrPressed.Add(listenerKey);
					ifDown = true;
					break;
				}
			}
			if (!ifDown)
			{
				foreach (KeyCode listenerKey in listListenerKeys)        //其他按键 按下的时候生效 不允许组合
				{
					if (Input.GetKey(listenerKey))
					{
						m_lstCurrPressed.Add(listenerKey);
						break;
					}
				}
			}
			else
			{
				m_strLastPressedKey = string.Empty;     // 不是一直按着键盘则不考虑事件触发间隔时间
			}
			if (m_lstCurrPressed.Count > 0)
			{
				string strKey = GenKey(m_lstCurrPressed);
				if (listKeyBoardEvents.Count > 0)
				{
					EventListenerInfo[] events = listKeyBoardEvents.ToArray();
					foreach (EventListenerInfo listenerInfo in events)
					{
						if (listenerInfo.listener != null)
						{
							if (strKey == m_strLastPressedKey && Time.realtimeSinceStartup - m_fLastPressedTime < listenerInfo.eventResponseInterval)
							{
								// 按下键盘响应太频繁
								//Debug.LogFormat(string.Format("DealKeyBoardEvent key {0} is too fast\r\n", strKey));
							}
							else
							{
								try
								{
									ResonseInfo resInfo = new ResonseInfo();
									resInfo.keys = m_lstCurrPressed;
									resInfo.strKey = strKey;
									resInfo.IfDown = ifDown;
									resInfo.isEventBegin = ifDown;
									//Debug.LogFormat(string.Format("Invoke target is {0} method is {1}\r\n", listenerInfo.listener.Target, listenerInfo.listener.Method));
									listenerInfo.listener.Invoke(resInfo);
								}
								catch (Exception e)
								{
									Debug.LogError(e);
								}
							}
						}
					}
					m_strLastPressedKey = strKey;
					m_fLastPressedTime = Time.realtimeSinceStartup;
				}
			}
		}
		else
		{
			foreach (KeyCode listenerKey in listListenerKeys) //其他按键 按下的时候生效 不允许组合
			{
				if (Input.GetKeyUp(listenerKey))
				{
					m_lstCurrPressed.Add(listenerKey);
					break;
				}
			}
			if (m_lstCurrPressed.Count > 0)
			{
				string strKey = GenKey(m_lstCurrPressed);
				if (listKeyBoardEvents.Count > 0)
				{
					EventListenerInfo[] events = listKeyBoardEvents.ToArray();
					foreach (EventListenerInfo listenerInfo in events)
					{
						if (strKey == m_strLastPressedKey && Time.realtimeSinceStartup - m_fLastPressedTime >= listenerInfo.eventResponseInterval && listenerInfo.listener != null)
						{
							try
							{
								ResonseInfo resInfo = new ResonseInfo();
								resInfo.keys = m_lstCurrPressed;
								resInfo.strKey = strKey;
								resInfo.IfDown = false;
								resInfo.isEventBegin = false;
								resInfo.isEventEnd = true;
								//Debug.LogFormat(string.Format("Invoke target is {0} method is {1}\r\n", listenerInfo.listener.Target, listenerInfo.listener.Method));
								listenerInfo.listener.Invoke(resInfo);
							}
							catch (Exception e)
							{
								Debug.LogError(e);
							}
						}
					}
					m_strLastPressedKey = strKey;
					m_fLastPressedTime = Time.realtimeSinceStartup;
				}
			}
		}
	}
}
