/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：UIEventListen.cs
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
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 文件名:事件监听组件
/// 说明:
/// </summary>
public class UIEventListen : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,  IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
{
	#region  无参
	public DelegateVoid onClick;                        //点击事件代理;   
	public DelegateVoid onDown;                         //按下事件代理;
    public DelegateVoid onEnter;                        //点击进入物体事件代理;
    public DelegateVoid onExit;                         //点击退出事件代理;
    public DelegateVoid onUp;                           //点击抬起事件代理;
    public DelegateVoid onSelect;                       //选中事件代理;
    public DelegateVoid onDeSelect;                     //取消选中事件代理;
    public DelegateVoid onUpdateSelect;                 //选中更新便件代理;
    public DelegateVoid onDragBegin;                    //拖动开始事件代理;
    public DelegateVoid onDrag;                         //拖动事件代理;
    public DelegateVoid onDragEnd;                      //拖动结束事件代理;
    public DelegateVoid onDrop;                         //拖动释放事件代理;
    public DelegateVoid onSubmit;                       //提交事件代理;
    public DelegateVoid onCancel;                       //取消按钮事件代理;
    public DelegateVoid onScroll;                       //滚动事件代理;

	#endregion

	#region  带参 System.Object
	public DelegateInt onClickInt;                      //点击事件代理;   
	public DelegateInt onDownInt;                       //按下事件代理;
	public DelegateInt onEnterInt;                      //点击进入物体事件代理;
	public DelegateInt onExitInt;                       //点击退出事件代理;
	public DelegateInt onUpInt;                         //点击抬起事件代理;
	public DelegateInt onSelectInt;                     //选中事件代理;
	public DelegateInt onDeSelectInt;                   //取消选中事件代理;
	public DelegateInt onUpdateSelectInt;               //选中更新便件代理;
	public DelegateInt onDragBeginInt;                  //拖动开始事件代理;
	public DelegateInt onDragInt;                       //拖动事件代理;
	public DelegateInt onDragEndInt;                    //拖动结束事件代理;
	public DelegateInt onDropInt;                       //拖动释放事件代理;
	public DelegateInt onSubmitInt;                     //提交事件代理;
	public DelegateInt onCancelInt;                     //取消按钮事件代理;
	public DelegateInt onScrollInt;                     //滚动事件代理;

	#endregion

	/// 添加事件监听
	/// </summary>
	/// <param name="go">被监听对象</param>
	public static UIEventListen Get(GameObject go)
    {
        UIEventListen listener = go.GetComponent<UIEventListen>();
        if (listener == null) listener = go.AddComponent<UIEventListen>();
        return listener;
    }

	/// 添加事件监听
	/// </summary>
	/// <param name="go">被监听对象</param>
	public static UIEventListen Get(Component Comp)
	{
		UIEventListen listener = Comp.gameObject.GetComponent<UIEventListen>();
		if (listener == null) listener = Comp.gameObject.AddComponent<UIEventListen>();
		return listener;
	}

	/// <summary>
	/// 拖动开始
	/// </summary>
	public void OnBeginDrag(PointerEventData eventData) 
    {
        if (onDragBegin != null) { onDragBegin(); }
    }

	/// <summary>
	/// 取消按钮
	/// </summary>
	public void OnCancel(BaseEventData eventData)
    {
        if (onCancel != null) { onCancel(); }
    }

	/// <summary>
	/// 取消选中
	/// </summary>
	public void OnDeselect(BaseEventData eventData)
    {
        if (onDeSelect != null) { onDeSelect(); }
    }

	/// <summary>
	///拖动
	/// </summary>
	public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) { onDrag(); }
    }

	/// <summary>
	/// 拖动释放
	/// </summary>
	public void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null) { onDrop(); }
    }

	/// <summary>
	/// 拖动结束
	/// </summary>
	public void OnEndDrag(PointerEventData eventData)
    {
        if (onDragEnd != null) { onDragEnd(); }
    }

	public void OnInitializePotentialDrag(PointerEventData eventData)
    {

    }

	public void OnMove(AxisEventData eventData)
    {

    }

	/// <summary>
	/// 点击事件操作
	/// </summary>
	public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) { onClick(); }
    }

	/// <summary>
	/// 点击按下
	/// </summary>
	public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) { onDown(); }
    }

	/// <summary>
	/// 点击进入
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) { onEnter(); }
    }

	/// <summary>
	/// 点击退出
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) { onExit(); }
    }

	/// <summary>
	/// 点击抬起
	/// </summary>
	public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) { onUp(); }
    }

	public void OnScroll(PointerEventData eventData)
    {
        if (onScroll != null) { onScroll(); }
    }

	/// <summary>
	/// 选中状态
	/// </summary>
	public void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) { onSelect(); }
    }

	/// <summary>
	/// 提交按钮
	/// </summary>
	public void OnSubmit(BaseEventData eventData)
    {
        if (onSubmit != null) { onSubmit(); }
    }

	/// <summary>
	/// 
	/// </summary>
	public void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) { onUpdateSelect(); }
    }

}
