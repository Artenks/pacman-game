
using System.Collections.Generic;
using UnityEngine;

public class Starting : GameMachineState
{
    private float _startupTime;

    public Starting(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles)
        : base(manager, ghostHouse, pacman, ghosts, allCollectibles)
    {
        State = GameState.Starting;
    }

    protected override void Enter()
    {
        _startupTime = Manager.StartupTime;

        StopAllCharacters();
        GhostHouse.enabled = false;

        base.Enter();
    }
    protected override void Update()
    {
        _startupTime -= Time.deltaTime;

        if (_startupTime <= 0)
        {
            ResetAllCharacters();
            GhostHouse.enabled = true;

            NextState = new Playing(Manager, GhostHouse, Pacman, Ghosts, AllCollectibles);
            Event = EventState.Exit;
            return;
        }

        base.Update();
    }
    protected override void Exit()
    {
        Manager.OnGameStarted?.Invoke();

        base.Exit();
    }

}
