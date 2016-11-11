/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：SymbolViewPresenters.cs
* 版权所有：	
* 文件编号：
* 创 建 人：Tycho
* 创建日期：2016-11-5
* 修 改 人：
* 修改日期：
* 描	述：业务逻辑类
* 版 本 号：1.0
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Slot.UI;
using DG.Tweening;

/// <summary>
/// 文件名:符号视图层
/// 说明:
/// </summary>
public class SymbolViewPresenters : ViewPresenters
{
	public GameObject prefabPanel;
	public GameObject symbolPrefab;
	public GameObject reelPanel;
	public GameObject reelPrefab;

	protected override void InitUI()
	{
		prefabPanel = viewPanel.FindUIExt<GameObject>("PrefabPanel");
		symbolPrefab = viewPanel.FindUIExt<GameObject>("SymbolPrefab");

		reelPanel = viewPanel.FindUIExt<GameObject>("ReelPanel");
		reelPrefab = viewPanel.FindUIExt<GameObject>("ReelPrefab");

	}

}
