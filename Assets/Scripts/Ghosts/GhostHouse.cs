using System.Collections.Generic;
using UnityEngine;

public class GhostHouse : MonoBehaviour
{
    public float LeaveHouseInterval;
    List<GhostAI> _allGhost;

    private float _leaveHouseTimer;

    public void Reset()
    {
        _leaveHouseTimer += LeaveHouseInterval;
    }

    private void Awake()
    {
        _allGhost = new List<GhostAI>();
        _leaveHouseTimer = LeaveHouseInterval;
    }
    private void Update()
    {
        if (_allGhost.Count > 0)
        {
            _leaveHouseTimer -= Time.deltaTime;
            if (_leaveHouseTimer <= 0)
            {
                _leaveHouseTimer += LeaveHouseInterval;
                _allGhost[0].LeaveHouse();
                _allGhost.RemoveAt(0);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ghost = other.GetComponent<GhostAI>();

        if (_allGhost.Count == 0)
        {
            _leaveHouseTimer = LeaveHouseInterval;
        }

        _allGhost.Add(ghost);
    }
}
