using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullCompletionWave : Wave
{
	[SerializeField] private List<string> observedTags =
		new List<string>() {"Enemy"};

	private List<Damageable> observedDamageables = new List<Damageable>();
	
	protected override IEnumerator HandleCompletionLogic()
	{
		var damagetables = FindObjectOfType<Damageable>();
		foreach (Damageable damagetable in damagetables)
		{
			if (IsValidTarget(damagetable))
			{
				observedDamageables.Add(damagetable);
				damagetable.DamageTaken += DamagetableOnDamageTaken;
			}
		}
		yield return null;
	}

	private void DamagetableOnDamageTaken()
	{
		var damagetablesToRemove = new List<Damageable>();
		foreach (var damagetable in observedDamageables)
		{
			if (damagetable.CurrentHealth <= 0)
			{
				damagetable.DamageTaken -= DamagetableOnDamageTaken;
				damagetablesToRemove.Add(damagetable);
			}
		}

		foreach (var damagetable in damagetablesToRemove)
			observedDamageables.Remove(damagetable);


		if (observedDamageables.Count == 0)
			HandleCompletion(true);

	}

	private bool IsValidTarget(Damageable damageable)
	{
		var validTag = observedTags.Contains(damageable.gameObject.tag);
		var isAlive = damageable.CurrentHealth > 0;
		return validTag && isAlive;

	}
}
