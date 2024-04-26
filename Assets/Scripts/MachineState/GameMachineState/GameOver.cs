
using System.Collections.Generic;

public class GameOver : EndGame
{
    public GameOver(GameManager manager, GhostHouse ghostHouse, CharacterMotor pacman, GhostAI[] ghosts, List<Collectible> allCollectibles)
        : base(manager, ghostHouse, pacman, ghosts, allCollectibles)
    {
        State = GameState.GameOver;
    }

    protected override void Enter()
    {
        StopAllCharacters();

        base.Enter();
    }
    protected override void Update()
    {
        Manager.OnGameOver?.Invoke();

        base.Update();
    }
    protected override void Exit()
    {
        base.Exit();
    }

}