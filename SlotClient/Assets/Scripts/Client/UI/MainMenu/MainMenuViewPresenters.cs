/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：MainMenuViewPresenters.cs
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
using UnityEngine.UI;
using Slot.UI;

/// <summary>
/// 文件名:主菜单视图层
/// 说明:
/// </summary>
public class MainMenuViewPresenters : ViewPresenters
{
	/// <summary>
	/// Bet
	/// </summary>
	public Text textBet = null;
	public Button btnRaiseBet = null;
	public Button btnLowerBet = null;

	/// <summary>
	/// PayLine
	/// </summary>
	public Text textPayLine = null;
	public Button btnAddLine = null;
	public Button btnSubLine = null;

	public Text textTotalBet = null;
	public Button btnSpin = null;
	public Button btnMaxBet = null;

	public Text textChip = null;
	public GameObject objEffectChip = null;

	public Button btnPayTable = null;
	public GameObject objPayTable = null;

	protected override void InitUI()
	{
		#region Bet
		textBet = viewPanel.FindUIExt<Text>("Text Bet");
		btnRaiseBet = viewPanel.FindUIExt<Button>("Button Raise Bet");
		btnLowerBet = viewPanel.FindUIExt<Button>("Button Lower Bet");
		#endregion

		#region PayLine
		textPayLine = viewPanel.FindUIExt<Text>("Text PayLine");
		btnAddLine = viewPanel.FindUIExt<Button>("Button Line +");
		btnSubLine = viewPanel.FindUIExt<Button>("Button Line -");
		#endregion

		textTotalBet = viewPanel.FindUIExt<Text>("Text TotalBet");
		btnSpin = viewPanel.FindUIExt<Button>("Button Spin");
		btnMaxBet = viewPanel.FindUIExt<Button>("Button Max Bet");

		textChip = viewPanel.FindUIExt<Text>("Text Chip");

		btnPayTable = viewPanel.FindUIExt<Button>("Button Pay Table");
		objPayTable = viewPanel.FindUIExt<GameObject>("Pay Table");

		objEffectChip = viewPanel.FindUIExt<GameObject>("Text Effect Chip");

	}

	/// <summary>
	/// 失去筹码特效
	/// </summary>
	public void ChipEffect()
	{
		//EffectBase
		//textEffectChip
	}

}
