using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace KickAss
{
    public enum Direction
    {
        up,
        left,
        down,
        right,
    }

    class AnimatedSprite
    {
        //CLASS FIELDS AND PROPERTIES
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public Vector2 position;
        

        //CONSTRUCTOR
        public AnimatedSprite(Texture2D tex, int rows, int columns)
        {
            Texture = tex;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Columns;          //normal sprite atlas matrix use Rows * Columns
        }


        //METHODS
        public void Update()
        {//this method steps through the animation sprites
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Direction playerDir)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            //int row = (int)((float)currentFrame / (float)Columns);            //use this to get the correct row in a atlas where a single animation spans multiple rows
            int row = (int)playerDir;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
                        
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);                       
        }         
    }
}
