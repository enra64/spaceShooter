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
    class Startscreen : ProtoGameState {
        private List<MenuItem> menuItemList = new List<MenuItem>();

        public override void draw() {
            foreach (MenuItem m in menuItemList)
                m.draw();
        }

        internal override void update() {
            foreach(MenuItem m in menuItemList)
                m.checkForHover();
            if (MouseHandler.UnhandledClick) {
                MouseHandler.UnhandledClick = false;
                switch (clickedIndex()) {
                    case 0:
                        Controller.loadState(Globals.EStateSelection.main, Globals.EGameStates.game);
                        break;
                    case 1:
                        Controller.Window.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private int clickedIndex() {
            for (int i = 0; i < menuItemList.Count; i++)
                if (menuItemList[i].checkForHover())
                    return i;
            return -1;
        }

        public override void kill() {
        }

        public Startscreen () : base() {
            //basic idea: create multiple menuitems dynamically in the given rectangle.
            Vector2f _position = new Vector2f(100, 50);
            Vector2f _size = new Vector2f(Controller.Window.Size.X - _position.X * 2, Controller.Window.Size.Y - _position.Y * 2);
            List<Texture> buttonList = Globals.startMenuTextures;
            float yMargin = 5;
            FloatRect availableSpace = new FloatRect(_position.X + 5, _position.Y + 5, _size.X - 10, _size.Y - 10);
            Vector2f menuItemSize = new Vector2f(availableSpace.Width, (availableSpace.Height - ((buttonList.Count) - 1 * yMargin)) / buttonList.Count);
            for(int i = 0; i < buttonList.Count; i++)
                menuItemList.Add(new MenuItem(menuItemSize, new Vector2f(availableSpace.Left, availableSpace.Top + (i * (menuItemSize.Y + yMargin))), buttonList[i]));
        }
    }
}
