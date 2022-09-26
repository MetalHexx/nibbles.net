using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class Menu : BorderedBox
    {        
        private List<GameText> _menuItems = new List<GameText>();
        public GameText SelectedMenuItem { get; private set; }
        private GameColor SelectedMenuItem_ForegroundColor = GameColor.Black;
        private GameColor SelectedMenuItem_BackgroundColor = GameColor.White;
        private GameColor UnselectedMenuItem_ForegroundColor = GameColor.White;
        private GameColor UnselectedMenuItem_BackgroundColor;

        public Menu(Point position, Size size, int zIndex, string menuText, List<string> menuItems, GameColor foregroundColor, GameColor backgroundColor) : base(position, size, zIndex, foregroundColor, backgroundColor)
        {
            UnselectedMenuItem_BackgroundColor = backgroundColor;

            var menuTitle = new GameText(new Point(position.X + 1, position.Y + 2), 1, menuText, foregroundColor, backgroundColor);

            for (int i = 0; i < menuItems.Count; i++)
            {
                GameColor fgColor;
                GameColor bgColor;

                if(i == 0)
                {
                    fgColor = SelectedMenuItem_ForegroundColor;
                    bgColor = SelectedMenuItem_BackgroundColor;
                }
                else
                {
                    fgColor = UnselectedMenuItem_ForegroundColor;
                    bgColor = UnselectedMenuItem_BackgroundColor;
                }

                var menuItem = new GameText(new Point(position.X + 2, position.Y + 3 + i), 1, menuItems[i], fgColor, bgColor);

                if(i == 0)
                {
                    SelectedMenuItem = menuItem;
                }
                menuItem.SpriteCreated += Add;
                menuItem.SetText(menuItems[i]);  
                _menuItems.Add(menuItem);
            }

            menuTitle.SpriteCreated += Add;
            menuTitle.SetText(menuText);

        }

        public void SelectNextMenuItem(DirectionType direction)
        {
            var currentIndex = _menuItems.IndexOf(SelectedMenuItem);

            SelectedMenuItem.ForegroundColor = UnselectedMenuItem_ForegroundColor;
            SelectedMenuItem.BackgroundColor = UnselectedMenuItem_BackgroundColor;
            SelectedMenuItem.SetText(SelectedMenuItem.Text);

            switch (direction)
            {
                case DirectionType.Down:
                    
                    if(currentIndex == _menuItems.Count - 1)
                    {
                        currentIndex = 0;
                    }
                    else
                    {
                        currentIndex++;
                    }
                    SelectedMenuItem = _menuItems[currentIndex];
                    break;

                case DirectionType.Up:
                    if (currentIndex == 0)
                    {
                        currentIndex = _menuItems.Count - 1;
                    }
                    else
                    {
                        currentIndex--;
                    }                    
                    SelectedMenuItem = _menuItems[currentIndex];
                    break;

                default: return;
            }

            SelectedMenuItem.ForegroundColor = SelectedMenuItem_ForegroundColor;
            SelectedMenuItem.BackgroundColor = SelectedMenuItem_BackgroundColor;
            SelectedMenuItem.SpriteCreated += Add;
            SelectedMenuItem.SetText(SelectedMenuItem.Text);

        }
    }
}
