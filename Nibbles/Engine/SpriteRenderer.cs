using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;

namespace Nibbles.Engine
{
    internal class SpriteRenderer : ISpriteRenderer
    {
        private readonly List<ISprite> _spritesToAdd = new();
        private readonly List<ISprite> _spritesToRemove = new();

        public SpriteRenderer()
        {
            Console.CursorVisible = false;
        }

        public void Add(ISprite sprite)
        {
            _spritesToAdd.Add(sprite);
        }

        public void AddRange(IEnumerable<ISprite> sprites)
        {
            _spritesToAdd.AddRange(sprites);
        }

        public void Remove(ISprite sprite)
        {
            _spritesToRemove.Add(sprite);
        }

        public void Render()
        {
            Destroy(_spritesToRemove);
            _spritesToRemove.Clear();
            Render(_spritesToAdd);
            _spritesToAdd.Clear();
        }

        private static void Render(IEnumerable<ISprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                WriteText(sprite.DisplayCharacter,
                    sprite.ForegroundColor,
                    sprite.BackgroundColor,
                    sprite.Position.X,
                    sprite.Position.Y);
            }
        }

        private static void Destroy(IEnumerable<ISprite?> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is null) continue;

                WriteText(' ',
                    SpriteConfig.BOARD_BACKGROUND_COLOR,
                    SpriteConfig.BOARD_BACKGROUND_COLOR,
                    sprite.Position.X,
                    sprite.Position.Y);
            }
        }

        private static void WriteText(char character, 
            GameColor foregroundColor, 
            GameColor backgroundColor, 
            int xPosition, 
            int yPosition)
        {
            Console.ForegroundColor = foregroundColor.ToConsoleColor();
            Console.BackgroundColor = backgroundColor.ToConsoleColor();
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(character);
            Console.ResetColor();
        }
    }
}