using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private List<Wave> waves = new List<Wave>();
    private int currentWaveIndex;
    public int CurrentWaveIndex => currentWaveIndex;
    public int WaveCount => waves.Count;
    public event Action WaveChanged;
    public event Action WavesCompleted;

    public void StartWaves()
    {
        if (waves.Count > 0)
        {
            InitCurrentWave();
        }
        else
        {
            print("no waves on Wave controller");
            SafeEventHandler.SafelyBroadcastAction(ref WavesCompleted);
        }
    }
    
    private void InitCurrentWave()
    {
        if (currentWaveIndex >= waves.Count)
            SafeEventHandler.SafelyBroadcastAction(ref WavesCompleted); 
        var wave = waves[currentWaveIndex];
        wave.WaveComleted += NextWave;
        wave.BeginWave();
        SafeEventHandler.SafelyBroadcastAction(ref WaveChanged);
    }

    private void NextWave(object sender, EventArgTemplate<bool> success)
    {
        if (success.Attachment)
        {
            waves[currentWaveIndex].WaveComleted -= NextWave;
            currentWaveIndex++;
            if (currentWaveIndex >= waves.Count)
                SafeEventHandler.SafelyBroadcastAction(ref WavesCompleted);
            else
            {
                InitCurrentWave();
            }
        }
    }

}
