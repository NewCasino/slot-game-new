/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：SymbolController.cs
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
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;


/// <summary>
/// 文件名:符号控制器
/// 说明:
/// </summary>
public class SymbolController : Controller
{
	public SymbolController(EViewID viewID, int viewInstID, GameObject view, bool active = true, bool native = false)
		: base(viewID, viewInstID, view, active, native)
	{

	}

	List<Reel> listReel = new List<Reel>{ };
    List<SymbolInstance> listSymbolPrefab = new List<SymbolInstance> { };

    /// <summary>
    /// 最后一列卷轴ID
    /// </summary>
    public int lastReelID
    {
        get{ return listReel[listReel.Count - 1].ID; }
    }

    private SymbolViewPresenters ViewSub
	{
		get { return this.View as SymbolViewPresenters; }
	}

	private SymbolModel ModelSub
	{
		get { return this.Model as SymbolModel; }
	}
	/// <summary>
	/// 创建View
	/// </summary>
	protected override ViewPresenters CreateView(GameObject go)
	{
		if (go.GetComponent<SymbolViewPresenters>() == null)
		{
			return go.AddComponent<SymbolViewPresenters>();
		}
		return go.GetComponent<SymbolViewPresenters>();
	}

	/// <summary>
	/// 创建Model
	/// </summary>
	/// <returns></returns>
	protected override Model CreateModel()
	{
		return new SymbolModel();
	}

	protected override void InitPost()
	{
		this.CreateSymbolPrefab();
		this.CreateReel();
		this.CreateSymbolInst();
        this.CreateSymbolAniInst();
    }

	/// <summary>
	/// 添加监听,只对view中InitUI()里的UI有效
	/// </summary>
	protected override void AddListener()
	{

	}
	/// <summary>
	/// 移除监听
	/// </summary>
	protected override void RemoveListener()
	{

	}

	/// <summary>
	/// 生成符号预设
	/// </summary>
	private void CreateSymbolPrefab()
	{		
		for (int i = 0; i < ModelSub.listSymbol.Count; i++)
		{
			GameObject symbolObj = GameObject.Instantiate(ViewSub.symbolPrefab);
			symbolObj.transform.SetParent(ViewSub.prefabPanel.transform);
			symbolObj.transform.ResetTransformExt();
            SymbolInstance symbolInstance = symbolObj.AddComponent<SymbolInstance>();
            symbolInstance.SymbolObj = symbolObj;
            symbolInstance.symbolData = ModelSub.listSymbol[i];
            Sprite sprite = AssetLoadMgr.Instance.LoadNativePrefab<Sprite>(string.Format("{0}/{1}",
                StrDef.PATH_SYMBOLTEXTURE, symbolInstance.symbolData.name));
            symbolInstance.sprite = sprite;
            symbolObj.GetComponent<Image>().sprite = sprite;
            listSymbolPrefab.Add(symbolInstance);
        }
    }

	/// <summary>
	/// 生成卷轴
	/// </summary>
	private void CreateReel()
	{
		for (int i = 0; i < ModelSub.reelCount; i++)
		{
            GameObject reelObj = GameObject.Instantiate(ViewSub.reelPrefab);
			reelObj.transform.SetParent(ViewSub.reelPanel.transform);
			reelObj.transform.ResetTransformExt();
            reelObj.name = string.Format("{0}{1}","Reel",i.ToString());
            Reel reel = reelObj.AddComponent<Reel>();
            reel.obj = reelObj;
            reel.ID = i;
            listReel.Add(reel);
		}
	}

	/// <summary>
	/// 生成符号
	/// </summary>
	private void CreateSymbolInst()
	{
        System.Random random = new System.Random();
        for (int i = 0; i < listReel.Count; i++)
		{
			for (int j = 0; j < ModelSub.symbolCountOnShow; j++)
			{
				int index = random.Next(0, listSymbolPrefab.Count - 1);
                //symbol = CloneUtils.DeepClone<SymbolInstance>(ModelSub.listSymbol[index]);`
                GameObject symbolObj = GameObject.Instantiate(listSymbolPrefab[index].SymbolObj);
				symbolObj.transform.SetParent(listReel[i].obj.transform);
                symbolObj.transform.ResetTransformExt();
				//symbolObj.name = string.Format(ModelSub.listSymbol[index].name);
                symbolObj.name = string.Format(j.ToString());
                SymbolInstance symbolInstance = symbolObj.GetComponent<SymbolInstance>();
                listReel[i].listSymbolObj.Add(symbolInstance);
			}
		}
	}

    /// <summary>
    /// 生成动画符号
    /// </summary>
    private void CreateSymbolAniInst()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < listReel.Count; i++)
        {
            for (int j = 0; j < ModelSub.symbolCountForAni; j++)
            {
                int index = random.Next(0, listSymbolPrefab.Count - 1);
                GameObject symbolObj = GameObject.Instantiate(listSymbolPrefab[index].SymbolObj);
                symbolObj.transform.SetParent(listReel[i].obj.transform);
                symbolObj.transform.ResetTransformExt();
                //symbolObj.name = string.Format(ModelSub.listSymbol[index].name);
                symbolObj.name = string.Format("ani"+j.ToString());
                SymbolInstance symbolInstance = symbolObj.GetComponent<SymbolInstance>();
                listReel[i].listSymbolObjForAni.Add(symbolInstance);
            }
        }
    }

    /// <summary>
    /// 刷新用于动画的符号
    /// </summary>
    public void RefreshAni()
    {
        //int iSeed = i;
        //System.Random random = new System.Random(iSeed);
        System.Random random = new System.Random();
        for (int i = 0; i < listReel.Count; i++)
        {
            for (int j = 0; j < listReel[i].listSymbolObjForAni.Count; j++)
            {
                int index = random.Next(0, listSymbolPrefab.Count - 1);
                Sprite sprite = listSymbolPrefab[index].sprite;
                listReel[i].listSymbolObjForAni[j].SymbolObj.transform.GetComponent<Image>().sprite = sprite;
            }
        }
    }

    /// <summary>
    /// 旋转卷轴
    /// </summary>
    public void SpinReel()
	{
        this.RefreshAni();
        for (int i = 0; i < listReel.Count; i++)
		{
            listReel[i].StartSpin(ModelSub.delayTime *i, ModelSub.speed);
        }
    }

    public void EndSpinReel()
    {

    }

}
