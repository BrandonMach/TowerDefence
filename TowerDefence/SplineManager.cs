using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Diagnostics;

namespace TowerDefence
{
    class SplineManager
    {
        public static SimplePath simplePath;

        public static void LoadSpline(GraphicsDevice graphicsDevice, GameWindow Window)
        {
           
            simplePath = new SimplePath(graphicsDevice);
            simplePath.Clean();
            

            simplePath.AddPoint( Vector2.Zero); // LÄgger till punkt nummer 0 på positionen 0,0
            simplePath.AddPoint( new Vector2(100,100));
            simplePath.AddPoint( new Vector2(100, 200));
            simplePath.AddPoint( new Vector2(200, 200));
            simplePath.AddPoint( new Vector2(700, 100));
            simplePath.AddPoint( new Vector2(1200, 150));
            simplePath.AddPoint( new Vector2(1500, 100));
            simplePath.AddPoint( new Vector2(1550, 300));
            simplePath.AddPoint( new Vector2(710, 300));
            simplePath.AddPoint( new Vector2(100, 550));
            simplePath.AddPoint( new Vector2(700, 600));
            simplePath.AddPoint( new Vector2(850, 550));
            simplePath.AddPoint( new Vector2(1700, 650));
            simplePath.AddPoint( new Vector2(900, 800));
            simplePath.AddPoint( new Vector2(100, 800));
            simplePath.AddPoint( new Vector2(170, 880));
            simplePath.AddPoint(new Vector2(700, 920));



            Debug.WriteLine(simplePath.GetPos(simplePath.beginT));



            simplePath.AddPoint(new Vector2(Window.ClientBounds.Width -10, Window.ClientBounds.Height - 10));
            
        }
    }
}
