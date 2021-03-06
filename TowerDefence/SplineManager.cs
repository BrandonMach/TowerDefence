using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using System.Collections.Generic;
using System.Diagnostics;

namespace TowerDefence
{
    class SplineManager
    {
        public static SimplePath simplePath;
        enum GameMode
        {
            Classic,
            Edit,
        }
        public static List<Vector2> pointList;

        public static void LoadSpline(GraphicsDevice graphicsDevice, GameWindow Window/*, bool classicGameMode*/)
        {
            pointList = new List<Vector2>();

            simplePath = new SimplePath(graphicsDevice);
            simplePath.Clean();
            simplePath.AddPoint(Vector2.Zero); // LÄgger till punkt nummer 0 på positionen 0,0
            simplePath.AddPoint(new Vector2(80, 200));//1
            simplePath.AddPoint(new Vector2(80, 250));//2
            simplePath.AddPoint(new Vector2(350, 200));//3
            simplePath.AddPoint(new Vector2(750, 100));//4
            simplePath.AddPoint(new Vector2(1300, 150));//5
            simplePath.AddPoint(new Vector2(1650, 200));//6
            simplePath.AddPoint(new Vector2(1450, 350));//7
            simplePath.AddPoint(new Vector2(1000, 450));//8
            simplePath.AddPoint(new Vector2(450, 390));
            simplePath.AddPoint(new Vector2(65, 520));
            simplePath.AddPoint(new Vector2(70, 550));
            simplePath.AddPoint(new Vector2(700, 650));
            simplePath.AddPoint(new Vector2(1600, 700));
            simplePath.AddPoint(new Vector2(1620, 600));
            simplePath.AddPoint(new Vector2(900, 800));
            simplePath.AddPoint(new Vector2(300, 800));
            simplePath.AddPoint(new Vector2(100, 900));
            simplePath.AddPoint(new Vector2(240, 960));
            simplePath.AddPoint(new Vector2(700, 900));
            simplePath.AddPoint(new Vector2(Window.ClientBounds.Width - 10, Window.ClientBounds.Height - 10));
     
            for (int i = 0; i < 21; i++)
            {
                pointList.Add(simplePath.GetPos(i));
            }
            Debug.WriteLine(simplePath.GetPos(simplePath.beginT));        
        }
    }
}
