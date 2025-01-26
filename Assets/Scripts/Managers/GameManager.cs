using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public Action OnMatchStart = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // hookup to onbutton clicked
    public void StartMatch()
    {
        //start match called fire start event
        OnMatchStart?.Invoke();
    }
}
