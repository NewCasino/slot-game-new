using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class AppUtility
{

	/// <summary>
	/// 修改子节点Layer  NGUITools.SetLayer();
	/// </summary>
	public static void ChangeLayer(Transform t, int layer, bool bIncludeChild)
	{
		t.gameObject.layer = layer;
		if (bIncludeChild)
		{
			for (int i = 0; i < t.childCount; ++i)
			{
				Transform child = t.GetChild(i);
				child.gameObject.layer = layer;
				ChangeLayer(child, layer, true);
			}
		}
	}

	/// <summary>
	/// 从自己开始往父节点寻找指定层的父亲
	/// </summary>
	/// <param name="trans"></param>
	/// <param name="layer"></param>
	/// <returns></returns>
	public static Transform FindSpecificFather(Transform trans, int layer)
	{
		Transform transRst = null;
		for (; trans != null; trans = trans.parent)
		{
			if (trans.gameObject.layer == layer)
			{
				transRst = trans;
				break;
			}
		}
		return transRst;
	}


	/// <summary>
	/// 从自己开始往父节点寻找指定tag的父亲
	/// </summary>
	/// <param name="trans"></param>
	/// <param name="tag"></param>
	/// <returns></returns>
	public static Transform FindSpecificFatherByTag(Transform trans, params string[] tag)
	{
		if (tag == null || tag.Length == 0) return null;

		for (; trans != null; trans = trans.parent)
		{
			for (int i = 0; i < tag.Length; i++)
			{
				if (trans.gameObject.tag == tag[i])
				{
					return trans;
				}
			}
		}

		return null;
	}

	/// <summary>
	/// 从自己开始往父节点寻找指定tag的父亲
	/// </summary>
	/// <param name="trans"></param>
	/// <param name="layer"></param>
	/// <returns></returns>
	public static Transform FindSpecificFatherByLayer(Transform trans, int layer)
	{
		Transform transRst = null;
		for (; trans != null; trans = trans.parent)
		{
			if (trans.gameObject.layer == layer)
			{
				transRst = trans;
				break;
			}
		}
		return transRst;
	}

	/// <summary>
	/// 创建UI对象
	/// </summary>
	/// <param name="name"></param>
	/// <param name="parent"></param>
	/// <returns></returns>
	public static GameObject CreateUIObject(string name, GameObject parent)
	{
		GameObject go = new GameObject(name);
		go.AddComponent<RectTransform>();
		SetParentAndAlign(go, parent);
		return go;
	}

	private static void SetParentAndAlign(GameObject child, GameObject parent)
	{
		if (parent == null)
			return;

		child.transform.SetParent(parent.transform, false);
		SetLayerRecursively(child, parent.layer);
	}

	private static void SetLayerRecursively(GameObject go, int layer)
	{
		go.layer = layer;
		Transform t = go.transform;
		for (int i = 0; i < t.childCount; i++)
			SetLayerRecursively(t.GetChild(i).gameObject, layer);
	}

	public static Transform FindSpecificChildByTag(Transform trans, string tag)
	{
		Transform resultTrs = null;
		resultTrs = (trans.tag == tag) ? trans : null;
		if (resultTrs != null)
			return resultTrs;

		foreach (Transform trs in trans.transform)
		{
			resultTrs = AppUtility.FindSpecificChildByTag(trs, tag);
			if (resultTrs != null)
				return resultTrs;
		}
		return resultTrs;
	}

	public static bool CheckChildTransform(Transform parent, Transform curr)
	{
		for (; curr != null; curr = curr.parent)
		{
			if (curr == parent)
				return true;
		}
		return false;
	}

	public static void InitTransfrom(Transform tran)
	{
		tran.localPosition = Vector3.zero;
		tran.localRotation = Quaternion.identity;
		tran.localScale = Vector3.one;
	}

	public static float AdjustAngle(float angle)
	{
		angle = angle > 360 ? angle - 360 : angle;
		angle = angle < -360 ? angle + 360 : angle;
		return angle;
	}

	public static Vector3 AdjustAngle(Vector3 angle)
	{
		angle.x = angle.x > 360 ? angle.x % 360 : angle.x;
		angle.x = angle.x < -360 ? angle.x % 360 : angle.x;

		angle.y = angle.y > 360 ? angle.y % 360 : angle.y;
		angle.y = angle.y < -360 ? angle.y % 360 : angle.y;

		angle.z = angle.z > 360 ? angle.z % 360 : angle.z;
		angle.z = angle.z < -360 ? angle.z % 360 : angle.z;
		return angle;
	}

	/// <summary>
	/// 判断一个给定的3D坐标点是否在屏幕显示范围内
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public static bool CheckPositionInsideScene(Vector3 pos)
	{
		Vector3 posscreen = Camera.main.WorldToScreenPoint(pos);
		if (posscreen.x < 0 || posscreen.y < 0 || posscreen.x > Screen.width || posscreen.y > Screen.height)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// 打开文件选择器
	/// </summary>
	/// <param name="defaultPath">文件的完整路径</param>
	/// <param name="fileFilter"></param>
	/// <param name="title">Title</param>
	/// <returns></returns>
	public static string OpenFilePicker(string fileFilter, string defaultPath = "", string title = "选择文件")
	{
		string result = string.Empty;
		OpenFileName ofn = new OpenFileName();
		ofn.structSize = System.Runtime.InteropServices.Marshal.SizeOf(ofn);
		ofn.filter = fileFilter;// "PPT Files(*.pptx;*.3dpx)\0*.pptx;*.3dpx";

		char[] maxPathChar = new char[256];
		//char[] pathChar = defaultPath.ToCharArray();
		//for (int i = 0; i < pathChar.Length; i++)
		//{
		//    if (i < maxPathChar.Length)
		//    {
		//        maxPathChar[i] = pathChar[i];
		//    }
		//}

		ofn.file = new string(maxPathChar);
		ofn.maxFile = ofn.file.Length;
		ofn.fileTitle = new string(new char[64]);
		ofn.maxFileTitle = ofn.fileTitle.Length;
		ofn.initialDir = string.IsNullOrEmpty(defaultPath) ? Application.dataPath : defaultPath;//默认路径
		ofn.title = title;
		//ofn.defExt = "PPTX";//显示文件的类型
		//注意 一下项目不一定要全选 但是0x00000008项不要缺少
		ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 /*| 0x00000200*/ | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
																					   //浏览|文件必须存在|路径必须存在|允许选择多个文件|不改变当前路径
		if (DllOpenFileDialog.GetOpenFileName(ofn))
		{
			result = ofn.file;
		}

		return result;
	}

	/// <summary>
	/// 计算GameObject的中心点
	/// </summary>
	/// <param name="gameObject"></param>
	/// <returns></returns>
	public static Vector3 CalcCenterScenePosition(GameObject gameObject)
	{
		Transform parent = gameObject.transform;
		Vector3 postion = parent.position;
		Quaternion rotation = parent.rotation;
		Vector3 scale = parent.localScale;
		parent.position = Vector3.zero;
		parent.rotation = Quaternion.Euler(Vector3.zero);
		parent.localScale = Vector3.one;

		Vector3 center = Vector3.zero;
		Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
		if (renders.Length > 0)
		{
			foreach (Renderer child in renders)
			{
				center += child.bounds.center;
			}
			center /= renders.Length;
		}

		parent.position = postion;
		parent.rotation = rotation;
		parent.localScale = scale;

		if (Terrain.activeTerrain != null)
		{
			Collider terrainCollider = Terrain.activeTerrain.GetComponent<Collider>();
			if (terrainCollider == null)
			{
				return new Vector3(Camera.main.transform.position.x, Terrain.activeTerrain.SampleHeight(center + parent.position) + 10, Camera.main.transform.position.z);
			}
			else
			{
				return new Vector3(terrainCollider.bounds.center.x, Terrain.activeTerrain.SampleHeight(center + terrainCollider.transform.position) + 15, terrainCollider.bounds.center.z);
			}
		}

		return center + parent.position;
	}

	public static Vector3 CalcCenterPosition(Transform target)
	{
		Vector3 center = Vector3.zero;
		Renderer[] renders = target.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in renders)
		{
			Vector3 cldCenter = child.bounds.center;

			if (child is SkinnedMeshRenderer)
			{
				cldCenter = (child as SkinnedMeshRenderer).sharedMesh.bounds.center;
				Vector3 scalechild = child.transform.lossyScale;
				cldCenter = new Vector3(cldCenter.x / scalechild.x, cldCenter.y / scalechild.y, cldCenter.z / scalechild.z);
				cldCenter += target.position;
			}
			center += cldCenter;
		}
		center /= renders.Length;
		center -= target.position;
		return center;
	}

	public static Vector3 CalSizeOfModel(Transform transRoot)
	{
		Vector3 size = Vector3.zero;
		Renderer[] renders = transRoot.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in renders)
		{
			Vector3 childSize = child.bounds.size;
			if (child is SkinnedMeshRenderer)
			{
				(child as SkinnedMeshRenderer).sharedMesh.RecalculateBounds();
				childSize = (child as SkinnedMeshRenderer).sharedMesh.bounds.size;
				Vector3 scalechild = child.transform.lossyScale;
				childSize = new Vector3(childSize.x / scalechild.x, childSize.y / scalechild.y, childSize.z / scalechild.z);
			}
			childSize = child.transform.rotation * childSize;
			childSize = new Vector3(Mathf.Abs(childSize.x), Mathf.Abs(childSize.y), Mathf.Abs(childSize.z));

			if (size.x < childSize.x)
				size.x = childSize.x;

			if (size.y < childSize.y)
				size.y = childSize.y;

			if (size.z < childSize.z)
				size.z = childSize.z;
		}

		return size;
	}

	public static void SetTagAllChildren(GameObject go, string tag)
	{
		if (go != null)
		{
			Transform trans = go.transform;
			for (int i = 0; i < trans.childCount; ++i)
			{
				trans.GetChild(i).gameObject.tag = tag;
			}
		}
	}

	/// <summary>
	/// 设置Tag
	/// </summary>
	/// <param name="tag">Tag</param>
	public static void SetTag(GameObject gameObject, string tag)
	{
		if (gameObject != null)
		{
			gameObject.tag = tag;
			ChangeChildTag(gameObject.transform, tag);
		}
	}
	static void ChangeChildTag(Transform tran, string tag)
	{
		for (int i = 0, length = tran.childCount; i < length; i++)
		{
			Transform t = tran.GetChild(i);
			t.gameObject.tag = tag;
			ChangeChildTag(t, tag);
		}
	}

	public static void SetLayer(GameObject gameObject, int layerId, bool containChildren = true)
	{
		try
		{
			if (gameObject != null)
			{
				gameObject.layer = layerId;
				if (containChildren)
				{
					Transform trans = gameObject.transform;
					if (trans != null)
					{
						for (int i = 0; i < trans.childCount; ++i)
						{

							Transform transChild = trans.GetChild(i);
							if (transChild != null)
							{
								SetLayer(transChild.gameObject, layerId);
							}
						}
					}
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e);
		}
	}

	/// <summary>
	/// 设置RenderQueue
	/// </summary>
	public static void SetRenderQueue(GameObject obj, int renderQueue)
	{
		if (obj == null) return;
		Renderer[] rendererArray = obj.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < rendererArray.Length; i++)
		{
			Renderer renderer = rendererArray[i];
			if (renderer.material != null)
			{
				renderer.material.renderQueue = renderQueue;
			}
			if (renderer.sharedMaterial != null)
			{
				renderer.sharedMaterial.renderQueue = renderQueue;
			}
		}
	}


	/// <summary>
	/// 设置UI界面的RenderQueue
	/// </summary>
	public static void SetUIRenderQueue(GameObject obj, int renderQueue = 3000)
	{
		Material defaultUIMaterial = null;
		Graphic[] allRenderer = obj.GetComponentsInChildren<Graphic>(true);
		for (int i = 0; i < allRenderer.Length; i++)
		{
			Material material = allRenderer[i].material;
			if (material == null)
			{
				if (defaultUIMaterial == null)
				{
					defaultUIMaterial = AssetLoadMgr.Instance.LoadNativePrefab<Material>(StrDef.PATH_DEFAULT_UI_Material);
					defaultUIMaterial.renderQueue = renderQueue;
				}
				allRenderer[i].material = defaultUIMaterial;
			}
			else
			{
				material.renderQueue = renderQueue;
			}
		}
	}

	/// <summary>
	/// 设置RenderQueue
	/// </summary>
	public static void SetRenderQueue(GameObject obj, int renderQueue, int sortingOrder)
	{
		if (obj == null) return;
		Renderer[] rendererArray = obj.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < rendererArray.Length; i++)
		{
			Renderer renderer = rendererArray[i];
			if (renderer.material != null)
			{
				renderer.material.renderQueue = renderQueue;

			}
			if (renderer.sharedMaterial != null)
			{
				renderer.sharedMaterial.renderQueue = renderQueue;
			}

			renderer.sortingOrder = sortingOrder;
		}
	}

	/// <summary>
	/// 计算与相机一定距离时的相机的四个角的坐标
	/// </summary>
	/// <param name="theCamera"></param>
	/// <param name="distance"></param>
	/// <returns></returns>
	public static Vector3[] GetCorners(Camera theCamera, float distance)
	{
		Vector3[] corners = new Vector3[4];

		float halfFOV = (theCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
		float aspect = theCamera.aspect;

		float height = distance * Mathf.Tan(halfFOV);
		float width = height * aspect;

		Transform tx = theCamera.transform;

		// UpperLeft
		corners[0] = tx.position - (tx.right * width);
		corners[0] += tx.up * height;
		corners[0] += tx.forward * distance;

		// UpperRight
		corners[1] = tx.position + (tx.right * width);
		corners[1] += tx.up * height;
		corners[1] += tx.forward * distance;

		// LowerLeft
		corners[2] = tx.position - (tx.right * width);
		corners[2] -= tx.up * height;
		corners[2] += tx.forward * distance;

		// LowerRight
		corners[3] = tx.position + (tx.right * width);
		corners[3] -= tx.up * height;
		corners[3] += tx.forward * distance;

		return corners;
	}

	/// <summary>
	/// 点到直线的距离
	/// </summary>
	/// <param name="point">点</param>
	/// <param name="linePoint1">直线上一个坐标</param>
	/// <param name="linePoint2">直线上的另一个坐标</param>
	/// <returns>距离</returns>
	public static float DisPoint2Line(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
	{
		Vector3 vec1 = point - linePoint1;
		Vector3 vec2 = linePoint2 - linePoint1;
		Vector3 vecProj = Vector3.Project(vec1, vec2);
		return Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(vecProj), 2));
	}

	/// <summary>
	///求两直线的交点坐标
	///给定两个点P1和P2，直线上的点为P
	///参数方程：
	///   P＝ P1 ＋ t*(P2-P1）
	///展开就是
	///   p.x = p1.x + t*(p2.x-p1.x)
	///   p.y = p1.y + t*(p2.y-p1.y)
	///   p.z = p1.z + t*(p2.z-p1.z)
	///这种写法就比用等式的好，因为不存在分母为0的问题
	/// </summary>
	/// <param name="pn1">L1上的点</param>
	/// <param name="pn2">L1上的点</param>
	/// <param name="pn3">L2上的点</param>
	/// <param name="pn4">L2上的点</param>
	/// <returns></returns>
	public static Vector3 LianZX_JD(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
	{
		Vector3 Jiaod = Vector3.zero;
		float P1x = 0.0f;
		float P1y = 0.0f;
		float P1z = 0.0f;
		double plr1_x = p2.x - p1.x;
		double plr1_y = p2.y - p1.y;
		double plr1_z = p2.z - p1.z;
		double plr2_x = p4.x - p3.x;
		double plr2_y = p4.y - p3.y;
		double plr2_z = p4.z - p3.z;
		double t = 1.0f;
		if (((plr1_x != 0) && (plr2_x == 0)) || ((plr1_x == 0) && (plr2_x != 0)))
		{
			if (plr2_x == 0)
			{
				t = (p3.x - p1.x) / plr1_x;
				P1x = (float)(p1.x + t * plr1_x);
				P1y = (float)(p1.y + t * plr1_y);
				P1z = (float)(p1.z + t * plr1_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
			else
			{
				t = (p1.x - p3.x) / plr2_x;
				P1x = (float)(p3.x + t * plr2_x);
				P1y = (float)(p3.y + t * plr2_y);
				P1z = (float)(p3.z + t * plr2_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
		}
		else if (((plr1_y != 0) && (plr2_y == 0)) || ((plr1_y == 0) && (plr2_y != 0)))
		{
			if (plr2_y == 0)
			{
				t = (p3.y - p1.y) / plr1_y;
				P1x = (float)(p1.x + t * plr1_x);
				P1y = (float)(p1.y + t * plr1_y);
				P1z = (float)(p1.z + t * plr1_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
			else
			{
				t = (p1.y - p3.y) / plr2_y;
				P1x = (float)(p3.x + t * plr2_x);
				P1y = (float)(p3.y + t * plr2_y);
				P1z = (float)(p3.z + t * plr2_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
		}
		else if (((plr1_z != 0) && (plr2_z == 0)) || ((plr1_z == 0) && (plr2_z != 0)))
		{
			if (plr2_z == 0)
			{
				t = (p3.z - p1.z) / plr1_z;
				P1x = (float)(p1.x + t * plr1_x);
				P1y = (float)(p1.y + t * plr1_y);
				P1z = (float)(p1.z + t * plr1_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
			else
			{
				t = (p1.z - p3.z) / plr2_z;
				P1x = (float)(p3.x + t * plr2_x);
				P1y = (float)(p3.y + t * plr2_y);
				P1z = (float)(p3.z + t * plr2_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
		}
		else
		{
			if (((plr1_x != 0) && (plr2_x != 0)) && ((plr1_y != 0) && (plr2_y != 0)))
			{
				double fz = (p3.x * plr2_y - p3.y * plr2_x - plr2_y * p1.x + plr2_x * p1.y);
				double fm = (plr1_x * plr2_y - plr1_y * plr2_x);
				t = fz / fm;
				P1x = (float)(p1.x + t * plr1_x);
				P1y = (float)(p1.y + t * plr1_y);
				P1z = (float)(p1.z + t * plr1_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
			else if (((plr1_x != 0) && (plr2_x != 0)) && ((plr1_z != 0) && (plr2_z != 0)))
			{
				double fz = (p3.x * plr2_z - p3.z * plr2_x - plr2_z * p1.x + plr2_x * p1.z);
				double fm = (plr1_x * plr2_z - plr1_z * plr2_x);
				t = fz / fm;
				P1x = (float)(p1.x + t * plr1_x);
				P1y = (float)(p1.y + t * plr1_y);
				P1z = (float)(p1.z + t * plr1_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
			else if (((plr1_y != 0) && (plr2_y != 0)) && ((plr1_z != 0) && (plr2_z != 0)))
			{
				double fz = (p3.y * plr2_z - p3.z * plr2_y - plr2_z * p1.y + plr2_y * p1.z);
				double fm = (plr1_y * plr2_z - plr1_z * plr2_y);
				t = fz / fm;
				P1x = (float)(p1.x + t * plr1_x);
				P1y = (float)(p1.y + t * plr1_y);
				P1z = (float)(p1.z + t * plr1_z);
				Jiaod = new Vector3(P1x, P1y, P1z);
				return Jiaod;
			}
			else
			{
				return Vector3.zero;
			}

		}
	}

	/// <summary>
	/// 判断一个点是否在多边形内部（凸多边形和凹多边形都能用）
	/// </summary>
	/// <param name="coordArray">多边形顶点的坐标集合</param>
	/// <param name="coord">判断点的坐标</param>
	/// <returns></returns>
	public static bool PointInPolygon(Vector2[] coordArray, Vector2 coord)
	{
		int count = coordArray.Length;
		int i, j = count - 1;
		bool isInside = false;
		for (i = 0; i < count; i++)
		{
			if ((coordArray[i].y < coord.y && coordArray[j].y >= coord.y
				|| coordArray[j].y < coord.y && coordArray[i].y >= coord.y)
				&& (coordArray[i].x <= coord.x || coordArray[j].x <= coord.x))
			{
				isInside ^= (coordArray[i].x + (coord.y - coordArray[i].y) / (coordArray[j].y - coordArray[i].y) * (coordArray[j].x - coordArray[i].x) < coord.x);
			}
			j = i;
		}
		return isInside;
	}

	/// <summary>
	/// 求一条直线与平面的交点
	/// </summary>
	/// <param name="planeVector">平面的法线向量</param>
	/// <param name="planePoint">平面经过的一点坐标</param>
	/// <param name="lineVector">直线的方向向量</param>
	/// <param name="linePoint">直线经过的一点坐标</param>
	/// <param name="crossPoint">直线与平面的交点</param>
	/// <returns>直线是否与平面相交</returns>
	public static bool CalPlaneLineIntersectPoint(Vector3 planeVector, Vector3 planePoint, Vector3 lineVector, Vector3 linePoint, out Vector3 crossPoint)
	{
		if (Mathf.Approximately(Vector3.Dot(planeVector, lineVector), 0.0f))
		{
			crossPoint = Vector3.zero;
			return false;
		}
		else
		{
			float d = Vector3.Dot(planePoint - linePoint, planeVector) / Vector3.Dot(lineVector, planeVector);
			crossPoint = d * lineVector.normalized + linePoint;
			return true;
		}
	}

	#region UIMath
	/// <summary>
	/// 颜色值转字符串
	/// </summary>
	/// <param name="c"></param>
	/// <returns></returns>
	static public string EncodeColor(Color c)
	{
		int i = 0xFFFFFF & (ColorToInt(c) >> 8);
		return string.Format("#{0}", DecimalToHex(i));
	}

	/// <summary>
	/// Convert the specified color to RGBA32 integer format.
	/// </summary>
	static public int ColorToInt(Color c)
	{
		int retVal = 0;
		retVal |= Mathf.RoundToInt(c.r * 255f) << 24;
		retVal |= Mathf.RoundToInt(c.g * 255f) << 16;
		retVal |= Mathf.RoundToInt(c.b * 255f) << 8;
		retVal |= Mathf.RoundToInt(c.a * 255f);
		return retVal;
	}

	static public string DecimalToHex(int num)
	{
		num &= 0xFFFFFF;
#if UNITY_FLASH
      StringBuilder sb = new StringBuilder();
      sb.Append(DecimalToHexChar((num >> 20) & 0xF));
      sb.Append(DecimalToHexChar((num >> 16) & 0xF));
      sb.Append(DecimalToHexChar((num >> 12) & 0xF));
      sb.Append(DecimalToHexChar((num >> 8) & 0xF));
      sb.Append(DecimalToHexChar((num >> 4) & 0xF));
      sb.Append(DecimalToHexChar(num & 0xF));
      return sb.ToString();
#else
		return num.ToString("X6");
#endif
	}
	/// <summary>
	/// 字符串颜色值转颜色
	/// </summary>
	/// <param name="hex">6位数的字符串颜色值</param>
	/// <returns></returns>
	public static Color HexToColor(string hex)
	{
		hex = hex.TrimStart('#');
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}
	#endregion
}
