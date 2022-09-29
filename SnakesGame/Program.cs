using Nibbles.Engine;
using SnakesGame.Engine;
using System.Drawing;

var inputReader = new KeyboardReader();
var player = new Player(inputReader);
var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
var renderer = new SpriteRenderer();
var collisionDetector = new CollisionDetector(gameState);
var scoreStore = new TopScoreStore();
var soundGenerator = new SoundGenerator();
var snakesGame = new SnakesStateReducer(gameState, renderer, collisionDetector, scoreStore, soundGenerator);

new SnakesGameLoop(renderer, snakesGame).Start();
Console.ReadLine();
snakesGame.ShowTopScores();
Console.ReadLine();
