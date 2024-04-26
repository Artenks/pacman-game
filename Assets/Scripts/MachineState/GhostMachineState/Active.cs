using UnityEngine;

public class Active : GhostMachineState
{
    private bool _getOutHouse = false;
    private bool _isVulnerable = false;

    public Active(GhostMove ghostMove, GhostAI ghostAI, Transform pacman)
        : base(ghostMove, ghostAI, pacman)
    {
        State = GhostState.Active;
        SetVulnerability += ((float, float) duration) =>
        {
            _isVulnerable = true;

            var state = new Vulnerable(GhostMove, ghostAI, pacman, duration);
            NextState = state;
            Event = Event.Exit;
            Exit();
        };

    }

    protected override void Detect()
    {

        if (!Collided.isCollided)
            return;

        if (Collided.obj.CompareTag("Player"))
        {
            Collided.obj.GetComponent<Life>().RemoveLife();
        }

        base.Detect();
    }

    protected override void Enter()
    {
        GhostMove.CharacterMotor.CollideWithGates(true);

        base.Enter();
    }

    protected override void Update()
    {
        if (_isVulnerable)
            return;


        if (_getOutHouse)
        {
            var seeDistance = GhostMove.transform.position - new Vector3(0, 3, 0);
            if (seeDistance.magnitude <= 0.1f)
            {
                GhostMove.CharacterMotor.CollideWithGates(true);
                GhostMove.SetTargetMoveLocation(Pacman.position);
                _getOutHouse = false;
            }
            else
            {
                GhostMove.SetTargetMoveLocation(new Vector3(0, 3, 0));
            }
        }
        else
        {
            GhostMove.SetTargetMoveLocation(Pacman.position);
        }

        if (GhostMove.transform.position == Vector3.zero)
        {
            _getOutHouse = true;
        }


        base.Update();
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
