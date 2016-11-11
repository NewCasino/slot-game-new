/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：TemplateController.cs
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

/// <summary>
/// 文件名:示例控制器
/// 说明:
/// </summary>
public class TemplateController : Controller
{
	public TemplateController(EViewID viewID, int viewInstID, GameObject view, bool active = true, bool native = false)
		: base(viewID, viewInstID, view, active, native)
	{

	}

	private TemplateViewPresenters ViewSub
	{
		get { return this.View as TemplateViewPresenters; }
	}

	private TemplateModel ModelSub
	{
		get { return this.Model as TemplateModel; }
	}
	/// <summary>
	/// 创建View
	/// </summary>
	protected override ViewPresenters CreateView(GameObject go)
	{
		if (go.GetComponent<TemplateViewPresenters>() == null)
		{
			return go.AddComponent<TemplateViewPresenters>();
		}
		return go.GetComponent<TemplateViewPresenters>();
	}

	/// <summary>
	/// 创建Model
	/// </summary>
	/// <returns></returns>
	protected override Model CreateModel()
	{
		return new TemplateModel();
	}

	protected override void InitPost()
	{

	}

	/// <summary>
	///  添加监听,只对view中InitUI()里的UI有效
	/// </summary>
	protected override void AddListener()
	{
		TemplateViewPresenters view = this.View as TemplateViewPresenters;
	}
	/// <summary>
	///  移除监听
	/// </summary>
	protected override void RemoveListener()
	{
		TemplateViewPresenters view = this.View as TemplateViewPresenters;
	}

}
