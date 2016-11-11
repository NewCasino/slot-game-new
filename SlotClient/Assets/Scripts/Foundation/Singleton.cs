/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：Singleton.cs
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

/// <summary>
/// 文件名:线程安全单例
/// 说明：
/// </summary>
public abstract class Singleton<T> where T : Singleton<T>, new()
{
	private static volatile T instance;
	// Creates an syn object.
	private static readonly object SynObject = new object();

	public static T Instance
	{
		get
		{
			// Double-Checked Locking
			if (null == instance)
			{
				lock (SynObject)
				{
					if (null == instance)
					{
						instance = new T();
						instance.Init();
					}
				}
			}
			return instance;
		}
	}

	/// <summary>
	/// 析构
	/// </summary>
	~Singleton()
	{
		Finish();
	}

	/// <summary>
	/// 初始化
	/// </summary>
	protected abstract void Init();

	/// <summary>
	/// 清理（多次）
	/// </summary>
	protected abstract void Clear();

	/// <summary>
	/// 结束（一次）
	/// </summary>
	protected abstract void Finish();

}
