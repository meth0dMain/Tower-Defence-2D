using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNavigationNode : NavigationNode
{
	public override NavigationNode GetNextNode()
	{
		if (linkedNodes.Count > 0)
		{
			var index = Random.Range(0, linkedNodes.Count);
			return linkedNodes[index];
		}
		return null;
	}
}
