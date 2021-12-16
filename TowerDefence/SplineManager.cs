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
            simplePath.AddPoint( new Vector2(80,200));//1
            simplePath.AddPoint( new Vector2(80, 250));//2
            simplePath.AddPoint( new Vector2(350, 200));//3
            simplePath.AddPoint( new Vector2(750, 100));//4
            simplePath.AddPoint( new Vector2(1300, 150));//5
            simplePath.AddPoint( new Vector2(1720, 170));//6
            simplePath.AddPoint( new Vector2(1650, 350));//7
            simplePath.AddPoint( new Vector2(1000, 450));//8
            simplePath.AddPoint(new Vector2(450, 390));
            simplePath.AddPoint( new Vector2(65, 520));
            simplePath.AddPoint( new Vector2(70, 550));
            simplePath.AddPoint( new Vector2(700, 650));
            simplePath.AddPoint( new Vector2(1800, 700));
            simplePath.AddPoint( new Vector2(900, 800));
            simplePath.AddPoint( new Vector2(60, 1000));
            simplePath.AddPoint( new Vector2(70, 1080));
            simplePath.AddPoint(new Vector2(700, 920));



            Debug.WriteLine(simplePath.GetPos(simplePath.beginT));



            simplePath.AddPoint(new Vector2(Window.ClientBounds.Width -10, Window.ClientBounds.Height - 10));
            
        }
    }
}
