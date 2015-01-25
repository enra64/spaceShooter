using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Background {
        private List<Sprite> starList = new List<Sprite>();
        private RenderTexture background;
        private Random r = new Random();
        private Sprite bgSprite;
        private Vector2f movementDelta = new Vector2f(), prevViewCenter = new Vector2f();
        private Vector2u textureSize;

        public Background() {
            background = new RenderTexture(Controller.Window.Size.X, Controller.Window.Size.Y);
            textureSize = Globals.starTexture.Size;
            for (int i = 0; i < 1000; i++) {
                starList.Add(new Sprite(Globals.starTexture));
                starList[i].Scale = new Vector2f(r.Next(0, 10) / 5, r.Next(0, 10) / 5);
                starList[i].Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
                starList[i].Position = new Vector2f(r.Next((int)Controller.Window.Size.X - (int)(textureSize.X * starList[i].Scale.X)), 
                    r.Next((int)Controller.Window.Size.Y - (int)(textureSize.Y * starList[i].Scale.Y)));
            }
        }

        public void update(){
            //check for view movement
            if (prevViewCenter.X != Controller.View.Center.X || prevViewCenter.Y != Controller.View.Center.Y)
                movementDelta = Controller.View.Center - prevViewCenter;
            else
                movementDelta = new Vector2f(0, 0);
            prevViewCenter = Controller.View.Center;

            //draw new stars
            if (movementDelta.X != 0 || movementDelta.Y != 0) {
                foreach (Sprite s in starList) {
                    //s.Scale = new Vector2f(r.Next(0, 10) / 5, r.Next(0, 10) / 5);
                    //s.Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));

                    //draw star to new position
                    //moving right
                    /*
                    if (s.Position.X < movementDelta.X && movementDelta.X > 0) {
                        s.Position = new Vector2f(r.Next((int)Controller.Window.Size.X - (int)movementDelta.X - (int)(textureSize.X * s.Scale.X), (int)Controller.Window.Size.X - (int)(textureSize.X * s.Scale.X)),
                            r.Next((int)Controller.Window.Size.Y - (int)(textureSize.Y * s.Scale.Y)));
                    }
                    //moving left
                    if (s.Position.X > (Controller.Window.Size.X + movementDelta.X) && movementDelta.X < 0) {
                        s.Position = new Vector2f(r.Next((int)Controller.Window.Size.X - (int)movementDelta.X, (int)Controller.Window.Size.X - (int)(textureSize.X * s.Scale.X)),
                            r.Next((int)Controller.Window.Size.Y - (int)(textureSize.Y * s.Scale.Y)));
                    }
                    
                    if (s.Position.X < movementDelta.X) {
                        s.Position = new Vector2f(r.Next((int)Controller.Window.Size.X - (int)movementDelta.X, (int)Controller.Window.Size.X - (int)(textureSize.X * s.Scale.X)),
                            r.Next((int)Controller.Window.Size.Y - (int)(textureSize.Y * s.Scale.Y)));
                    }
                    if (s.Position.X < movementDelta.X) {
                        s.Position = new Vector2f(r.Next((int)Controller.Window.Size.X - (int)movementDelta.X, (int)Controller.Window.Size.X - (int)(textureSize.X * s.Scale.X)),
                            r.Next((int)Controller.Window.Size.Y - (int)(textureSize.Y * s.Scale.Y)));
                    }*/
                }
            }

            background.Clear();
            foreach (Sprite s in starList)
                background.Draw(s);
            background.Display();
            bgSprite = new Sprite(background.Texture);
            bgSprite.Position = new Vector2f(0, 0);
        }

        //idea: draw onto a rendertexture, draw that behind everything
        //if we are moving right, draw new stars on the left and delete the old ones
        public void draw(){
            Controller.Window.SetView(Controller.Window.DefaultView);
            if(bgSprite != null)
                Controller.Window.Draw(bgSprite);
        }
    }
}
