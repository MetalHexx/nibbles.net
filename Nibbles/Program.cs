using Nibbles.Engine;
using Nibbles.Player;

var inputReader = new KeyboardReader();
var playerInput = new PlayerInput(inputReader);
var renderer = new SpriteRenderer();
var gameState = new GameState();
var gameStateHandler = new GameStateHandler(gameState);

new Engine(playerInput, renderer, gameStateHandler).Start();
Console.ReadLine();
