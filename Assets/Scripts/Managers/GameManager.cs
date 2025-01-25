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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // hookup to onbutton clicked
    public void StartMatch()
    {
        //start match called fire start event
        OnMatchStart?.Invoke();
    }
}
