using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingMelee : Damaging
{
	protected override IEnumerator Attack(bool isRepeating)
	{
		while (Timer < attackFrequency)
			yield return new WaitForSeconds(0.1f);
		
		ResetTimer();

		if (animator != null)
		{
			animator.SetTrigger(attackTriggerName);
		}

		if (isRepeating)
			StartCoroutine(Attack(true));

		yield return null;
	}
}
