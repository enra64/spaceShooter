using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code {
    static class Globals {
        public enum EGameStates {
            fuckedUp,
            startscreen, 
            game,
            endscreen,
            credits
        }

        public enum EStateSelection {
            none,
            main,
            sub
        }

        public static List<Texture> startMenuTextures = new List<Texture>();
    }
}
