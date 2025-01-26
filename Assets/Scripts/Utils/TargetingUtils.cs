using UnityEngine;

public static class TargetingUtils
{
    public static bool IsWithinRange(Vector3 positionA, Vector3 positionB, float range)
    {
        float distSquared = (positionA - positionB).sqrMagnitude;
        float rangeSquared = range * range;
        return distSquared <= rangeSquared;
    }

    public static Character FindClosestTarget(Character searcher, float range)
    {
        float rangeSquared = range * range;
        Character closestCharacter = null;
        float minDistanceSquared = rangeSquared;

        if (CharacterManager.Instance == null)
        {
            Debug.LogError("FindClosestTarget: CharacterManager.Instance is NULL!");
            return null;
        }

        if (CharacterManager.Instance.ActiveCharacters.Count == 0)
        {
            Debug.LogWarning("FindClosestTarget: No active characters in the list!");
            return null;
        }

        foreach (var activeCharacter in CharacterManager.Instance.ActiveCharacters)
        {
            if (activeCharacter == null)
            {
                Debug.LogWarning("FindClosestTarget: Skipping null character.");
                continue;
            }
            if (activeCharacter == searcher) continue;
            if (activeCharacter.CurrentHealth <= 0) continue;


            float distSquared = (activeCharacter.transform.position - searcher.transform.position).sqrMagnitude;

            if (distSquared < minDistanceSquared)
            {
                minDistanceSquared = distSquared;
                closestCharacter = activeCharacter;
            }
        }

        if (closestCharacter == null)
        {
            Debug.LogWarning($"FindClosestTarget: {searcher.DisplayName} found NO targets in range!");
        }

        return closestCharacter;
    }
}
