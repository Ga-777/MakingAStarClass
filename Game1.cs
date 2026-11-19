using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace MakingAStarClass
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle window;


        Texture2D shipTexture;
        Rectangle shipRect;

        Texture2D starTexture;
        star star1;

        Vector2 starSpeed;

        List<star> stars;

        Random random;
        int ramdomNumber, randomSize;
        Color randomColor;

        float randomOpacity;
        

        int speed = -1;
        int speed2 = -2;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0,0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            
            shipRect = new Rectangle(30,150, 150, 40);
            random = new Random();
            randomColor = new Color();
            base.Initialize();
            stars = new List<star>();
            for (int i = 0; i < 170; i++)
            {
                randomSize = random.Next(2, 7);
                ramdomNumber = random.Next(1, 7);
                randomOpacity = (float)random.NextDouble();

                if (randomSize < 4)
                {
                    starSpeed = new Vector2(speed, 0);
                }
                else
                {
                    starSpeed = new Vector2(speed2, 0);
                }



                if (ramdomNumber == 1)
                {
                    randomColor = Color.White;
                }
                else if (ramdomNumber == 2)
                {
                    randomColor = Color.Yellow;
                }
                else if (ramdomNumber == 3)
                {
                    randomColor = Color.Aqua;
                }
                else if (ramdomNumber == 4)
                {
                    randomColor = Color.Red;
                }
                else if (ramdomNumber == 5)
                {
                    randomColor = Color.Orange;
                }
                else if (ramdomNumber == 6)
                {
                    randomColor = Color.Purple;
                }
               
                int X, Y;
                X = random.Next(window.Width);
                Y = random.Next(window.Height);
                Rectangle star = new Rectangle(X, Y, randomSize, randomSize);
               
                   
               
                stars.Add(new star(star, starSpeed, starTexture, randomColor * randomOpacity));
               
            }
            star1 = new star(new Rectangle(100,100,2,2),starSpeed, starTexture, randomColor * randomOpacity);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            starTexture = Content.Load<Texture2D>("image/circle");
            shipTexture = Content.Load<Texture2D>("image/UNSCship");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Keyboard.GetState();
            




            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                shipRect.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                shipRect.Y += 2;
            }

            
            

            foreach (star star in stars)
            {
                star.Update(window);
            }
            // TODO: Add your update logic here
            star1.Update(window);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            
            star1.Draw(_spriteBatch);
            foreach (var star in stars)
            {
                star.Draw(_spriteBatch);
            }
            _spriteBatch.Draw(shipTexture, shipRect, Color.White);



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
