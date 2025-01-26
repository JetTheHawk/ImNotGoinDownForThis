using UnityEngine;

public class SearchingState : CharacterBaseState
{
    public SearchingState(Character character) : base(character) { }

    public override void OnEnter()
    {
        //Debug.Log($"{character.name} entering SearchingState");
    }

    public override void OnUpdate()
    {
        float sightRange = CharacterManager.Instance.GlobalData.CharacterSightRange;

        Character potentialTarget = TargetingUtils.FindClosestTarget(character, sightRange);

        if (potentialTarget != null)
        {
            character.CurrentTarget = potentialTarget;
            character.SetState(character.ActiveCharacterAIStates.Chasing);
        }
        else
        {
            Debug.LogWarning($"{character.DisplayName} No targets found. Remaining in SearchingState.");
        }
    }


    public override void OnExit()
    {
        //Debug.Log($"{character.name} exiting SearchingState");
    }
}
