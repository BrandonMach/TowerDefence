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
            simplePath.RemovePoint(11);
            simplePath.RemovePoint(12);
            simplePath.RemovePoint(13);
            simplePath.RemovePoint(14);
            simplePath.RemovePoint(15);

            simplePath.SetPos(0, Vector2.Zero); // LÄgger till punkt nummer 0 på positionen 0,0
            simplePath.SetPos(1, new Vector2(100,100)); 
            simplePath.SetPos(2, new Vector2(100,200));
            simplePath.SetPos(3, new Vector2(200,200));
            simplePath.SetPos(4, new Vector2(700,100));
            simplePath.SetPos(5, new Vector2(1200,150));
            simplePath.SetPos(6, new Vector2(1500,100));
            simplePath.SetPos(7, new Vector2(1550,300));
            simplePath.SetPos(8, new Vector2(710,300));
            simplePath.SetPos(9, new Vector2(100, 550));
            simplePath.SetPos(10, new Vector2(700, 600));
            simplePath.SetPos(11, new Vector2(850, 550));
            simplePath.SetPos(12, new Vector2(1700, 650));
            simplePath.SetPos(13, new Vector2(900, 800));
            simplePath.SetPos(14, new Vector2(100, 800));
            simplePath.SetPos(15, new Vector2(170, 880));
            simplePath.AddPoint(new Vector2(700, 920));
            
            
            
            
            

            Debug.WriteLine(simplePath.GetPos(simplePath.beginT));



            simplePath.AddPoint(new Vector2(Window.ClientBounds.Width -10, Window.ClientBounds.Height - 10));
            
        }
    }
}
