using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damaging : PausableTimer
{
	[SerializeField] protected int damage = 1;
	[SerializeField] protected float attackFrequency = 1.0f;
	[SerializeField] protected string attackTriggerName = "Attack";
	[SerializeField] protected List<string> targetTags = new List<string>();

	public GameObject Target { get; protected set; }
	
	protected Animator animator;

	public int Damage => damage;

	private new void Start()
	{
		base.Start();
		animator = GetComponent<Animator>();
	}

	public void StartAttacking(bool isRepeating)
	{
		StartCoroutine(Attack(isRepeating));
	}

	public void StopAttacking()
	{
		StopAllCoroutines();
	}
	
	public void StartTargeting(GameObject target)
	{
		this.Target = target;
	}

	public void StopTargeting()
	{
		this.Target = null;
	}

	protected abstract IEnumerator Attack(bool isRepeating);
}
