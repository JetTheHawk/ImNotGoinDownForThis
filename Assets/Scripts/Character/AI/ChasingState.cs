using UnityEngine;
using UnityEngine.AI;

public class ChasingState : CharacterBaseState
{
    public ChasingState(Character character) : base(character) { }

    public override void OnEnter()
    {
        //Debug.Log($"{character.name} entering ChasingState");
        character.Agent.isStopped = false;
    }

    public override void OnUpdate()
    {
        // Check if currentTarget is still valid
        if (character.CurrentTarget == null || character.CurrentTarget.CurrentHealth <= 0f)
        {
            // Target lost or dead, go back to Searching
            character.SetState(character.ActiveCharacterAIStates.Searching);
            return;
        }

        // Move towards target
        character.Agent.SetDestination(character.CurrentTarget.transform.position);

        if (TargetingUtils.IsWithinRange(character.transform.position, character.CurrentTarget.transform.position, character.CurrentWeaponData.Range))
        {
            character.SetState(character.ActiveCharacterAIStates.Attacking);
        }

    }

    public override void OnExit()
    {
        //Debug.Log($"{character.name} exiting ChasingState");
    }
}
