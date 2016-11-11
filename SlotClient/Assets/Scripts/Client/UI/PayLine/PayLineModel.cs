/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：PayLineModel.cs
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

/// <summary>
/// 文件名：赔付线模型层
/// 说明:
/// </summary>
public class PayLineModel : Model
{
	#region 赔付线
	//赔付线总数 9~50
	public int lineCount;

	//当前赔付线数量
	public int CurLineCount;

	List<int> LineRank; 
	#endregion

	/// <summary>
	/// 初始化
	/// </summary>
	protected override void Init()
	{
		lineCount = 20;
		CurLineCount = 1;
	}

}
