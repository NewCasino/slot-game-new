/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：ViewConfig.cs
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
using System.IO;
using System;
using System.Collections.Generic;
using Slot.Utils;

/// <summary>
/// 窗口ID定义
/// </summary>
public enum EViewID
{
	None = 0,
	MainMenu = 1001,
	PayLine = 1002,
	Symbol = 1003
}

/// <summary>
/// 窗口实例化类型
/// </summary>
public enum EViewInstType
{
	None,
	Single,     //只能实例化一个
	Multi,      //允许实例化多个
}

/// <summary>
/// 文件名:视图配置类
/// 说明:
/// </summary>
public class ViewConfig: Singleton<ViewConfig>
{

	private Dictionary<EViewID, ViewInfo> viewInfoDic = new Dictionary<EViewID, ViewInfo>();
	private Dictionary<EViewID, GameObject> viewPrefabDic = new Dictionary<EViewID, GameObject>();

	public class ViewInfo
	{
		public string prefabPath;
		public EViewInstType instType;
	}
	/// <summary>
	/// 初始化
	/// </summary>
	protected override void Init()
	{
		InitConfig();
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
	/// 初始化视图配置信息
	/// </summary>
	private void InitConfig()
	{
		string fullPath = FileUtils.GetStreamingCFilePath(string.Format(StrDef.PATH_VIEWCONFIG));

		if (!File.Exists(fullPath))
		{
			Debug.LogError(string.Format("The file [{0}] is not exist!!", fullPath));
			throw new Exception(string.Format("The file [{0}] is not exist!!", fullPath));
		}
		try
		{
			TextReader txtreader = new StreamReader(fullPath);

			string strLine = txtreader.ReadLine();
			for (; strLine != null; strLine = txtreader.ReadLine())
			{
				strLine = strLine.Trim();

				if (strLine != "")
				{
					if (strLine[0] == '#')
					{
						continue;
					}

					string[] keyPair = strLine.Split(new char[] { '=' }, 2);
					if (keyPair.Length < 2)
						continue;

					string strKey = keyPair[0];

					int iKey = 0;
					if (!int.TryParse(strKey, out iKey))
					{
						continue;
					}
					EViewID dlgid = (EViewID)Enum.ToObject(typeof(EViewID), iKey);

					if (!viewInfoDic.ContainsKey(dlgid))
					{
						string[] valPair = keyPair[1].Split(new char[] { '|' });
						ViewInfo info = new ViewInfo();
						info.instType = (EViewInstType)Enum.Parse(typeof(EViewInstType), valPair[0]);
						info.prefabPath = valPair[1];
						viewInfoDic.Add(dlgid, info);
					}
					else
					{
						Debug.LogWarning(string.Format("The key [{0}],value:[{1}] has already been existed",iKey, keyPair[1]));
					}
				}
			}

		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	/// <summary>
	/// 获取窗口实例化类型
	/// </summary>
	public EViewInstType GetViewInstType(EViewID viewID)
	{
		ViewInfo viewInfo = null;
		if (!viewInfoDic.TryGetValue(viewID, out viewInfo))
		{
			Debug.LogWarning(string.Format("The EDialogID [{0}] is not configed", viewID.ToString()));
			return EViewInstType.None;
		}
		return viewInfo.instType;
	}

	public GameObject GetViewPrefab(EViewID viewID)
	{
		if (viewPrefabDic.ContainsKey(viewID))
		{
			return viewPrefabDic[viewID];
		}

		string strUIPrefab = "";
		ViewInfo viewInfo = null;
		if (!viewInfoDic.TryGetValue(viewID, out viewInfo))
		{
			Debug.LogWarning(string.Format("The EDialogID [{0}] is not configed", viewID.ToString()));
			return null;
		}

		string viewPrefabPath = string.Format("{0}/{1}", StrDef.VIEWDIR, viewInfo.prefabPath);
		GameObject viewPrefab = AssetLoadMgr.Instance.LoadNativePrefab<GameObject>(viewPrefabPath);

		if (viewPrefab == null)
		{
			Debug.LogError(string.Format("The view [{0}] is not exists", viewPrefab.name));
			return null;
		}
		viewPrefabDic.Add(viewID, viewPrefab);
		return viewPrefab;
	}

	public Controller CreateViewInstance(EViewID viewID,int viewInstID,GameObject viewObj)
	{
		Controller controller = null;

		switch (viewID)
		{
			case EViewID.MainMenu: controller = new MainMenuController(viewID, viewInstID, viewObj); break;
			case EViewID.PayLine: controller = new PayLineController(viewID, viewInstID, viewObj); break;
			case EViewID.Symbol: controller = new SymbolController(viewID, viewInstID, viewObj); break;
		}

		if (!controller.IsLoaded)
		{
			Debug.LogError(string.Format("The View viewID:[{0}] viewInstID:[{1}] Init fail", viewID, viewInstID));
			GameObject.Destroy(viewObj);
			controller = null;
			return null;
		}

		return controller;
	}
}
