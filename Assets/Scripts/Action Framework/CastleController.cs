using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
	RequireComponent(typeof(Damageable)),
	RequireComponent(typeof(Targetable))
]
public class CastleController : MonoBehaviour
{
	private Damageable damageBehaviour;
	private Targetable targetableBehaviour;

	public event Action DamageTaken;

	private void Start()
	{
		damageBehaviour = GetComponent<Damageable>();
		targetableBehaviour = GetComponent<Targetable>();

		damageBehaviour.DamageTaken += OnDamageTaken;
	}

	private void OnDamageTaken()
	{
		if (damageBehaviour.CurrentHealth <= 0)
		{
			targetableBehaviour.Disable();
		}
		SafeEventHandler.SafelyBroadcastAction(ref DamageTaken);
	}
}
