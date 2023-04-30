using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Button shuffleButton;
    [SerializeField] Button nextRoundButton;
    [SerializeField] Button pauseMenuButton;

    [SerializeField] GameObject menuPanel;
    [SerializeField] Button resumeMenuButton;

    [SerializeField] TextMeshProUGUI currentTurnText;
    [SerializeField] TextMeshProUGUI winnerText;

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
        shuffleButton.onClick.AddListener(Deck.Instance.ShuffleDeck);
        pauseMenuButton.onClick.AddListener(OpenInGameMenu);
        resumeMenuButton.onClick.AddListener(CloseInGameMenu);
    }

    public void UpdateCurrentTurnText(string text)
    {
        currentTurnText.text = text.ToUpper();
    }

    public void ShowWinnerText(string text)
    {
        winnerText.gameObject.SetActive(true);

        winnerText.text = text.ToUpper();

        pauseMenuButton.gameObject.SetActive(false);
        shuffleButton.gameObject.SetActive(false);
        currentTurnText.gameObject.SetActive(false);
    }

    public void OpenInGameMenu()
    {
        menuPanel.SetActive(true);

        shuffleButton.gameObject.SetActive(false);
        currentTurnText.gameObject.SetActive(false);
        pauseMenuButton.gameObject.SetActive(false);
    }

    public void CloseInGameMenu()
    {
        menuPanel.SetActive(false);

        shuffleButton.gameObject.SetActive(true);
        currentTurnText.gameObject.SetActive(true);
        pauseMenuButton.gameObject.SetActive(true);
    }
}
