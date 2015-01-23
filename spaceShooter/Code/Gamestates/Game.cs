using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.GameClasses;
using spaceShooter.Code.Global;
using spaceShooter.Code.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.Gamestates {
    class Game : ProtoGameState {
        public Ship myShip { get; set; }
        public Game () {
            myShip = new Ship(new Vector2f(100, 100), new Vector2f(100, 100), Globals.shipTextures[0]);
        }

        public override void draw() {
            myShip.draw();
        }

        public override void kill() {
        }

        internal override void update() {
            myShip.update();
            Controller.View.Center = myShip.Center;
            Controller.Window.SetView(Controller.View);
        }
    }
}
