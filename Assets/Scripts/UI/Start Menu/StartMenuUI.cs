using UnityEngine;
using Zenject;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_menuObject;
    [SerializeField]
    private GameObject m_buttonsPanel;
    [SerializeField]
    private GameObject m_loadingPanel;

    private GameManager m_gameManager;
    private SignalBus m_signalBus;

    [Inject]
    private void Construct(GameManager gameManager, SignalBus signalBus)
    {
        m_gameManager = gameManager;
        m_signalBus = signalBus;
    }

    private void Awake()
    {
        m_signalBus.Subscribe<GamePrepareStartedSignal>(OnPrepareStarted);
        m_signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        m_signalBus.Subscribe<GameFinishedSignal>(OnGameFinished);
    }

    private void OnDestroy()
    {
        m_signalBus.Unsubscribe<GamePrepareStartedSignal>(OnPrepareStarted);
        m_signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        m_signalBus.Unsubscribe<GameFinishedSignal>(OnGameFinished);
    }

    public void StartGame()
    {
        m_gameManager.StartGame();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnPrepareStarted(GamePrepareStartedSignal prepareStartedSignal)
    {
        m_buttonsPanel.gameObject.SetActive(false);
        m_loadingPanel.gameObject.SetActive(true);
    }

    private void OnGameStarted(GameStartedSignal gameStartedSignal)
    {
        m_menuObject.gameObject.SetActive(false);

        m_signalBus.Fire(new GameUnlockCursor());
    }

    private void OnGameFinished(GameFinishedSignal gameFinishedSignal)
    {
        m_menuObject.gameObject.SetActive(true);
        m_buttonsPanel.gameObject.SetActive(true);
        m_loadingPanel.gameObject.SetActive(false);

        m_signalBus.Fire(new GameLockCursor());
    }
}
