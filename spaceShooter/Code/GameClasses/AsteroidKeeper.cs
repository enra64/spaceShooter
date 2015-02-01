using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class AsteroidKeeper {
        private List<Asteroid> asteroidList = new List<Asteroid>();
        private int asteroidCount;
        private Random r = new Random();

        public AsteroidKeeper(int _asteroidCount) {
            asteroidCount = _asteroidCount;
            for (int i = 0; i < _asteroidCount; i++) {
                float rotation = r.Next(4) * (1 - 2 * r.Next(2));
                Vector2f position = new Vector2f(r.Next(8000), r.Next(8000));
                Vector2f direction = new Vector2f((float)r.Next(10) / 10f, (float)r.Next(10) / 10f);

                asteroidList.Add(new Asteroid(position, direction, rotation, Globals.asteroidTextures[r.Next(0)]));
            }
        }

        public void update(){
            foreach (Asteroid a in asteroidList)
                a.update();
        }

        public void draw(){
            foreach (Asteroid a in asteroidList)
                a.draw();
        }
    }
}
