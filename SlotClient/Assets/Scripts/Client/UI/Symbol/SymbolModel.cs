/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：SymbolModel.cs
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
using System.ComponentModel;
using System.Collections.Generic;
using Slot.Utils;

/// <summary>
/// 文件名：符号模型层
/// 说明:
/// </summary>
public class SymbolModel : Model
{
	//普通符号数量,8个以上
	public int SymbolCount;

	//分散符号数量,不一定需要出現在特定的赔付线上，3个级以上scatter触发免费旋转。
	//3 or more on any reel starts the Free Spin.
	public int ScatterSymbolCount;

    //列数
    public int reelCount = 5;
    //每列显示的符号数
    public int symbolCountOnShow = 3;
    //每列用于动画的符号数
    public int symbolCountForAni = 30;
    //动画速度
    public float speed = 0.0003f;
    //每列动画延迟时间
    public float delayTime = 0.2f;
    
    public List<SymbolData> listSymbol = new List<SymbolData> { };

	/// <summary>
	/// 初始化
	/// </summary>
	protected override void Init()
	{
		SymbolCount = 8;
		ScatterSymbolCount = 1;
		initSymbol();
	}

	private void initSymbol()
	{
        //string path = FileUtils.GetStreamingCFilePath(string.Format(StrDef.PATH_SYMBOLCONFIG));
        listSymbol = SymbolDAL.GetSymbol();
	}
}

