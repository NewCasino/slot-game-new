/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：ModelMgr.cs
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
using Slot.Utils;

/// <summary>
/// 文件名:模型管理器
/// 说明：子管理器
/// </summary>
public class ModelMgr : SingletonWithComponent<ModelMgr>
{
	private Dictionary<string,AssetBundle> modelDic = new Dictionary<string,AssetBundle>();
	/// <summary>
	/// 初始化
	/// </summary>
	protected override void InitPre()
	{
		modelDic.Clear();
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
	/// 加载模型
	/// </summary>
	public delegate void ModelCallback(string name, GameObject model, object callbackData);
	public void LoadModel(string name, ModelCallback callback0, object callbackData0 = null)
	{
		string assetFormat = "prefab";
		string fullPath = FileUtils.GetResourcePath(string.Format("{0}.{1}",name,assetFormat));
		if (this.modelDic.ContainsKey(fullPath))
		{
			AssetBundle assetBundle = this.modelDic[fullPath];
			GameObject obj = GameObject.Instantiate(assetBundle.LoadAsset(assetBundle.GetAllAssetNames()[0])) as GameObject;
			if (callback0 != null)
			{
				callback0(name, obj, callbackData0);
			}
		}
		else
		{
			AssetLoadMgr.Instance.LoadAsset(fullPath, new AssetLoadMgr.AssetCallback(delegate (string path, WWW asset, object callbackData)
			{
				if (asset != null)
				{
					//GameObject obj = GameObject.Instantiate(asset.assetBundle.LoadAsset(asset.assetBundle.GetAllAssetNames()[0])) as GameObject;
					GameObject obj = GameObject.Instantiate(asset.assetBundle.mainAsset) as GameObject;
					this.modelDic[fullPath] = asset.assetBundle;
					if (callback0 != null)
					{
						callback0(name, obj, callbackData0);
					}
				}
				else
				{
					if (callback0 != null)
					{
						callback0(name, null, callbackData0);
					}
				}
			}), null);
		}
	}
}
