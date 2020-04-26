using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[
	RequireComponent(typeof(Animation)),
	RequireComponent(typeof(Damageable)),
	RequireComponent(typeof(AutoMovement)),
	RequireComponent(typeof(Targetable)),
	RequireComponent(typeof(Damaging))
]
public class EnemyController : MonoBehaviour
{
	[SerializeField] private string healthAnimatorParam = "Health";
	[SerializeField] private AutoTargeting autoTargetingChild;
	
	private Animator animator;
	private Damageable damageableBehaviour;
	private AutoMovement autoMovementBehaviour;
	private Targetable targetableBehaviour;
	private Damaging damagingBehaviour;

	private void Awake()
	{
		Assert.IsNotNull(autoTargetingChild);
	}

	private void Start()
	{
		animator = GetComponent<Animator>();
		damageableBehaviour = GetComponent<Damageable>();
		autoMovementBehaviour = GetComponent<AutoMovement>();
		targetableBehaviour = GetComponent<Targetable>();
		damagingBehaviour = GetComponent<Damaging>();

		damageableBehaviour.DamageTaken += handleDamageTaken;
		autoTargetingChild.TargetAcquired += OnTargetAcquired;
		autoTargetingChild.TargetLost += OnTargetLost;
	}

	private void handleDamageTaken()
	{
		var health = damageableBehaviour.CurrentHealth;
		animator.SetInteger(healthAnimatorParam, health);
		if (health <= 0)
		{
			autoMovementBehaviour.enabled = false;
			targetableBehaviour.Disable();
			GetComponent<CapsuleCollider2D>().enabled = false;
			//capsule.enabled = false;
		}
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
