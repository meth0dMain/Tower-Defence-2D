using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedNavigationNode : NavigationNode
{
	public override NavigationNode GetNextNode()
	{
		return linkedNodes.Count > 0 ? linkedNodes[0] : null;
	}
}
