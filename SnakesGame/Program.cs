using Nibbles.Engine;
using SnakesGame.Engine;
using System.Drawing;

var inputReader = new KeyboardReader();
var player = new Player(inputReader);
var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
var renderer = new SpriteRenderer();
var collisionDetector = new CollisionDetector(gameState);
var engine = new SnakesManager(gameState, renderer, collisionDetector);

new GameLoop(renderer, engine).Start();
Console.ReadLine();
