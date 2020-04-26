using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnInstruction
{
	[SerializeField] private NavigationNode startingNode;
	[SerializeField] private float spawnDelay;
	[SerializeField] private AutoMovement spawnAgent;

	public NavigationNode StartingNode => startingNode;
	public float SpawnDelay => spawnDelay;
	public AutoMovement SpawnAgent => spawnAgent;
}
