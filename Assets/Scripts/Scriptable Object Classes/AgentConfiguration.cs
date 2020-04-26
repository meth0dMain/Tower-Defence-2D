using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentConfiguration.asset", menuName = "TowerDefence/AgentConfiguration", order = 1)]
public class AgentConfiguration : ScriptableObject
{
	public string agentName;
	public string description;
	public Agent prefab;
} 
