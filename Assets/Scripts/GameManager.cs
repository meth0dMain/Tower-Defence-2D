using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject levelSelectionMenu;
	[SerializeField] private GameObject backgroundGrid;
	[SerializeField] private GameObject hud;

	public static GameManager Instance;
	
	public enum State
	{
		SelectingDifficulty,
		GameStarted,
		GameWon,
		GameLost
	}
	
	public bool IsPaused { get; private set; }
	public State CurrentState { get; private set; }
	public LevelController CurrentLevelController { get; private set; }

	public event EventHandler<EventArgTemplate<LevelController>> NewLevelSelected;
	public event EventHandler<EventArgTemplate<bool>> GameFinished;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != null && Instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		CurrentState = State.SelectingDifficulty;
		hud.SetActive(false);
		levelSelectionMenu.SetActive(true);
		backgroundGrid.SetActive(true);
	}

	private void Update()
	{
		CheckForQuit();
	}

	public void OnDifficultySelection(LevelController levelController)
	{
		var newLevel = Instantiate(levelController);
		// Set up this level
		AssignLevelController(newLevel);
		
		SafeEventHandler.SafelyBroadcastEvent(ref NewLevelSelected, newLevel, this);
		newLevel.StartLevel();
		levelSelectionMenu.SetActive(false);
		backgroundGrid.SetActive(false);
		hud.SetActive(true);
	}
	
	private void AssignLevelController(LevelController levelController)
	{
		if (CurrentLevelController != null)
		{
			CurrentLevelController.LevelStarted -= OnLevelStarted;
			CurrentLevelController.LevelEnded -= OnLevelCompleted;
			Destroy(CurrentLevelController.gameObject);
		}

		CurrentLevelController = levelController;
		CurrentLevelController.LevelStarted += OnLevelStarted;
		CurrentLevelController.LevelEnded += OnLevelCompleted;
	}

	private void OnLevelStarted()
	{
		CurrentState = State.GameStarted;
	}	

	private void OnLevelCompleted(object sender, EventArgTemplate<bool> success)
	{
		CurrentState = success.Attachment ? State.GameWon : State.GameLost;
		SafeEventHandler.SafelyBroadcastEvent(ref GameFinished, success.Attachment, this);
	}

	private void CheckForQuit()
	{
		var isPressingShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		var isPressingQ = Input.GetKey(KeyCode.Q);
		if (isPressingShift && isPressingQ)
		{
			Application.Quit();
		}
	}
}
