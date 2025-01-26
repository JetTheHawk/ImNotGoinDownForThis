using UnityEngine;

public class VictoryState : CharacterBaseState
{
    public VictoryState(Character character) : base(character) { }

    public override void OnEnter()
    {
        character.Agent.isStopped = true;

        //play vicory anim

        // tell game/ui manager UI for winner

    }
}
