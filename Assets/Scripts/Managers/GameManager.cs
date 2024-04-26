using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float StartupTime;

    public float LifeLostTimer;

    private GhostAI[] _allGhosts;
    private CharacterMotor _pacmanMotor;

    private GhostHouse _ghostHouse;

    public Action OnGameStarted;
    public Action OnVictory;
    public Action OnGameOver;

    private GameMachineState _nowGameMachineState;

    private void Start()
    {
        var allColletibles = FindObjectsOfType<Collectible>();
        var pacman = GameObject.FindWithTag("Player");
        _pacmanMotor = pacman.GetComponent<CharacterMotor>();
        _allGhosts = FindObjectsOfType<GhostAI>();

        _ghostHouse = FindObjectOfType<GhostHouse>();

        _nowGameMachineState = new Starting
            (this, _ghostHouse, _pacmanMotor, _allGhosts, allColletibles.ToList());
    }
    void Update()
    {
        _nowGameMachineState = _nowGameMachineState.Handle();
    }
}
