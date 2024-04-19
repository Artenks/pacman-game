using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;
    public event Action<int, GhostAI> OnShowGhostScore;

    private int _currentScore;

    private int _highScore;
    private GhostScoreData _ghostScoreData;
    private int _currentGhostCaughtCount;

    public int CurrentScore
    {
        get => _currentScore;
    }
    public int HightScore { get => _highScore; }

    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("high-score", 0);
    }

    void Start()
    {
        var allCollectibles = FindObjectsOfType<Collectible>();
        foreach (var collectible in allCollectibles)
        {
            collectible.OnCollected += Collectible_OnCollected;
        }

        //var eatGhost = FindObjectOfType<EatGhost>();
        //eatGhost.OnEatGhost += EatGhost_OnEatGhost;

        var allGhosts = FindObjectsOfType<GhostAI>();
        foreach (var ghost in allGhosts)
        {
            ghost.OnGhoustCaught += GhostAI_OnGhoustCaught;
        }
    }

    private void GhostAI_OnGhoustCaught(GhostAI ghost)
    {
        _currentGhostCaughtCount++;

        int score = _ghostScoreData.GhostScore + _ghostScoreData.GhostScoreIncrement * _currentGhostCaughtCount;

        AddScore(score);

        OnShowGhostScore?.Invoke(score, ghost);

        Debug.Log($"devorados:{_currentGhostCaughtCount} | +{_ghostScoreData.GhostScoreIncrement * _currentGhostCaughtCount}");
    }

    //private void EatGhost_OnEatGhost(int score)
    //{
    //    AddScore(score);
    //}

    private void Collectible_OnCollected(int score, Collectible collectible)
    {
        AddScore(score);

        collectible.OnCollected -= Collectible_OnCollected;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("high-score", _highScore);
    }

    private void AddScore(int score)
    {
        _currentScore += score;
        OnScoreChanged?.Invoke(_currentScore);

        if (_currentScore >= _highScore)
        {
            _highScore = _currentScore;
            OnHighScoreChanged?.Invoke(_highScore);
        }
    }

    public void EnergizerActivated(GhostScoreData ghostScoreData)
    {
        _ghostScoreData = ghostScoreData;
        _currentGhostCaughtCount = 0;
    }
}
