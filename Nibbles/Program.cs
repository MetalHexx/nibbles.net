using Nibbles.Engine;
using Nibbles.Player;

var inputReader = new KeyboardReader();
var playerInput = new PlayerInput(inputReader);
var renderer = new SpriteRenderer();
var gameState = new GameState();
var collisionDetector = new CollisionDetector(gameState);
var gameStateHandler = new NibblesGame(gameState, renderer, collisionDetector);

new Engine(playerInput, renderer, gameStateHandler).Start();
Console.ReadLine();
