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
        private Background background;
        public Game () {
            myShip = new Ship(new Vector2f(2000, 2000), new Vector2f(100, 100), Globals.shipTextures[0]);
            background = new Background();
        }

        public override void draw() {
            background.draw();
            myShip.draw();
        }

        public override void kill() {
        }

        internal override void update(){
            myShip.update();
            background.update();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                Controller.View.Zoom(1.02f);
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                Controller.View.Zoom(.98f);
            
            Controller.Window.SetView(Controller.View);
        }
    }
}
