using System;
using UnityEngine;
using static GhostMachineState;

public enum GhostState
{
    Active,
    Vulnerable,
    VulnerabilityEnding,
    Defeated
}

[RequireComponent(typeof(GhostMove))]
public class GhostAI : MonoBehaviour
{
    public Action<GhostState> OnGhostStateChanged;

    public Action<GhostAI> OnGhoustCaught;

    public float VulnerabilityEndingTime;

    private GhostMove _ghostMove;

    private Transform _pacman;

    public GhostMachineState _nowGhostMachineState { get; private set; }

    public void LeaveHouse()
    {
        _ghostMove.CharacterMotor.CollideWithGates(false);
    }
    public void SetVulnerable(float duration)
    {
        _nowGhostMachineState.SetVulnerability?.Invoke((duration, VulnerabilityEndingTime));
    }

    public void StartMoving()
    {
        _ghostMove.CharacterMotor.enabled = true;
    }
    public void StopMoving()
    {
        _ghostMove.CharacterMotor.enabled = false;
    }
    public void Reset()
    {
        _ghostMove.CharacterMotor.ResetPosition();

        _nowGhostMachineState = new Active(_ghostMove, this, _pacman);
    }

    void Start()
    {
        _ghostMove = GetComponent<GhostMove>();
        _pacman = GameObject.FindWithTag("Player").transform;

        _nowGhostMachineState = new Active(_ghostMove, this, _pacman);

    }

    private void Update()
    {
        _nowGhostMachineState = _nowGhostMachineState.Handle();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _nowGhostMachineState.OnColission(other);
    }
}

