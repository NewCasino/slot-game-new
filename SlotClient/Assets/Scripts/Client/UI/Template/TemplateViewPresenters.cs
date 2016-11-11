/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：TemplateViewPresenters.cs
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
/// 文件名:示例视图层
/// 说明:
/// </summary>
public class TemplateViewPresenters : ViewPresenters
{
	public Button btnRaiseBet = null;

	protected override void InitUI()
	{
		btnRaiseBet = viewPanel.FindUIExt<Button>("Button Raise Bet");
	}
}
