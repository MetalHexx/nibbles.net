namespace Nibbles
{
    internal class SnakerRenderer: ScreenWriter
    {
        public void Render(IEnumerable<SnakePart> snakeParts)
        {
            foreach (var snakePart in snakeParts)
            {
                WriteCharacter('S', ConsoleColor.Green, snakePart.Position.XPosition, snakePart.Position.YPosition);
            }
        }

        public void Clear(IEnumerable<SnakePart> snakeParts)
        {
            foreach (var snakePart in snakeParts)
            {
                WriteCharacter(' ', ConsoleColor.Green, snakePart.Position.XPosition, snakePart.Position.YPosition);
            }
        }
    }    
}
