using System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.Global {
    public abstract class ProtoGameState{
        public bool Running { get; private set; }

        public ProtoGameState() {

        }

        public void mainUpdate() {
            Controller.Window.Clear(Color.Black);
            Controller.Window.DispatchEvents();
            update();
            draw();
            Controller.Window.Display();
        }

        public void pause() {
            Running = false;
        }

        public abstract void kill();

        public abstract void draw();
        internal abstract void update();
    }
}
