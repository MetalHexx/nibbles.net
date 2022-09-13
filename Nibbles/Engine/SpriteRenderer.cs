using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;

namespace Nibbles.Engine
{
    internal class SpriteRenderer : ISpriteRenderer
    {  
        public SpriteRenderer()
        {
            Console.CursorVisible = false;
        }

        public void RenderSprites(SpriteRenderUpdate update)
        {
            Destroy(update.SpritesToRemove);
            Render(update.SpritesToAdd);
        }

        private void Render(IEnumerable<ISprite?> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is null) continue;

                WriteText(sprite.DisplayCharacter,
                    sprite.ForegroundColor,
                    sprite.BackgroundColor,
                    sprite.GetPosition().XPosition,
                    sprite.GetPosition().YPosition);
            }
        }

        private void Destroy(IEnumerable<ISprite?> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is null) continue;

                WriteText(' ',
                    SpriteConfig.BOARD_BACKGROUND_COLOR,
                    SpriteConfig.BOARD_BACKGROUND_COLOR,
                    sprite.GetPosition().XPosition,
                    sprite.GetPosition().YPosition);
            }
        }

        private void WriteText(char character, 
            ConsoleColor foregroundColor, 
            ConsoleColor backgroundColor, 
            int xPosition, 
            int yPosition)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(character);
            Console.ResetColor();
        }
    }
}