﻿using SFML.Graphics;
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

        public enum moveDirection {
            none,
            left,
            down,
            right,
            up
        };

        public static List<Texture> startMenuTextures = new List<Texture>();
        public static List<Texture> shipTextures = new List<Texture>();
        public static List<Texture> bulletTextures = new List<Texture>();
        public static List<Texture> asteroidTextures = new List<Texture>();
        public static Texture starTexture;
    }
}
