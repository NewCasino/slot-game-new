/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：PayLineViewPresenters.cs
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

/// <summary>
/// 文件名:赔付线视图层
/// 说明:
/// </summary>
public class PayLineViewPresenters : ViewPresenters
{
	public Text textBet = null;
	public Button btnRaiseBet = null;

	public RectTransform leftPanel = null;
	public RectTransform rightPanel = null;

	public GameObject payLinePrefab;

	public List<GameObject> listPayLineObj = new List<GameObject>();
    public GameObject symbol0;
    public GameObject symbol1;


    protected override void InitUI()
	{
		leftPanel = viewPanel.FindUIExt<RectTransform>("LeftPanel");
		rightPanel = viewPanel.FindUIExt<RectTransform>("RightPanel");

		payLinePrefab = viewPanel.FindUIExt<GameObject>("PayLinePrefab");

        symbol0 = viewPanel.FindUIExt<GameObject>("position(0,0)");
        symbol1 = viewPanel.FindUIExt<GameObject>("position(1,0)");


    }
}
