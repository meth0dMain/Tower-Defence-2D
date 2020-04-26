using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AutoTargeting : MonoBehaviour
{
	[SerializeField] private List<string> targetTags = new List<string>();
	[SerializeField] private List<TargetType> targetTypes = new List<TargetType>();

	public GameObject Target { get; private set; }
	private List<GameObject> targetsInRange = new List<GameObject>();
	
	public event Action TargetLost;
	public event EventHandler<EventArgTemplate<GameObject>> TargetAcquired;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (IsValidTarget(other))
		{
			targetsInRange.Add(other.gameObject);
			AddTarget(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		targetsInRange.Remove(other.gameObject);
		if (other.gameObject.Equals(Target))
			RemoveAndUpdateTarget();
	}

	private void AddTarget(GameObject targetCandidate)
	{
		if (Target == null) 
		{
			Target = targetCandidate;
			Target.GetComponent<Targetable>().TargetableStatusRemoved += RemoveAndUpdateTarget;
			SafeEventHandler.SafelyBroadcastEvent(ref TargetAcquired, Target, this);
		}
	}

	private void RemoveAndUpdateTarget()
	{
		targetsInRange.Remove(Target);
		Target.GetComponent<Targetable>().TargetableStatusRemoved -= RemoveAndUpdateTarget;
		Target = null;
		SafeEventHandler.SafelyBroadcastAction(ref TargetLost);
		if (targetsInRange.Count > 1)
		{
			AddTarget(targetsInRange[0]);
			SafeEventHandler.SafelyBroadcastEvent(ref TargetAcquired, Target, this);
		}
	}

	private bool IsValidTarget(Collider2D other)
	{
		var isValidTag = targetTags.Contains(other.tag);
		var isValidType = false;

		var targetable = other.gameObject.GetComponent<Targetable>();
		if (targetable != null)
		{
			var otherTypes = targetable.Types;
			foreach (var type in otherTypes)
				if (targetable.Types.Contains(type))
				{
					isValidType = true;
					break;
				}

		}

		return isValidTag && isValidType;
	}
}
