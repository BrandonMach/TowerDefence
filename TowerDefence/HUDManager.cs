using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private Vector2 moneyPos;
        private Vector2 avastTextPos;
        private Vector2 nordVPNTextPos;
        public Rectangle levelUpButtonRect;
        private SpriteFont money_Font;

        private string avastText;
        private string nordVpnText;

        private Vector2 waveTextPos;

        public  bool nordTowerClicked;
        public  bool avastTowerClicked;


        public enum TowerInfo
        {
            Avast,
            Nord, 
            None,
        }


        public TowerInfo currentInfo = TowerInfo.None;
        
       
        public HUDManager(ContentManager Content)
        {
         

            avastIcon = new Rectangle(1750, 140, 100,100);
            avastTextPos = new Vector2(1810, 270);
            nordVPNIcon = new Rectangle(1750, 290, 100,100);
            nordVPNTextPos = new Vector2(1810, 430);
            moneyPos = new Vector2(1750, 10);
            money_Font = Content.Load<SpriteFont>("money_Font");
            waveTextPos = new Vector2(1550, 10);
           

            avastText = Game1.avastPlaceCost.ToString();
            nordVpnText = Game1.nordVPNCost.ToString();

            nordTowerClicked = false;
            avastTowerClicked = false;

        }
        public void Update()
        {
            levelUpButtonRect = new Rectangle(1710, 800, SpriteManager.LevelUpButtonTex.Width, SpriteManager.LevelUpButtonTex.Height);


            if (avastIcon.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick() && Game1.money >= Game1.avastPlaceCost)
            {
                Game1.currentTowerSelected = Game1.TowerSelect.Avast;
            }
            else if (nordVPNIcon.Contains(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y) && KeyMouseReader.LeftClick() && Game1.money >= Game1.nordVPNCost)
            {
                Game1.currentTowerSelected = Game1.TowerSelect.NordVPN;
            }

            //switch (currentInfo)
            //{
            //    case TowerInfo.Avast:
            //        break;
            //    case TowerInfo.Nord:
            //        break;
            //    case TowerInfo.None:
            //        break;
            //    default:
            //        break;
            //}



        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            string avastTowerText = "Avast Tower";
            string nordTowerText = "Nord VPN Tower";

            _spriteBatch.DrawString(money_Font, "" + Game1.waveNum, waveTextPos, Color.Black);
            _spriteBatch.DrawString(money_Font, "$" + Game1.money, moneyPos, Color.Gold);

            switch (currentInfo)
            {
                case TowerInfo.Avast:


                    foreach (Towers tower in Game1.towersList)
                    {
                        if (tower is AvastTower && tower.infoClicked)
                        {
                            _spriteBatch.DrawString(money_Font, avastTowerText + "\nTower Level: " + tower.level, new Vector2(1860, 100), Color.White, 0f, money_Font.MeasureString(avastTowerText), 0.7f, SpriteEffects.None, 1f);

                            if (tower.level < tower.maxLevel)
                            {
                                _spriteBatch.Draw(SpriteManager.LevelUpButtonTex, levelUpButtonRect, Color.White);
                            }
                          

                        }

                    }

                    break;
                case TowerInfo.Nord:
                    foreach (Towers tower in Game1.towersList )
                    {
                        if (tower is NordVPNTower && tower.infoClicked)
                        {
                            _spriteBatch.DrawString(money_Font, nordTowerText +"\n Tower Level: "+ tower.level, new Vector2(1896, 100), Color.White, 0f, money_Font.MeasureString(nordTowerText), 0.65f, SpriteEffects.None, 1f);

                            if (tower.level < tower.maxLevel)
                            {
                                _spriteBatch.Draw(SpriteManager.LevelUpButtonTex, levelUpButtonRect, Color.White);
                            }

                        }

                    }

                    break;
                case TowerInfo.None:

                    _spriteBatch.Draw(SpriteManager.AvastTex, avastIcon, Color.White);
                    _spriteBatch.Draw(SpriteManager.NordVPNTex, nordVPNIcon, Color.White);
                    if (Game1.money <= Game1.avastPlaceCost)
                    {
                        _spriteBatch.Draw(SpriteManager.AvastTex, avastIcon, Color.Red);
                    }
                    if (Game1.money <= Game1.nordVPNCost)
                    {
                        _spriteBatch.Draw(SpriteManager.NordVPNTex, nordVPNIcon, Color.Red);
                    }

                    _spriteBatch.DrawString(money_Font, "$" + Game1.avastPlaceCost, avastTextPos, Color.Gold, 0f, money_Font.MeasureString(avastText), 0.7f, SpriteEffects.None, 1f);
                    _spriteBatch.DrawString(money_Font, "$" + Game1.nordVPNCost, nordVPNTextPos, Color.Gold, 0f, money_Font.MeasureString(nordVpnText), 0.7f, SpriteEffects.None, 1f);

                    break;
              
            }


        }
    }
}
