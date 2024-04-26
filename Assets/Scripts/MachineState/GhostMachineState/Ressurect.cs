using UnityEngine;

public class Ressurect : GhostMachineState
{
    public Ressurect(GhostMove ghostMove, GhostAI ghostAI, Transform pacman)
        : base(ghostMove, ghostAI, pacman)
    {
        State = GhostState.Ressurect;
    }

    protected override void Enter()
    {
        GhostMove.SetTargetMoveLocation(new Vector3(0, 3, 0));

        base.Enter();
    }

    protected override void Update()
    {

        var seeDistance = GhostMove.transform.position - new Vector3(0, 3, 0);
        if (seeDistance.magnitude <= 0.2f)
        {
            GhostMove.CharacterMotor.CollideWithGates(true);
            GhostMove.SetTargetMoveLocation(Pacman.position);

            NextState = new Active(GhostMove, GhostAI, Pacman);
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
