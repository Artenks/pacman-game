
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMachineState
{
    public enum GameState
    {
        Starting,
        Playing,
        LifeLost,
        GameOver,
        Victory
    }

    public enum EventState
    {
        Start,
        Update,
        Exit
    }

    protected GameState State;
    protected EventState Event;
    protected GameMachineState NextState;

    protected GameManager Manager;
    protected GhostHouse GhostHouse;
    protected CharacterMotor Pacman;
    protected GhostAI[] Ghosts;
    protected List<Collectible> AllCollectibles;

    protected virtual void Enter() => Event = EventState.Update;
    protected virtual void Update() => Event = EventState.Update;
    protected virtual void Exit() => Event = EventState.Exit;

    public GameMachineState(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles)
    {
        Manager = manager;
        GhostHouse = ghostHouse;
        Pacman = pacman;
        Ghosts = ghosts;
        AllCollectibles = allCollectibles;
    }

    public GameMachineState Handle()
    {
        switch (Event)
        {
            case EventState.Start:
                Debug.Log(State);
                Enter();
                break;

            case EventState.Update:
                Update();
                break;

            case EventState.Exit:
                Exit();
                return NextState;
        }
        return this;
    }

    protected void StartAllCharacters()
    {
        Pacman.enabled = true;

        foreach (var ghost in Ghosts)
        {
            ghost.StartMoving();
        }
    }
    protected void StopAllCharacters()
    {
        Pacman.enabled = false;

        foreach (var ghost in Ghosts)
        {
            ghost.StopMoving();
        }
    }
    protected void ResetAllCharacters()
    {
        Pacman.ResetPosition();

        foreach (var ghost in Ghosts)
        {
            ghost.Reset();
        }

        StartAllCharacters();
    }
}
