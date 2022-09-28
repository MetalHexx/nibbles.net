using Nibbles.Engine;
using SnakesGame.Engine;
using System.Drawing;

var inputReader = new KeyboardReader();
var player = new Player(inputReader);
var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
var renderer = new SpriteRenderer();
var collisionDetector = new CollisionDetector(gameState);
var scoreStore = new TopScoreStore();
var snakesGame = new SnakesManager(gameState, renderer, collisionDetector, scoreStore);

new GameLoop(renderer, snakesGame).Start();
Console.ReadLine();
snakesGame.ShowTopScores();
Console.ReadLine();
