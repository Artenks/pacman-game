using UnityEngine;

public class VulnerableEnding : Vulnerabilitys
{
    public VulnerableEnding(GhostMove ghostMove, GhostAI ghostAI, Transform pacman, (float total, float ending) duration)
        : base(ghostMove, ghostAI, pacman, duration)
    {
        State = GhostState.VulnerabilityEnding;
    }

    protected override void Enter()
    {
        base.Enter();
    }
    protected override void Update()
    {
        base.Update();

        if (TimerEnd)
        {
            NextState = new Active(GhostMove, GhostAI, Pacman);
            Event = Event.Exit;
            return;
        }
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
