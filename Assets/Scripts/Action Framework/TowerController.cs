using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Damaging))]
public class TowerController : MonoBehaviour
{
	[SerializeField] private AutoTargeting autoTargetingChild;

	private Damaging damagingBehaviour;

	private void Awake()
	{
		Assert.IsNotNull(autoTargetingChild);
	}

	private void Start()
	{
		damagingBehaviour = GetComponent<Damaging>();
		autoTargetingChild.TargetAcquired += OnTargetAcquired;
		autoTargetingChild.TargetLost += OnTargetLost;
	}

	private void OnTargetAcquired(object sender, EventArgTemplate<GameObject> target)
	{
		damagingBehaviour.StartTargeting(target.Attachment);
		damagingBehaviour.StartAttacking(true);
	}

	private void OnTargetLost()
	{
		damagingBehaviour.StopTargeting();
		damagingBehaviour.StopAttacking();
	}
}
