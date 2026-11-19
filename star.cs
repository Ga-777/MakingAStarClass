using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Xna.Framework.MathHelper;
using Random = System.Random;

namespace MakingAStarClass
{
    public class star
    {
        private Rectangle _location;
        private Vector2 _speed;
        private Texture2D _texture;
        private Color _color;
       

        public star(Rectangle location, Vector2 speed, Texture2D texture, Color color )
        {
            _location = location;
            _speed = speed;
            _texture = texture;
            _color = color;
        }

        public void Update(Rectangle window)
        {
            
            _location.Offset(_speed);
            if (_location.Right < 0)
            {
                _location.X = window.Width;
            }
            

        }

       
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, _color);
        }
    }
   
}
