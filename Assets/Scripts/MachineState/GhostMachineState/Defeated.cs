using UnityEngine;

public class Defeated : GhostMachineState
{
    public Defeated(GhostMove ghostMove, GhostAI ghostAI, Transform pacman)
        : base(ghostMove, ghostAI, pacman)
    {
        State = GhostState.Defeated;
    }

    protected override void Enter()
    {
        GhostMove.SetTargetMoveLocation(new Vector3(0, 3, 0));

        base.Enter();
    }
    protected override void Update()
    {
        var seeDistance = GhostMove.transform.position - new Vector3(0, 3, 0);
        if (seeDistance.magnitude <= 0.05f)
        {
            GhostMove.SetTargetMoveLocation(Vector3.zero);
            GhostMove.CharacterMotor.CollideWithGates(false);
        }

        seeDistance = GhostMove.transform.position - Vector3.zero;
        if (seeDistance.magnitude <= 0.05f)
        {
            GhostMove.CharacterMotor.CollideWithGates(true);

            NextState = new Ressurect(GhostMove, GhostAI, Pacman);
            Event = Event.Exit;
            return;
        }

        base.Update();
    }

    protected override void Exit()
    {
        base.Exit();
    }
}