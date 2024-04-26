
using System.Collections.Generic;

public class Victory : EndGame
{
    public Victory(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles)
        : base(manager, ghostHouse, pacman, ghosts, allCollectibles)
    {
        State = GameState.Victory;
    }

    protected override void Enter()
    {
        base.Enter();
    }
    protected override void Update()
    {
        StopAllCharacters();
        Manager.OnVictory?.Invoke();

        base.Update();
    }
    protected override void Exit()
    {
        base.Exit();
    }

}
