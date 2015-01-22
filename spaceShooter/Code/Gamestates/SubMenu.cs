using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Global;
using spaceShooter.Code.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.Gamestates {
    class SubMenu : ProtoGameState {
        private List<MenuItem> menuItemList = new List<MenuItem>();

        public override void draw() {
        }

        internal override void update() {
        }

        public override void kill() {
        }

        public SubMenu (Vector2f _position, Vector2f _size, List<Texture> buttonList) : base() {
            //basic idea: create multiple menuitems dynamically in the given rectangle.
            float yMargin = 5;
            FloatRect availableSpace = new FloatRect(_position.X + 5, _position.Y + 5, _size.X - 10, _size.Y - 10);
            Vector2f menuItemSize = new Vector2f((availableSpace.Height - ((buttonList.Count) - 1 * yMargin)) / buttonList.Count, availableSpace.Width);
            for(int i = 0; i < buttonList.Count; i++)
                menuItemList.Add(new MenuItem(menuItemSize, new Vector2f(availableSpace.Left, availableSpace.Top + (i * (menuItemSize.Y + yMargin))), buttonList[i]));
        }
    }
}
