using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class HUDController : MonoBehaviour
{
	[SerializeField] private TMP_Text waveText;
	[SerializeField] private TMP_Text currencyText;
	[SerializeField] private TMP_Text healthText;

	private LevelController currentLevel;

	private void Awake()
	{
		Assert.IsNotNull(waveText);
		Assert.IsNotNull(currencyText);
		Assert.IsNotNull(healthText);
	}

	private void Start()
	{
		if (GameManager.Instance.CurrentLevelController != null)
		{
			currentLevel = GameManager.Instance.CurrentLevelController;
			AssignAllListeners();
			UpdateAll();
		}

		GameManager.Instance.NewLevelSelected += OnNewLevelSelected;
	}

	private void OnNewLevelSelected(object sender, EventArgTemplate<LevelController> levelController)
	{
		currentLevel = levelController.Attachment;
		AssignAllListeners();
		UpdateAll();
	}

	private string JoinFractionText(int numerator, int denominator)
	{
		return string.Format("{0}/{1}", numerator, denominator);
	}

	private void OnHealthUpdated()
	{
		healthText.text = JoinFractionText(currentLevel.CurrentHealth, currentLevel.MaxHealth);
	}

	private void OnWaveUpdated()
	{
		waveText.text = JoinFractionText(currentLevel.CurrentWave, currentLevel.WaveCount);
	}
	
	private void OnCurrencyUpdated()
	{
		currencyText.text = currentLevel.Currency.ToString();
	}

	private void AssignAllListeners()
	{
		if (currentLevel != null)
		{
			currentLevel.HealthUpdated += OnHealthUpdated;
			currentLevel.CurrencyUpdated += OnCurrencyUpdated;
			currentLevel.WaveUpdated += OnWaveUpdated;
		}
	}

	private void UpdateAll()
	{
		OnHealthUpdated();
		OnCurrencyUpdated();
		OnWaveUpdated();
	}
}
