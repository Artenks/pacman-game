
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : GameMachineState
{
    public EndGame(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles)
        : base(manager, ghostHouse, pacman, ghosts, allCollectibles)
    { }

    protected override void Enter()
    {
        base.Enter();
    }
    protected override void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
            return;
        }

        base.Update();
    }
    protected override void Exit()
    {
        base.Exit();
    }

}
