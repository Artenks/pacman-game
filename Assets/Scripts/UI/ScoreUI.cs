using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI CurrentScoreText;

    public TextMeshProUGUI HighScoreText;

    public AudioSource AudioSource;
    public AudioClip GhostScoreSound;
    public float PauseTime;

    public TextMeshPro ScorePrefab;

    private PacmanView _pacmanView;

    private void Start()
    {
        var scoreManager = FindObjectOfType<ScoreManager>();

        scoreManager.OnScoreChanged += ScoreManager_OnScoreChanged;
        scoreManager.OnHighScoreChanged += ScoreManager_OnHighScoreChanged;

        scoreManager.OnShowGhostScore += ScoreManager_OnShowGhostScore;

        _pacmanView = GameObject.FindWithTag("Player").GetComponentInChildren<PacmanView>();

        ScoreManager_OnHighScoreChanged(scoreManager.HightScore);
    }


    private void ScoreManager_OnHighScoreChanged(int HighScore)
    {
        HighScoreText.text = $"{HighScore:00}";
    }

    private void ScoreManager_OnScoreChanged(int currentScore)
    {
        CurrentScoreText.text = $"{currentScore:00}";
    }
    private void ScoreManager_OnShowGhostScore(int score, GhostAI ghost)
    {
        StartCoroutine(GhostScoreCoroutine(score, ghost));
    }

    private IEnumerator GhostScoreCoroutine(int score, GhostAI ghost)
    {
        AudioSource.PlayOneShot(GhostScoreSound);
        Time.timeScale = 0;

        var ghostView = ghost.GetComponentInChildren<GhostView>();

        _pacmanView.Hide();
        ghostView.Hide();

        var scoreTextInstance = Instantiate(ScorePrefab, ghost.transform.position, Quaternion.identity);
        scoreTextInstance.text = score.ToString();

        yield return new WaitForSecondsRealtime(PauseTime);

        _pacmanView.Show();
        ghostView.Show();

        Destroy(scoreTextInstance.gameObject);

        Time.timeScale = 1;
    }
}
