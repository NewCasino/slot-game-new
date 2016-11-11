/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：MainMenuController.cs
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
using Slot.UI;
using UnityEngine.UI;

/// <summary>
/// 文件名:主菜单控制器
/// 说明:
/// </summary>
public class MainMenuController : Controller
{
	public MainMenuController(EViewID viewID, int viewInstID, GameObject view, bool active = true, bool native = false)
		: base(viewID, viewInstID, view, active, native)
	{

	}

	private MainMenuViewPresenters ViewSub
	{
		get { return this.View as MainMenuViewPresenters; }
	}

	private MainMenuModel ModelSub
	{
		get { return this.Model as MainMenuModel; }
	}

	public DelegateVoid changeLineCountListener = null;


	/// <summary>
	/// 创建View
	/// </summary>
	protected override ViewPresenters CreateView(GameObject go)
	{
		if (go.GetComponent<MainMenuViewPresenters>() == null)
		{
			return go.AddComponent<MainMenuViewPresenters>();
		}
		return go.GetComponent<MainMenuViewPresenters>();
	}

	/// <summary>
	/// 创建Model
	/// </summary>
	/// <returns></returns>
	protected override Model CreateModel()
	{
		return new MainMenuModel();
	}

	protected override void InitPost()
	{
		ViewSub.textBet.text = ModelSub.betCountPerLine.ToString();
		ViewSub.textTotalBet.text = ModelSub.betCountPerLine.ToString();
		PayLineController payLineController = UIMgr.Instance.GetView(EViewID.PayLine) as PayLineController;
		ViewSub.textPayLine.text = payLineController.GetCurLineCount().ToString();
		this.changeLineCountListener.Invoke();

		ViewSub.textChip.text = ModelSub.TotalChipCount.ToString();
	}

	/// <summary>
	///  添加监听,只对view中InitUI()里的UI有效
	/// </summary>
	protected override void AddListener()
	{
		UIEventListen.Get(ViewSub.btnRaiseBet).onClick += RaiseBet;
		UIEventListen.Get(ViewSub.btnLowerBet).onClick += LowerBet;
		UIEventListen.Get(ViewSub.btnAddLine).onClick += AddLine;
		UIEventListen.Get(ViewSub.btnSubLine).onClick += SubLine;

		UIEventListen.Get(ViewSub.btnPayTable).onClick += ShowPayTable;
		UIEventListen.Get(ViewSub.objPayTable).onClick += ClosePayTable;
		
		UIEventListen.Get(ViewSub.btnSpin).onClick += Spin;
		UIEventListen.Get(ViewSub.btnMaxBet).onClick += SpinMaxBet;

		changeLineCountListener += UpdateTotalBet;
	}
	/// <summary>
	///  移除监听
	/// </summary>
	protected override void RemoveListener()
	{
		UIEventListen.Get(ViewSub.btnRaiseBet).onClick -= RaiseBet;
		UIEventListen.Get(ViewSub.btnLowerBet).onClick -= LowerBet;
		UIEventListen.Get(ViewSub.btnAddLine).onClick -= AddLine;
		UIEventListen.Get(ViewSub.btnSubLine).onClick -= SubLine;
		UIEventListen.Get(ViewSub.btnSpin).onClick -= Spin;
	}

	/// <summary>
	///  加注
	/// </summary>
	private void RaiseBet()
	{
		if(ModelSub.betIndex>=ModelSub.listBetCountPerLine.Count-1)
		{
			//TODO 音乐
			return;
		}
		ModelSub.betIndex++;
		ModelSub.betCountPerLine = ModelSub.listBetCountPerLine[ModelSub.betIndex];
		ViewSub.textBet.text = ModelSub.betCountPerLine.ToString();
		this.changeLineCountListener.Invoke();
	}

	/// <summary>
	///  减注
	/// </summary>
	private void LowerBet()
	{
		if (ModelSub.betIndex <= 0)
		{
			//TODO 音乐
			return;
		}
		ModelSub.betIndex--;
		ModelSub.betCountPerLine = ModelSub.listBetCountPerLine[ModelSub.betIndex];
		ViewSub.textBet.text = ModelSub.betCountPerLine.ToString();
		this.changeLineCountListener.Invoke();
	}

	/// <summary>
	///  增加赌注到最大值
	/// </summary>
	private void AddMaxBet()
	{
		if (ModelSub.betIndex == ModelSub.listBetCountPerLine.Count - 1)
		{
			return;
		}
		ModelSub.betIndex = ModelSub.listBetCountPerLine.Count - 1;
		ModelSub.betCountPerLine = ModelSub.listBetCountPerLine[ModelSub.betIndex];
		ViewSub.textBet.text = ModelSub.betCountPerLine.ToString();
		this.changeLineCountListener.Invoke();
	}

	/// <summary>
	///  增加赔付线
	/// </summary>
	private void AddLine()
	{
		PayLineController payLineController = UIMgr.Instance.GetView(EViewID.PayLine) as PayLineController;
		ViewSub.textPayLine.text = payLineController.AddLine().ToString();
	}

	/// <summary>
	///  减少赔付线
	/// </summary>
	private void SubLine()
	{
		PayLineController payLineController = UIMgr.Instance.GetView(EViewID.PayLine) as PayLineController;
		ViewSub.textPayLine.text = payLineController.SubLine().ToString();
	}

	/// <summary>
	///  刷新赌注总量
	/// </summary>
	public void UpdateTotalBet()
	{
		PayLineController payLineController = UIMgr.Instance.GetView(EViewID.PayLine) as PayLineController;
		ModelSub.TotalBetCount = payLineController.GetCurLineCount() * ModelSub.betCountPerLine;
		ViewSub.textTotalBet.text = ModelSub.TotalBetCount.ToString();
	}

	/// <summary>
	///  显示支付表
	/// </summary>
	private void ShowPayTable()
	{
		ViewSub.objPayTable.SetActive(true);
	}

	/// <summary>
	///  关闭支付表
	/// </summary>
	private void ClosePayTable()
	{
		ViewSub.objPayTable.SetActive(false);
	}

	/// <summary>
	///  开始旋转
	///  实际上老虎机的每次旋转都是根据设定的酬付比例随机生成的，机器不会记住自己刚刚吐出大奖的
	/// </summary>
	private void Spin()
	{
		if (ModelSub.TotalBetCount > ModelSub.TotalChipCount)
		{
			//TODO 音乐 筹码不够
			return;
		}
		else
		{
			GameObject objEffectChip = GameObject.Instantiate<GameObject>(ViewSub.objEffectChip);
			objEffectChip.transform.SetParentObjExt(ViewSub.textChip.gameObject);
			objEffectChip.transform.GetComponent<Text>().text = string.Format("-{0}", ModelSub.TotalBetCount.ToString());
			objEffectChip.SetActive(true);
			ModelSub.TotalChipCount -= ModelSub.TotalBetCount;
			ViewSub.textChip.text = ModelSub.TotalChipCount.ToString();
			
			//旋转卷轴
			SymbolController symbolController = UIMgr.Instance.GetView(EViewID.Symbol) as SymbolController;
			symbolController.SpinReel();
		}
	}

	/// <summary>
	/// 最大赌注进行游戏
	/// </summary>
	private void SpinMaxBet()
	{
		PayLineController payLineController = UIMgr.Instance.GetView(EViewID.PayLine) as PayLineController;
		int maxLineCount = payLineController.AddMaxLine();
		ViewSub.textPayLine.text = maxLineCount.ToString();
		this.AddMaxBet();
		this.changeLineCountListener.Invoke();
		this.Spin();
	}
	

}
