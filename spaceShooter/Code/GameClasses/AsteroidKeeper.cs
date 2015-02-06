﻿using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class AsteroidKeeper {
        private List<List<Asteroid>> outerList = new List<List<Asteroid>>();
        private int asteroidCount, idCounter = 0;
        private Random r = new Random();
        private Game reference;
        private static Vector2f addToMoveAwaySpeed = new Vector2f(4, 4);

        public AsteroidKeeper(int _asteroidCount, Game _reference) {
            asteroidCount = _asteroidCount;
            reference = _reference;
            generateAsteroids(_asteroidCount);
        }

        private void generateAsteroids(int astCount) {
            for (int mainCounter = 0; mainCounter < 9; mainCounter++) {
                outerList.Add(new List<Asteroid>());
                FloatRect bgFloat = reference.background.bgRects[mainCounter];
                for (int subCounter = 0; subCounter < astCount; subCounter++){
                    float rotation = r.Next(4) * (1 - 2 * r.Next(2));
                    Vector2f position = new Vector2f(r.Next((int)bgFloat.Width) + bgFloat.Left, r.Next((int)bgFloat.Height) + bgFloat.Top);
                    Vector2f direction = new Vector2f((float)r.Next(10) / 10f, (float)r.Next(10) / 10f);
                    outerList[mainCounter].Add(new Asteroid(position, direction, rotation, Globals.asteroidTextures[r.Next(0)], idCounter));
                    idCounter++;
                }
            }
        }

        public void update(){
            //only update inner floatrect
            List<Asteroid> centerReference = outerList[4];
            //updating
            foreach (Asteroid a in centerReference)
                a.update();

            collisionHandling(centerReference);

            //asteroid dying
            for (int i = centerReference.Count - 1; i >= 0; i--)
                if (centerReference[i].Health <= 0)
                    centerReference.RemoveAt(i);
        }

        internal void moveAsteroids(Globals.moveDirection where) {
            //for every outer list, move all asteroids to new random positions, but
            //keep the positions of the mid asteroids
            for (int mainCounter = 0; mainCounter < 9; mainCounter++) {
                FloatRect bgFloat = reference.background.bgRects[mainCounter];
                List<Asteroid> innerReference = outerList[mainCounter];

                for (int i = 0; i < innerReference.Count; i++) {
                    Vector2f nextPosition = new Vector2f(r.Next((int)bgFloat.Width) + bgFloat.Left, r.Next((int)bgFloat.Height) + bgFloat.Top);
                    innerReference[i].Sprite.Position = nextPosition;
                }
            }
            //because i dont want to interrupt the asteroid look, we move only a specific set of tiles on each move-command.
            
        }

        private void collisionHandling(List<Asteroid> centerReference) {
            //collision
            foreach (Asteroid a in centerReference) {
                //with bullets
                for (int i = reference.myShip.bulletList.Count - 1; i >= 0; i--) {
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
                for (int i = centerReference.Count - 1; i >= 0; i--) {
                    Asteroid b = centerReference[i];
                    //hopefully checks reference identity
                    if (b != a) {
                        if (centerReference[i].GlobalBounding.Intersects(a.GlobalBounding)) {
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
        }

        public void draw(){
            /*
            foreach (List<Asteroid> l in outerList)
                foreach (Asteroid a in l)
                    a.draw();
            */
        }
    }
}
