/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：FileUtils.cs
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

namespace Slot.Utils
{
	/// <summary>
	/// 文件名:文件工具
	/// 说明：获取各个路径
	/// </summary>
	public class FileUtils
	{
		/// <summary>
		/// 得到StreamingAssets目录下的文件全路径,可以通过www加载
		/// </summary>
		/// <returns>The WWW file path.</returns>
		/// <param name="relPath">Rel path.</param>
		public static string GetStreamingWWWFilePath(string relPath)
		{
			//if (Application.platform == RuntimePlatform.Android)
			//{
			//	return ("file://" + Application.persistentDataPath + "/" + relPath);
			//}
			//else
			//{
			//	return "file://" + Application.streamingAssetsPath + "/" + relPath;
			//}
			return "file://" + Application.streamingAssetsPath + "/" + relPath;
		}

		/// <summary>
		/// 得到StreamingAssets目录下的文件全路径,可以通过C#加载
		/// </summary>
		public static string GetStreamingCFilePath(string relPath)
		{
			return Application.streamingAssetsPath + "/" + relPath;
		}

		/// <summary>
		/// 得到Persistent目录下的文件全路径,可以通过C#加载
		/// </summary>
		public static string GetPersistentCFilePath(string relPath)
		{
			return (Application.persistentDataPath + "/" + relPath);
		}

		/// <summary>
		/// 得到Temporary目录下的文件全路径,可以通过C#加载
		/// </summary>
		public static string GetTemporaryCFilePath(string relPath)
		{
			return Application.temporaryCachePath + "/" + relPath;
		}

		/// <summary>
		/// 服务器资源路径
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetResourcePath(string name)
		{
			string path = string.Empty;
			if (ApplicationMgr.Instance.IsInternal())
			{
				path = string.Format(@"Data/{0}", name);
				path = GetStreamingWWWFilePath(path);
				Debug.Log(string.Format(@"移动端上资源沙盒路径:{0}", path));
			}
			else
			{
				if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.Android)
				{
					path = string.Format(@"file:///{0}/Data/{1}", Application.persistentDataPath, name);
					Debug.Log(string.Format(@"PC上资源应用数据路径:{0}", path));
				}
				else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.IPhonePlayer)
				{
					path = string.Format(@"file:///{0}/Data/{1}", Application.temporaryCachePath, name);
					Debug.Log(string.Format(@"PC上资源应用数据路径:{0}", path));
				}
			}

			return path;
		}

	}
}
