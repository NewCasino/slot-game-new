/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：MainMenuModel.cs
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
using System.Collections.Generic;

/// <summary>
/// 文件名：主菜单模型层
/// 说明:
/// </summary>
public class MainMenuModel : Model
{
	#region 赌注
	//下注限额
	public float limit;
	//每条赔付线赌注
	public float betCountPerLine;
	public List<float> listBetCountPerLine;
	//档位
	public int betIndex = 0;
	//赌注
	public float TotalBetCount;

	#endregion
	//玩家筹码总数
	public float TotalChipCount;

	/// <summary>
	/// 初始化
	/// </summary>
	protected override void Init()
	{
		listBetCountPerLine = new List<float> {0.25f,1f,2f,5f,10f,20f};
		betIndex = 0;
		betCountPerLine = listBetCountPerLine[betIndex];
		TotalChipCount = 1000f;
	}
}
