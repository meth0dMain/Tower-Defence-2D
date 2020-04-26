using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] private List<TargetType> types = new List<TargetType>();

    public event Action TargetableStatusRemoved;

    public List<TargetType> Types => types;

    public void Disable()
    {
        SafeEventHandler.SafelyBroadcastAction(ref TargetableStatusRemoved);
        enabled = false;
    }
}
