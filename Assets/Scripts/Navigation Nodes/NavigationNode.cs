using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavigationNode : MonoBehaviour
{
    [SerializeField] protected List<NavigationNode> linkedNodes;

    public abstract NavigationNode GetNextNode();
}
