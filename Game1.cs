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

        Texture2D shipWindowTexture;

		List<star> stars;

        Random random;
        int ramdomNumber, randomSize;
        Color randomColor;

        float randomOpacity;
        

        bool windowOpen = false;

		MouseState MouseState;

		float speed = -1;
        float speed2 = -2;

		int speedBoost = 0;

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
                    starSpeed = new Vector2(speed , 0);
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
			shipWindowTexture = Content.Load<Texture2D>("image/spaceshipwindow");
			// TODO: use this.Content to load your game content here
		}

        protected override void Update(GameTime gameTime)
        {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			MouseState = Mouse.GetState();

			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				speed -= 0.0005f;
				speed2 -= 0.001f;
				if (speedBoost < 15)
                {
					speedBoost++;
				}
					
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
                if (speed < -0.5f)
                {
					speed += 0.0005f;
				}
                
				if (speed2 < -1.0f) 
                {
					speed2 += 0.001f;
				}
				if (speedBoost > 0)
                {
					speedBoost--;
				}
					
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				if (shipRect.Y <= 0)
				{
					shipRect.Y -= 0;
				}
				else
				{
					shipRect.Y -= 2;
				}
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				if (shipRect.Y >= 550)
				{
					shipRect.Y += 0;
				}
				else
				{
					shipRect.Y += 2;
				}
			}

            if (MouseState.LeftButton == ButtonState.Pressed && MouseState.Position.X >= shipRect.X && MouseState.Position.X <= shipRect.X + shipRect.Width && MouseState.Position.Y >= shipRect.Y && MouseState.Position.Y <= shipRect.Y + shipRect.Height)
			{
				windowOpen = true;
			}
			else if (MouseState.RightButton == ButtonState.Pressed)
			{
				windowOpen = false;
			}

			int steps = 1 + speedBoost;
			for (int step = 0; step < steps; step++)
			{
				foreach (star star in stars)
				{
					star.Update(window);
				}
				star1.Update(window);
			}

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

            if (windowOpen == true)
			{
				_spriteBatch.Draw(shipWindowTexture, window, Color.White);
			}
			else
			{
				_spriteBatch.Draw(shipTexture, shipRect, Color.White);
			}
			



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
