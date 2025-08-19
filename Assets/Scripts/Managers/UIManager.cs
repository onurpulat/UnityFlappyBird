using TMPro;
using UnityEngine;

public class UIManager : ManagerBase<UIManager>
{
    [SerializeField] private GameObject m_readyPanel;

    [SerializeField] private GameObject m_playPanel;
    [SerializeField] private TextMeshProUGUI m_playScoreText;

    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private GameObject m_bestText;
    [SerializeField] private TextMeshProUGUI m_gameOverScoreText;

    public void SetUI(GameManager.EGameState gameState)
    {
        m_readyPanel.SetActive(false);
        m_playPanel.SetActive(false);
        m_gameOverPanel.SetActive(false);

        switch (gameState)
        {
            case GameManager.EGameState.Ready:
                m_readyPanel.SetActive(true);
                break;

            case GameManager.EGameState.Play:
                m_playPanel.SetActive(true);
                break;

            case GameManager.EGameState.GameOver:
                m_gameOverPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetBestText(bool value) => m_bestText.SetActive(value);

    public void SetScore(int score)
    {
        m_playScoreText.text = score.ToString();
        m_gameOverScoreText.text = score.ToString();
    }
}
