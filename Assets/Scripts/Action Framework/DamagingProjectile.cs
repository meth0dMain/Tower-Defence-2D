using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DamagingProjectile : Damaging
{
	[SerializeField] private Projectile projectile;
	
	private void Awake()
	{
		Assert.IsNotNull(projectile);
	}

	protected override IEnumerator Attack(bool isRepeating)
	{
		while (Timer < attackFrequency)
			yield return new WaitForSeconds(0.1f);
		ResetTimer();
		if (Target != null)
			FireProjectile();
		
		if (isRepeating)
			StartCoroutine(Attack(true));

		yield return null;
	}

	private void FireProjectile()
	{
		var newProjectile = Instantiate(projectile);
		newProjectile.transform.parent = transform;
		newProjectile.transform.localPosition = new Vector2();
		newProjectile.Fire(Target);
	}
}
