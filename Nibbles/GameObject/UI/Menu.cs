using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class Menu : BorderedBox
    {
        public MenuItem SelectedMenuItem { get; private set; }

        private List<MenuItem> _menuItems = new List<MenuItem>();
        private readonly string _menuText = "";
        private readonly List<string> _menuItemsText = new List<string>();        

        private GameColor SelectedMenuItem_ForegroundColor = GameColor.Black;
        private GameColor SelectedMenuItem_BackgroundColor = GameColor.White;
        private GameColor UnselectedMenuItem_ForegroundColor = GameColor.White;        
        private GameColor UnselectedMenuItem_BackgroundColor;

        public Menu(Point position, Size size, int zIndex, string menuText, IEnumerable<string> menuItems, GameColor foregroundColor, GameColor backgroundColor) : base(position, size, zIndex, foregroundColor, backgroundColor)
        {
            _menuText = menuText;
            _menuItemsText = menuItems.ToList();
            UnselectedMenuItem_BackgroundColor = backgroundColor;
            Initialize();
        }

        private void Initialize()
        {
            var yOffset = GetCenteredYPositionDelta();
            CreateMenuTitle(yOffset);
            CreateMenuItems(yOffset);
        }

        private int GetCenteredYPositionDelta()
        {
            var menuItemsAndTitleHeight = _menuItemsText.Count + 1;
            return (Size.Height - menuItemsAndTitleHeight) / 2 + 1;
        }

        private void CreateMenuTitle(int yOffset)
        {
            var menuTitlePositionX = Position.X + (Size.Width / 2) - _menuText.Length / 2;
            var menuTitle = new GameText(new Point(menuTitlePositionX, Position.Y + yOffset - 1), 1, _menuText, ForegroundColor, BackgroundColor);
            menuTitle.SpriteCreated += Add;
            menuTitle.SetText(_menuText);
        }

        private void CreateMenuItems(int yOffset)
        {
            var maxWidth = _menuItemsText.Max(item => item.Length) + 2;

            for (int menuItemIndex = 0; menuItemIndex < _menuItemsText.Count; menuItemIndex++)
            {
                var menuItem = CreateMenuItem(yOffset + menuItemIndex, maxWidth, _menuItemsText[menuItemIndex]);

                if (menuItemIndex == 0)
                {
                    menuItem.SelectItem();
                    SelectedMenuItem = menuItem;
                }
                else
                {
                    menuItem.DeselectItem();
                }                
                menuItem.SpriteCreated += Add;
                _menuItems.Add(menuItem);
                menuItem.SetText(_menuItemsText[menuItemIndex]);
            }
        }

        private MenuItem CreateMenuItem(int yOffset, int maxWidth, string menuItemText)
        {
            var menuItemPositionX = Position.X + (Size.Width / 2) - menuItemText.Length / 2;
            return new MenuItem(new Point(menuItemPositionX, Position.Y + yOffset), 1, menuItemText, maxWidth, UnselectedMenuItem_ForegroundColor, UnselectedMenuItem_BackgroundColor, SelectedMenuItem_ForegroundColor, SelectedMenuItem_BackgroundColor);
        }

        public void SelectNextMenuItem(DirectionType direction)
        {
            var currentIndex = _menuItems.IndexOf(SelectedMenuItem);

            SelectedMenuItem.DeselectItem();
            SelectedMenuItem.SetText(SelectedMenuItem.Text);

            switch (direction)
            {
                case DirectionType.Down:

                    SelectNextItemUp(currentIndex);
                    break;

                case DirectionType.Up:
                    SelectNextItemDown(currentIndex);
                    break;

                default: return;
            }
            SelectedMenuItem.SelectItem();
            SelectedMenuItem.SpriteCreated += Add;
            SelectedMenuItem.SetText(SelectedMenuItem.Text);
        }

        private void SelectNextItemDown(int currentIndex)
        {
            if (currentIndex == 0)
            {
                currentIndex = _menuItems.Count - 1;
            }
            else
            {
                currentIndex--;
            }
            SelectedMenuItem = _menuItems[currentIndex];
        }

        private void SelectNextItemUp(int currentIndex)
        {
            if (currentIndex == _menuItems.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            SelectedMenuItem = _menuItems[currentIndex];
        }
    }
}
