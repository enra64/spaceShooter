using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Bullet : ProtoGameObject{
        public int Faction { get; set; }
        public int Type { get; set; }

        private float speed, orientation;

        public Bullet(Vector2f _pos, float direction, int type, int faction, Texture _tex) : base(_pos, _tex.Size, _tex){
            Type = type;
            Faction = faction;
            Sprite.Origin = new Vector2f(_tex.Size.X, _tex.Size.Y);
            Sprite.Rotation = direction - 90;
            orientation = direction;
            speed = 40;
        }

        public override void update(){
            //calculate thrust angel
            Vector2f thrustVector = new Vector2f();
            thrustVector.Y = (float)Math.Sin(Math.PI * orientation / 180) * speed;
            thrustVector.X = (float)Math.Cos(Math.PI * orientation / 180) * speed;
            Sprite.Position += thrustVector;
        }
    }
}
