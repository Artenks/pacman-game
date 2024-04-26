using UnityEngine;

public class Vulnerable : Vulnerabilitys
{
    public Vulnerable(GhostMove ghostMove, GhostAI ghostAI, Transform pacman, (float total, float ending) duration)
        : base(ghostMove, ghostAI, pacman, duration)
    {
        State = GhostState.Vulnerable;
    }

    protected override void Enter()
    {
        GhostMove.AllowReverseDirection();

        base.Enter();
    }

    protected override void Update()
    {
        base.Update();

        if (TimerEnd)
        {
            NextState = new VulnerableEnding(GhostMove, GhostAI, Pacman, (VulnerabilityTimer, 0));
            Event = Event.Exit;
            return;
        }

    }
    protected override void Exit()
    {
        base.Exit();
    }
}
