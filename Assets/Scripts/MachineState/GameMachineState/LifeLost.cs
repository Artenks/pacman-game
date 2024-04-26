
using System.Collections.Generic;
using UnityEngine;

public class LifeLost : GameMachineState
{
    private float _lifeLostTimer = 0;
    private bool _isGameOver = false;
    private int _remainingLives = 0;

    public LifeLost(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles, int remainingLives)
        : base(manager, ghostHouse, pacman, ghosts, allCollectibles)
    {
        State = GameState.LifeLost;
        _remainingLives = remainingLives;
    }

    protected override void Enter()
    {
        _lifeLostTimer = Manager.LifeLostTimer;
        GhostHouse.Reset();

        base.Enter();
    }
    protected override void Update()
    {
        _lifeLostTimer -= Time.deltaTime;

        if (_lifeLostTimer <= 0)
        {
            _isGameOver = _remainingLives <= 0;

            if (_isGameOver)
            {
                Debug.Log("Game Over");
                NextState = new GameOver(Manager, GhostHouse, Pacman, Ghosts, AllCollectibles);
                Event = EventState.Exit;
                return;
            }
            else
            {
                Debug.Log("Lost a life");
                NextState = new Playing(Manager, GhostHouse, Pacman, Ghosts, AllCollectibles);
                Event = EventState.Exit;
                return;
            }
        }

        base.Update();
    }
    protected override void Exit()
    {
        ResetAllCharacters();
        base.Exit();
    }

}
