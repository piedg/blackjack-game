using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("In Game UI")]
    [SerializeField] Button shuffleButton;
    [SerializeField] Button nextRoundButton;
    [SerializeField] Button pauseButton;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;

    [Header("Winner Panel")]
    [SerializeField] GameObject winnerPanel;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] TextMeshProUGUI currentTurnText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        resumeButton.onClick.AddListener(OnResumeButton);
        restartButton.onClick.AddListener(OnRestartButton);

        pauseButton.onClick.AddListener(OnPauseButton);
        shuffleButton.onClick.AddListener(Deck.Instance.ShuffleDeck);

        nextRoundButton.onClick.AddListener(OnNextRoundButton);
    }

    public void UpdateCurrentTurnText(string text)
    {
        currentTurnText.text = text.ToUpper();
    }

    public void ShowWinnerText(string text)
    {
        winnerText.text = text.ToUpper();
        ShowWinPanel(true);
        ShowInGameUI(false);
    }

    private void OnResumeButton()
    {
        ShowPauseMenuPanel(false);
        ShowInGameUI(true);
    }

    private void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPauseButton()
    {
        ShowPauseMenuPanel(true);
        ShowInGameUI(false);
    }

    private void OnNextRoundButton()
    {

        // TODO remove line below
        OnRestartButton();

        //GameManager.Instance.NextRound();

        ShowInGameUI(true);
        ShowWinPanel(false);
        ShowPauseMenuPanel(false);
    }

    private void ShowInGameUI(bool value)
    {
        pauseButton.gameObject.SetActive(value);
        shuffleButton.gameObject.SetActive(value);
        currentTurnText.gameObject.SetActive(value);

        // TODO 
        // add text for current points
        // add text for status (?)
    }

    private void ShowPauseMenuPanel(bool value)
    {
        pauseMenuPanel.SetActive(value);
    }

    private void ShowWinPanel(bool value)
    {
        winnerPanel.SetActive(value);
    }
}
