using UnityEngine;

public class AttackingState : CharacterBaseState
{
    private float attackCooldown = 0f;

    public AttackingState(Character character) : base(character) { }

    public override void OnEnter()
    {
        //Debug.Log($"[{character.DisplayName}] ENTERING AttackingState | Weapon: {character.CurrentWeaponData.name} | AttackSpeed: {character.CurrentWeaponData.AttackSpeed}");
        attackCooldown = character.CurrentWeaponData.AttackSpeed;
        character.Agent.isStopped = true;
    }

    public override void OnUpdate()
    {

        // Check if target is gone or dead
        if (character.CurrentTarget == null || character.CurrentTarget.CurrentHealth <= 0f)
        {
            Debug.LogWarning($"{character.DisplayName}'s Target is NULL or DEAD. Returning to SearchingState.");
            character.SetState(character.ActiveCharacterAIStates.Searching);
            return;
        }

        // Check if target is still in range
        float distanceToTarget = (character.CurrentTarget.transform.position - character.transform.position).sqrMagnitude;
        float attackRangeSqr = character.CurrentWeaponData.Range * character.CurrentWeaponData.Range;

        if (distanceToTarget > attackRangeSqr)
        {
            Debug.Log($"{character.DisplayName}'s Target is out of range. Switching to ChasingState.");
            character.SetState(character.ActiveCharacterAIStates.Chasing);
            return;
        }

        // Attack if cooldown allows
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            PerformAttack();
            attackCooldown = character.CurrentWeaponData.AttackSpeed;
        }
    }

    public override void OnExit()
    {
        //Debug.Log($"[{character.DisplayName}] EXITING AttackingState");
    }

    private void PerformAttack()
    {
        if (character.CurrentTarget == null)
        {
            Debug.LogError($"{character.DisplayName} ERROR: Attempted to attack, but target is NULL!");
            return;
        }

        float damage = character.CurrentWeaponData?.BulletType?.BulletDamage ?? 10f;
        Debug.Log($"{character.DisplayName} is ATTACKING {character.CurrentTarget.DisplayName} | Damage: {damage}");

        character.CurrentTarget.TakeDamage(damage);
    }
}
