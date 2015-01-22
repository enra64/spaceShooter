using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.Menu {
    class MenuItem {
        private Sprite mySprite;
        private bool mouseHover;

        public MenuItem(Vector2f _size, Vector2f _position, Texture _texture) {
            mySprite = new Sprite(_texture);
            mySprite.Position = _position;
            mySprite.Scale = new Vector2f((float)_size.X / (float)_texture.Size.X, (float)_size.Y / (float)_texture.Size.Y);
        }

        public bool Contains(float x, float y) {
            return mySprite.GetGlobalBounds().Contains(x, y);
        }
        public bool checkForHover() {
            mouseHover = mySprite.GetGlobalBounds().Contains(MouseHandler.CurrentPosition.X, MouseHandler.CurrentPosition.Y);
            return mouseHover;
        }

        public void draw() {
            if (mouseHover)
                mySprite.Color = new Color(255, 255, 255, 255);
            else
                mySprite.Color = new Color(255, 255, 255, 240);
            Controller.Window.Draw(mySprite);
        }
    }
}
