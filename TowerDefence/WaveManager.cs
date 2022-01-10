using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;
namespace TowerDefence
{
    class WaveManager
    {
        Enemys enemys;
        double startSpawn = 0;
        double spawnDelay = 0.7;
        public double startSpawnDuration = 0;
        public void Update(GameTime gamTime)
        {     
            SpawnWave(Game1.waveNum, gamTime);         
        }
        public void SpawnWave(int waveNummber, GameTime gameTime)
        {
            startSpawnDuration += gameTime.ElapsedGameTime.TotalSeconds;           
            if(startSpawnDuration < waveNummber/2 +1)
            {
                startSpawn += gameTime.ElapsedGameTime.TotalSeconds;
                if (startSpawn >= spawnDelay)
                {
                    startSpawn = 0;
                    enemys = new Enemys(SpriteManager.TrojanTex, Vector2.Zero, new Rectangle(0, 0, SpriteManager.TrojanTex.Width, SpriteManager.TrojanTex.Height));
                    Game1.enemyList.Add(enemys);
                    Debug.WriteLine(Game1.enemyList.Count);
                }              
            }         
            else
            {
                Game1.spawnWaves = false;
            } 
        }     
    }
}
