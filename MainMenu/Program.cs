using MainMenu;
using Nibbles.Engine;
using SnakesGame.Engine;
using System.Drawing;

ExecuteMainMenu();
Console.ReadLine();

static void ExecuteGame(string game)
{
    Console.Clear();
    switch (game)
    {
        case GameMenuConfig.SNAKES:
            PlaySnakes();
            break;
        default: return;
    }
    ExecuteMainMenu();
}

static void ExecuteMainMenu()
{
    var inputReader = new KeyboardReader();
    var player = new Player(inputReader);
    var renderer = new SpriteRenderer();
    var mainMenu = new MenuManager(renderer, player);
    mainMenu.GameSelected += ExecuteGame;
    new GameLoop(renderer, mainMenu).Start();
}

static void PlaySnakes()
{
    var inputReader = new KeyboardReader();
    var player = new Player(inputReader);
    var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
    var renderer = new SpriteRenderer();
    var collisionDetector = new CollisionDetector(gameState);
    var snakesGame = new SnakesManager(gameState, renderer, collisionDetector);    
    new GameLoop(renderer, snakesGame).Start();
    Console.ReadLine();
    
}