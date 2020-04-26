using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : PausableTimer
{
	[SerializeField] private List<Damageable> castles = new List<Damageable>();
	[SerializeField] private WaveController waveController;
	[SerializeField] private bool alwaysGainCurrency;
	[SerializeField] private int currencyPerInterval;
	[SerializeField] private float currencyGainInterval;


	public int CurrentHealth { get; private set; }
	public int MaxHealth { get; private set; }
	public int CurrentWave { get; private set; }
	public int WaveCount { get; private set; }
	public int Currency { get; private set; }

	public event Action HealthUpdated;
	public event Action WaveUpdated;
	public event Action CurrencyUpdated;

	public event Action LevelStarted;
	public event EventHandler<EventArgTemplate<bool>> LevelEnded;

	private new void Start()
	{
		base.Start();
		AssignCastleListeners();
		UpdateHealth();
		WaveCount = waveController.WaveCount;

		waveController.WaveChanged += OnWaveUpdated;
		waveController.WavesCompleted += OnWaveCompletion;
		
		OnWaveUpdated();

		if (alwaysGainCurrency)
		{
			StartCoroutine(GainConstantCurrency());
		}
	}

	private void AssignCastleListeners()
	{
		foreach (var castle in castles)
		{
			castle.DamageTaken += UpdateHealth;
		}
	}

	private void UpdateHealth()
	{
		var currentHealth = 0;
		var maxHealth = 0;
		foreach (var castle in castles)
		{
			currentHealth += castle.CurrentHealth;
			maxHealth += castle.MaxHealth;
		}

		CurrentHealth = currentHealth;
		MaxHealth = maxHealth;
		SafeEventHandler.SafelyBroadcastAction(ref HealthUpdated);

		if (CurrentHealth <= 0)
		{
			SafeEventHandler.SafelyBroadcastEvent(ref LevelEnded, false, this);
		}
	}

	private void OnWaveUpdated()
	{
		CurrentWave = waveController.CurrentWaveIndex + 1;
		SafeEventHandler.SafelyBroadcastAction(ref WaveUpdated);
	}

	private void OnWaveCompletion()
	{
		SafeEventHandler.SafelyBroadcastEvent(ref LevelEnded, true, this);
	}

	private IEnumerator GainConstantCurrency()
	{
		while (true)
		{
			yield return WaitForTime(currencyGainInterval);
			Currency += currencyPerInterval;
			SafeEventHandler.SafelyBroadcastAction(ref CurrencyUpdated);
		}
	}

	public void StartLevel()
	{
		SafeEventHandler.SafelyBroadcastAction(ref LevelStarted);
		waveController.StartWaves();
	}
}
