using UnityEngine;
using System.Collections;

public class Test_SingletonWithComponent : SingletonWithComponent<Test_SingletonWithComponent>
{

	/// <summary>
	/// 初始化
	/// </summary>
	protected override void InitPre()
	{

	}

	/// <summary>
	/// 后初始化
	/// </summary>
	protected override void InitPost()
	{

	}

	/// <summary>
	/// 清理（多次）
	/// </summary>
	protected override void Clear()
	{

	}

	/// <summary>
	/// 结束（一次）
	/// </summary>
	protected override void Finish()
	{

	}




}
