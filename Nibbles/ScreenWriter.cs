namespace Nibbles
{
    public class ScreenWriter
    {
        public ScreenWriter()
        {
            Console.CursorVisible = false;
        }
        protected void WriteCharacter(char character, ConsoleColor color, int xPosition, int yPosition)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(character);
            Console.ResetColor();
        }
    }
}
