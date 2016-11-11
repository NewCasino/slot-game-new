/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：PayLineController.cs
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
using Slot.UI;
using Vectrosity;

/// <summary>
/// 文件名:赔付线控制器
/// 说明:
/// </summary>
public class PayLineController : Controller
{
	public PayLineController(EViewID viewID, int viewInstID, GameObject view, bool active = true, bool native = false)
		: base(viewID, viewInstID, view, active, native)
	{

	}

	private PayLineViewPresenters ViewSub
	{
		get { return this.View as PayLineViewPresenters; }
	}

	private PayLineModel ModelSub
	{
		get { return this.Model as PayLineModel; }
	}

	public DelegateVoid changeLineCountListener = null;

	/// <summary>
	/// 创建View
	/// </summary>
	protected override ViewPresenters CreateView(GameObject go)
	{
		if (go.GetComponent<PayLineViewPresenters>() == null)
		{
			return go.AddComponent<PayLineViewPresenters>();
		}
		return go.GetComponent<PayLineViewPresenters>();
	}

	/// <summary>
	/// 创建Model
	/// </summary>
	/// <returns></returns>
	protected override Model CreateModel()
	{
		return new PayLineModel();
	}

	protected override void InitPost()
	{
		this.CreatePayLineIcon();
        this.CreatePayLine();

    }

    /// <summary>
    ///  添加监听,只对view中InitUI()里的UI有效
    /// </summary>
    protected override void AddListener()
	{
		PayLineViewPresenters view = this.View as PayLineViewPresenters;

		MainMenuController mainMenuController = UIMgr.Instance.GetView(EViewID.MainMenu) as MainMenuController;

		changeLineCountListener += mainMenuController.UpdateTotalBet;

	}
	/// <summary>
	///  移除监听
	/// </summary>
	protected override void RemoveListener()
	{
		PayLineViewPresenters view = this.View as PayLineViewPresenters;
	}

	/// <summary>
	///  生成赔付线标示
	/// </summary>
	private void CreatePayLineIcon()
	{
		PayLineModel model = this.Model as PayLineModel;
		PayLineViewPresenters view = this.View as PayLineViewPresenters;

		for (int i = 0; i < model.lineCount/2; i++)
		{
			GameObject payLineObj = GameObject.Instantiate(view.payLinePrefab);
			payLineObj.transform.SetParent(view.leftPanel.transform);
			payLineObj.transform.ResetTransformExt();
			view.listPayLineObj.Add(payLineObj);
		}

		for (int i = 0; i < model.lineCount / 2; i++)
		{
			GameObject payLineObj = GameObject.Instantiate(view.payLinePrefab);
			payLineObj.transform.SetParent(view.rightPanel.transform);
			payLineObj.transform.ResetTransformExt();
			view.listPayLineObj.Add(payLineObj);
		}

	}

    /// <summary>
    ///  生成赔付线
    /// </summary>
    private void CreatePayLine()
    {
        Debug.Log(ViewSub.symbol0.transform.localPosition);
        Debug.Log(ViewSub.symbol1.transform.localPosition);

        VectorLine vectorLine = VectorLine.SetLine(Color.blue, ViewSub.symbol0.transform.localPosition,
            ViewSub.symbol1.transform.localPosition);
        vectorLine.SetWidth(3.0f);
        vectorLine.Draw();

    }


    /// <summary>
    ///  增加赔付线
    /// </summary>
    public int GetCurLineCount()
	{
		return ModelSub.CurLineCount;
	}
	/// <summary>
	///  增加赔付线
	/// </summary>
	public int AddLine()
	{
		if (ModelSub.CurLineCount >= ModelSub.lineCount)
		{
			//TODO 音乐
			return ModelSub.CurLineCount;
		}
		ModelSub.CurLineCount++;
		this.changeLineCountListener.Invoke();
		return ModelSub.CurLineCount;
	}

	/// <summary>
	///  减少赔付线
	/// </summary>
	public int SubLine()
	{
		if (ModelSub.CurLineCount <= 1)
		{
			//TODO 音乐
			return ModelSub.CurLineCount;
		}
		ModelSub.CurLineCount--;
		this.changeLineCountListener.Invoke();
		return ModelSub.CurLineCount;
	}

	/// <summary>
	///  增加赔付线到最大值
	/// </summary>
	public int AddMaxLine()
	{
		if (ModelSub.CurLineCount == ModelSub.lineCount)
		{
			return ModelSub.CurLineCount;
		}
		ModelSub.CurLineCount = ModelSub.lineCount;
		this.changeLineCountListener.Invoke();
		return ModelSub.CurLineCount;
	}

}
