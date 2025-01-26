using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject StartPanel;
    public GameObject VictoryPanel;

    public Button StartSimButton;
    public Button RestartButton;
    public Button QuitButton;

    public TextMeshProUGUI VictoryText;
    private void Awake()
    {
        if (Instance == null)
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
    private void Start()
    {
        // Ensure correct UI state at game start
        StartPanel.SetActive(true);
        VictoryPanel.SetActive(false);

        // Hook up button events
        StartSimButton.onClick.AddListener(OnStartSimulation);
        RestartButton.onClick.AddListener(OnRestart);
        QuitButton.onClick.AddListener(OnQuit);
    }

    private void OnStartSimulation()
    {
        StartPanel.SetActive(false);
        GameManager.Instance.StartMatch();
    }

    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    public void ShowVictoryScreen(string winnerName, string weaponName)
    {
        VictoryPanel.SetActive(true);
        VictoryText.text = $"Winner: {winnerName} - Weapon: {weaponName}";
    }
}
