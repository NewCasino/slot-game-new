/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：UIComponentExt.cs
* 版权所有：	
* 文件编号：
* 创 建 人：Tycho
* 创建日期：2016-11-7
* 修 改 人：
* 修改日期：
* 描	述：业务逻辑类
* 版 本 号：1.0
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Slot.UI
{
	/// <summary>
	/// 文件名:UI组件扩展
	/// 说明:
	/// </summary>
	public static class UIComponentExt
	{

		#region Transform
		/// <summary>
		/// 查找窗口下的UI组件
		/// </summary>
		/// <returns></returns>
		public static T FindUIExt<T>(this Transform trans, string strChildName, GameObject parentObj = null, bool includeInactive = true) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(strChildName))
			{
				Debug.LogError(string.Format("Please set the UI name first"));
				return default(T);
			}
			if (parentObj == null)
			{
				parentObj = trans.gameObject;
			}
			T childComp = default(T);
			if (typeof(T).IsSubclassOf(typeof(Component)))   //说明是自定义的类型，无需另外再封装
			{
				childComp = FindDeepChildComp<T>(strChildName, parentObj, includeInactive) as T;
			}
			else if (typeof(T) == typeof(GameObject))
			{
				childComp = FindDeepChildObj(strChildName, parentObj, includeInactive) as T;
			}
			else
			{
				Debug.LogWarning(string.Format("This type [{0}] is not supported", typeof(T)));
			}
			return childComp;
		}

		/// <summary>
		/// 查找子节点组件
		/// </summary>
		private static T FindDeepChildComp<T>(string strChildName, GameObject target, bool includeInactive = true) where T : UnityEngine.Object
		{
			if ((target == null) || string.IsNullOrEmpty(strChildName))
			{
				return default(T);
			}
			Component[] arrayAllComponent = target.transform.GetComponentsInChildren(typeof(T), includeInactive);

			//T[] arrayAllComponent = target.transform.GetComponentsInChildren<T>(includeInactive);
			List<Component> listComponent = new List<Component>(arrayAllComponent).Where(m => m.name.Equals(strChildName)).ToList();
			if (listComponent.Count == 0)
			{
				Debug.LogError(string.Format("The component [{0}] is not exist ", strChildName));
			}
			if (listComponent.Count > 1)
			{
				Debug.LogError(string.Format("The component [{0}] has more than one ", strChildName));
			}
			return listComponent[0] as T;
		}

		/// <summary>
		/// 查找子节点物体
		/// </summary>
		private static GameObject FindDeepChildObj(string strChildName, GameObject target, bool includeInactive = true)
		{
			if ((target == null) || string.IsNullOrEmpty(strChildName))
			{
				return null;
			}
			Transform[] arrayAllObj = target.transform.GetComponentsInChildren<Transform>(includeInactive);
			List<Transform> listObj = new List<Transform>(arrayAllObj).Where(m => m.name.Equals(strChildName)).ToList();
			if (listObj.Count == 0)
			{
				Debug.LogError(string.Format("The gameObject [{0}] is not exist ", strChildName));
			}
			if (listObj.Count > 1)
			{
				Debug.LogError(string.Format("The gameObject [{0}] has more than one ", strChildName));
			}
			return listObj[0].gameObject;
		}

		/// <summary>
		/// 设置父节点
		/// </summary>
		public static void SetParentObjExt(this Transform trans, GameObject target)
		{
			if (target != null)
			{
				trans.SetParent(target.transform, false);
			}
			else
			{
				Debug.LogError(string.Format("The [{0}] is not exist", target.name));
			}
		}

		/// <summary>
		/// 查找子节点物体
		/// </summary>
		public static void ResetTransformExt(this Transform trans)
		{
			trans.localScale = Vector3.one;
		}
		#endregion
	}
}
