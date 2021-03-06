﻿using SFML.Graphics;
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
        public Background background;
        private AsteroidKeeper asteroids;
        private UserInterface ui;
        
        public Game () {
            myShip = new Ship(new Vector2f(2000, 2000), new Vector2f(Globals.shipTextures[0].Size.X, Globals.shipTextures[0].Size.Y), Globals.shipTextures[0]);
            background = new Background(this);
            asteroids = new AsteroidKeeper(80, this);
            ui = new UserInterface(new Vector2f(Controller.Window.Size.X, Controller.Window.Size.Y / 4), this);
        }

        public override void draw() {
            background.draw();
            asteroids.draw();
            myShip.draw();
            ui.draw();
        }

        public override void kill() {
            
        }

        internal override void update(){
            myShip.update();
            asteroids.update();
            background.update();
            ui.update();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                Controller.View.Zoom(1.02f);
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                Controller.View.Zoom(.98f);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                background.moveBackground(Globals.moveDirection.left);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                background.moveBackground(Globals.moveDirection.right);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                background.moveBackground(Globals.moveDirection.up);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                background.moveBackground(Globals.moveDirection.down);
            
            Controller.Window.SetView(Controller.View);
        }

        //add calls to handle moving through the background tiles
        internal void bgMoveCallBack(Globals.moveDirection where) {
            //keep first, sets currentcenterrect
            background.moveBackground(where);
            asteroids.moveAsteroids(where);
        }
    }
}
