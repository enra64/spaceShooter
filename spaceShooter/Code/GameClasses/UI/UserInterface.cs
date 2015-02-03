using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.GameClasses.UI;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class UserInterface {
        private View uiView;
        private Game reference;
        private Vector2f size;
        private Container mainContainer;

        public UserInterface(Vector2f _size, Game _ref) {
            size = _size;
            reference = _ref;
            construct();
            uiView = new View(new FloatRect(0, 0, _size.X, _size.Y));
            uiView.Viewport = new FloatRect(0, .75f, 1, .25f);
        }

        private void construct() {
            mainContainer = new Container(size, new Vector2f(0,0), reference, "main_container", false);
            mainContainer.Add(new TestView(new Vector2f(), new Vector2f(), reference, "test_view"));
            mainContainer.Add(new Healthbar(new Vector2f(), new Vector2f(), reference, "health_view"));
        }

        public void update() {
            mainContainer.update();
        }

        public void draw() {
            Controller.Window.SetView(uiView);
            mainContainer.draw(uiView);
        }
    }
}
