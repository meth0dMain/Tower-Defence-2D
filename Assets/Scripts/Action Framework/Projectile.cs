using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed = 0.05f;
	[SerializeField] private int damage = 1;

	private bool hasImpacted;

	public int Damage => damage;
	
	public GameObject Target { get; private set; }

	public void Fire(GameObject target)
	{
		Target = target;
		StartCoroutine(FlyToward());
	}

	private IEnumerator FlyToward()
	{
		while (Target != null)
		{
			CorrectRotation();
			transform.position = Vector2.MoveTowards(transform.position, Target.transform.position,
				speed * Time.deltaTime);
			yield return null;
		}

		if (!hasImpacted)
			Destroy(gameObject);
	}

	private IEnumerator MakeImpact()
	{
		hasImpacted = true;
		Target = null;
		Destroy(gameObject);
		yield return null;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.Equals(Target))
		{
			StopCoroutine(FlyToward());
			StartCoroutine(MakeImpact());
		}
	}

	private void CorrectRotation()
	{
		Vector2 direction = Target.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
