using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    public enum EGameState
    {
        Ready,
        Play,
        GameOver
    }

    private GroundParallax _groundParallax;
    private Bird _bird;

    public EGameState GameState { get; private set; } = EGameState.Ready;
    public int Score { get; private set; } = 0;
    public int BestScore
    {
        get => PlayerPrefs.GetInt("BestScore", 0);
        set => PlayerPrefs.SetInt("BestScore", value);
    }

    protected override void Awake()
    {
        base.Awake();

        _groundParallax = FindAnyObjectByType<GroundParallax>();
        _bird = FindFirstObjectByType<Bird>();
    }

    private void Start()
    {
        ChangeState(EGameState.Ready);
    }

    private void Update()
    {
        switch (GameState)
        {
            case EGameState.Ready:
                if (Input.GetMouseButtonDown(0))
                {
                    _bird.StartFly();
                    ChangeState(EGameState.Play);
                    UIManager.Instance.SetUI(GameState);
                }
                break;
            case EGameState.Play:
                if (Input.GetMouseButtonDown(0))
                {
                    _bird.ForceFly();
                }
                _bird.CheckRotation();
                break;
            case EGameState.GameOver: break;
            default: break;
        }
    }

    public void ChangeState(EGameState newState)
    {
        GameState = newState;

        switch (GameState)
        {
            case EGameState.Ready:
                Score = 0;
                UIManager.Instance.SetScore(Score);
                _groundParallax.StartParallax();
                _bird.Reset();
                UIManager.Instance.SetBestText(false);
                break;
            case EGameState.Play:
                Score = 0;
                _bird.StartFly();
                PipeManager.Instance.StartSpawn();
                break;
            case EGameState.GameOver:
                if (Score > BestScore)
                {
                    UIManager.Instance.SetBestText(true);
                    BestScore = Score;
                }
                _groundParallax.StopParallax();
                PipeManager.Instance.StopSpawn();
                _bird.GameOver();
                break;
            default:
                break;
        }
    }

    public void IncreaseScore()
    {
        Score++;
        UIManager.Instance.SetScore(Score);
    }

    public void ResetGame()
    {
        ChangeState(EGameState.Ready);
        UIManager.Instance.SetUI(GameState);
    }

    public void GameOver()
    {
        ChangeState(EGameState.GameOver);
        UIManager.Instance.SetUI(GameState);
    }
}
