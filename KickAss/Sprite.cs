using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace KickAss
{
    class Sprite
    {
        public Vector2 position;        
        private Texture2D mSpriteTexture;
        public float RotationAngle { get; set; }
        public Rectangle rectSize;
        public float rectScale = 1.0f;


        //LOAD SPRITE FROM CONTENT
        public void LoadContent(ContentManager cm, string assetName)
        {
            mSpriteTexture = cm.Load<Texture2D>(assetName);
            rectSize = new Rectangle(0, 0, (int)(mSpriteTexture.Width * rectScale), (int)(mSpriteTexture.Height * rectScale));
            Console.WriteLine(mSpriteTexture.Name + " - Size: " + mSpriteTexture.Width);        
        }



        //DRAW SPRITE
        public void Draw(SpriteBatch sprBtch)
        {
            sprBtch.Draw(mSpriteTexture, position, Color.White);            
        }

        public void DrawWithRect(SpriteBatch sprBtch)
        {
            sprBtch.Draw(mSpriteTexture, position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, rectScale, SpriteEffects.None, 0);
        }

        public void DrawRotating(SpriteBatch sprBtch)
        {            
            Vector2 location = new Vector2(470, 470);
            Vector2 origin = new Vector2(mSpriteTexture.Width / 2, mSpriteTexture.Height / 2);
            Rectangle sourceRectangle = new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height);
            sprBtch.Draw(mSpriteTexture, location, sourceRectangle, Color.White, RotationAngle, origin, 1.0f, SpriteEffects.None, 1);
                       
        } 
    }
}
