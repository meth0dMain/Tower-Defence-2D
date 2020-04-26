using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectionScreen : MonoBehaviour
{
	public void SelectLevel(LevelController levelController)
	{
		GameManager.Instance.OnDifficultySelection(levelController);
	}
}
