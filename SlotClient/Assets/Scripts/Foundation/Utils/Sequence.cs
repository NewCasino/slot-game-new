/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* 文 件 名：Sequence.cs
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

namespace Slot.Utils
{ 
	/// <summary>
	/// 文件名:工具类
	/// 说明:
	/// </summary>
	public class Sequence
	{
		public Sequence()
		{
		}

		public Sequence(int iStart, int iStep)
		{
			m_iStart = iStart;
			m_iCurr = iStart;
			m_iStep = iStep;
		}

		//private variable
		private int m_iStart = 0;
		private int m_iCurr = 0;
		private int m_iStep = 1;

		public int nextval
		{
			get
			{
				m_iCurr += m_iStep;
				return m_iCurr;
			}
		}

		public int currval
		{
			get { return m_iCurr; }
			set
			{
				if (value >= m_iStart)
				{
					m_iCurr = value;
				}
			}
		}

		public void SetStep(int iStep)
		{
			m_iStep = iStep;
		}

		public void SetStartVal(int iStart)
		{
			if (m_iStart == m_iCurr)
			{
				m_iCurr = iStart;
			}
			m_iStart = iStart;
		}

		public void Reset()
		{
			m_iCurr = m_iStart;
		}
	}
}