using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : PausableTimer
{
    [SerializeField] protected List<SpawnInstruction> SpawnInstructions = new List<SpawnInstruction>();
    protected int currentInstructionIndex;

    public event EventHandler<EventArgTemplate<bool>> WaveComleted;

    public virtual void BeginWave()
    {
        CompleteNextInstruction();
    }

    protected virtual void HandleCompletion(bool success)
    {
        if (WaveComleted != null)
        {
            var completion = new EventArgTemplate<bool>(success);
            WaveComleted.Invoke(this,completion);
        }
    }

    protected virtual void CompleteNextInstruction()
    {
        if (currentInstructionIndex >= SpawnInstructions.Count)
        {
            StartCoroutine(HandleCompletionLogic());
            return;
        }

        var currentInstruction = SpawnInstructions[currentInstructionIndex];
        currentInstructionIndex++;
        // Run the current instruction
        StartCoroutine(HandleCurrentInstruction(currentInstruction));
    }
    
    protected IEnumerator HandleCurrentInstruction(SpawnInstruction instruction)
    {
        yield return WaitForTime(instruction.SpawnDelay);
        SpawnInstructionEnemy(instruction);
        CompleteNextInstruction();
    }
    
    protected void SpawnInstructionEnemy(SpawnInstruction instruction)
    {
        var newAgent = Instantiate(instruction.SpawnAgent);
        newAgent.StartingNode = instruction.StartingNode;
        newAgent.transform.parent = instruction.StartingNode.transform;
        newAgent.transform.localPosition = new Vector2(0,0);
    }

    protected abstract IEnumerator HandleCompletionLogic();
}
