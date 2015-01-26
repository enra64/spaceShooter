using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    abstract class ProtoGameObject {
        public Vector2f Size {get; set;}
        public Sprite Sprite { get; set; }

        public ProtoGameObject(Vector2f _startPosition, Vector2f _size, Texture _texture) {
            Sprite = new Sprite(_texture);
            Sprite.Scale = new Vector2f((float)_size.X / (float)_texture.Size.X, (float)_size.Y / (float)_texture.Size.Y);
            Size = _size;
            Sprite.Position = _startPosition;
        }

        public virtual void update(){}

        public virtual void draw(){
            Controller.Window.Draw(Sprite);
        }

        public Vector2f Center {
            get {
                return new Vector2f(Sprite.Position.X + Size.X / 2f, Sprite.Position.Y + Size.Y / 2f); 
            }
        }
    }
}
