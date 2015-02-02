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
        private ProtoUIObject mainContainer;

        public UserInterface(Vector2f _size, Game _ref) {
            size = _size;
            reference = _ref;
            mainContainer = new Container(_size, _ref, "main_container");
        }

        public void update() {
            mainContainer.update();
        }

        public void draw() {
            Controller.Window.SetView(uiView);
            mainContainer.draw();
        }
    }
}
