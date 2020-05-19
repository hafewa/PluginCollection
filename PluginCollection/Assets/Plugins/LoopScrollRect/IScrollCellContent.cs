using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
	public interface IScrollCellContent<T>
	{
		void ScrollCellContent(int index,T data);    
    }
}
