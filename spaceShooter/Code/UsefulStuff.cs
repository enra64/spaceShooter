using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code {
    static class UsefulStuff {
        /// <summary>
        /// normalises the source vector
        /// </summary>
        public static Vector2f normalise(Vector2f source) {
            float length = (float) Math.Sqrt(Math.Pow(source.X, 2) + Math.Pow(source.Y, 2));
            if(length != 0)
                return new Vector2f(source.X / length, source.Y / length);
            return source;
        }
    }
}
