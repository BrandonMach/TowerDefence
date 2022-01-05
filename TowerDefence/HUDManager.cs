using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    class HUDManager
    {
        private Rectangle avastIcon;
        private Rectangle nordVPNIcon;
       
        public HUDManager()
        {
         

            avastIcon = new Rectangle(1750, 120, 100,100);
            nordVPNIcon = new Rectangle(1750, 250, 100,100);

        }
        public void Update()
        {

            if (avastIcon.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
            {

                Game1.currentTowerSelected = Game1.TowerSelect.Avast;

            }
            else if (nordVPNIcon.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick())
            {

                Game1.currentTowerSelected = Game1.TowerSelect.NordVPN;

            }



        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(SpriteManager.AvastTex, avastIcon, Color.White);
            _spriteBatch.Draw(SpriteManager.NordVPNTex, nordVPNIcon, Color.White);
        }
    }
}
