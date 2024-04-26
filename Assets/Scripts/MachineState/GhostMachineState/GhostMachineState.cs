using System;
using UnityEngine;

public enum Event
{
    Enter,
    Update,
    Exit
}

public abstract class GhostMachineState
{
    [Serializable]
    public enum GhostState
    {
        Active,
        Vulnerable,
        VulnerabilityEnding,
        Defeated,
        Ressurect
    }

    protected GhostState _state;
    protected GhostState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            GhostAI.OnGhostStateChanged?.Invoke((global::GhostState)_state);
        }
    }

    protected Event Event;

    protected GhostMachineState NextState;

    //

    protected GhostMove GhostMove;
    protected GhostAI GhostAI;
    protected Transform Pacman;

    protected (bool isCollided, Collider2D obj) Collided;

    public Action<(float total, float ending)> SetVulnerability;

    //
    public GhostMachineState(GhostMove ghostMove, GhostAI ghostAI, Transform pacman)
    {
        GhostMove = ghostMove;
        Pacman = pacman;
        GhostAI = ghostAI;
    }
    public GhostMachineState(GhostMove ghostMove, GhostAI ghostAI, Transform pacman, float duration)
    {
        GhostMove = ghostMove;
        Pacman = pacman;
        GhostAI = ghostAI;
    }

    public void OnColission(Collider2D other)
    {
        var isCollided = (other != null ? true : false);
        var obj = (other != null ? other : null);

        Collided = (isCollided, obj);
    }

    public GhostState GetState()
    {
        return State;
    }

    //
    protected virtual void Detect() => Collided = (false, null);

    protected virtual void Enter() => Event = Event.Update;

    protected virtual void Update() => Event = Event.Update;

    protected virtual void Exit() => Event = Event.Exit;

    public GhostMachineState Handle()
    {
        switch (Event)
        {
            case Event.Enter:
                Enter();
                break;
            case Event.Update:
                Detect();
                Update();
                break;

            case Event.Exit:
                Exit();
                return NextState;
        }

        return this;
    }
}
