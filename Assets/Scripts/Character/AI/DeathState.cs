using UnityEditor;
using UnityEngine;

public class DeathState : CharacterBaseState
{
    public DeathState(Character character) : base(character) { }

    public override void OnEnter()
    {
        character.Agent.isStopped = true;

        if (CharacterManager.Instance != null)
        {
            CharacterManager.Instance.UnregisterCharacter(character);
            character.gameObject.SetActive(false);
        }

        // disable character + play a death animation
    }
}
