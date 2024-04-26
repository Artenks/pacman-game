
using System.Collections.Generic;
using UnityEngine;

public class Playing : GameMachineState
{
    private int _victoryCount;
    private bool _lifeLost = false;
    private int _remainingLives = 0;

    public Playing(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles)
        : base(manager, ghostHouse, pacman, ghosts, allCollectibles)
    {
        State = GameState.Playing;
    }

    protected override void Enter()
    {
        foreach (var collectible in AllCollectibles)
        {
            if (collectible.IsVictoryCondition)
            {
                _victoryCount++;
                collectible.OnCollected += Collectible_OnCollected;
            }
        }

        Pacman.GetComponent<Life>().BeforeLifeRemoved += Pacman_BeforeLifeRemoved;

        base.Enter();
    }
    protected override void Update()
    {
        if (_lifeLost)
        {
            NextState = new LifeLost(Manager, GhostHouse, Pacman, Ghosts, AllCollectibles, _remainingLives);
            Event = EventState.Exit;
            return;
        }

        if (_victoryCount <= 0)
        {
            Debug.Log("Victory");
            NextState = new Victory(Manager, GhostHouse, Pacman, Ghosts, AllCollectibles);
            Event = EventState.Exit;
            return;
        }

        base.Update();
    }
    protected override void Exit()
    {

        base.Exit();
    }

    private void Pacman_BeforeLifeRemoved(int remainingLives)
    {
        StopAllCharacters();
        _remainingLives = remainingLives;

        _lifeLost = true;
    }

    private void Collectible_OnCollected(int _, Collectible collectible)
    {
        _victoryCount--;

        collectible.OnCollected -= Collectible_OnCollected;
        AllCollectibles.Remove(collectible);
    }
}
