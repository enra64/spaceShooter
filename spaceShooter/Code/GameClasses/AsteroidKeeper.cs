using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class AsteroidKeeper {
        private List<Asteroid> asteroidList = new List<Asteroid>();
        private int asteroidCount, idCounter = 0;
        private Random r = new Random();
        private Game reference;
        private static Vector2f addToMoveAwaySpeed = new Vector2f(4, 4);

        public AsteroidKeeper(int _asteroidCount, Game _reference) {
            asteroidCount = _asteroidCount;
            reference = _reference;
            for (int i = 0; i < _asteroidCount; i++) {
                float rotation = r.Next(4) * (1 - 2 * r.Next(2));
                //Vector2f position = new Vector2f(r.Next(8000), r.Next(8000));
                Vector2f position = new Vector2f(800 + i * 200, 2000);
                Vector2f direction = new Vector2f((float)r.Next(10) / 10f, (float)r.Next(10) / 10f);
                asteroidList.Add(new Asteroid(position, direction, rotation, Globals.asteroidTextures[r.Next(0)], idCounter));
                idCounter++;
            }
        }

        public void update(){
            foreach (Asteroid a in asteroidList)
                a.update();
            //collision
            foreach (Asteroid a in asteroidList){
                //with bullets
                for (int i = reference.myShip.bulletList.Count - 1; i >= 0; i--){
                    Bullet b = reference.myShip.bulletList[i];
                    if (a.GlobalBounding.Intersects(b.GlobalBounding)) {
                        a.Health -= 20;
                        reference.myShip.bulletList.RemoveAt(i);
                    }
                }
                //with myShip
                if (a.GlobalBounding.Intersects(reference.myShip.GlobalBounding)) { 
                    reference.myShip.Health -= 20;
                    //move asteroid away from ship
                    float xNew = reference.myShip.SpeedVector.X * ((float)r.Next(6, 12) / 5f);
                    float yNew = reference.myShip.SpeedVector.Y * ((float)r.Next(6, 12) / 5f);
                    a.Direction = new Vector2f(xNew, yNew);
                }
                bool noCollision = true;
                //with other asteroids
                for (int i = asteroidList.Count - 1; i >= 0; i--) {
                    Asteroid b = asteroidList[i];
                    //hopefully checks reference identity
                    if (b != a) {
                        if (asteroidList[i].GlobalBounding.Intersects(a.GlobalBounding)) {
                            //stop swapping collision avoid
                            if (!a.CollisionHandled && !b.CollisionHandled) {
                                Vector2f transferDirection = a.Direction;
                                a.Direction = b.Direction;
                                b.Direction = transferDirection;
                                a.CollisionHandled = true;
                                b.CollisionHandled = true;
                            }
                            noCollision = false;
                        }
                    }
                }
                if (noCollision)
                    a.CollisionHandled = false;
            }
            
            for (int i = asteroidList.Count - 1; i >= 0; i--)
                if (asteroidList[i].Health <= 0)
                    asteroidList.RemoveAt(i);
        }

        public void draw(){
            foreach (Asteroid a in asteroidList)
                a.draw();
        }
    }
}
