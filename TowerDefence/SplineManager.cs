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

            simplePath.SetPos(1, Vector2.Zero); // LÄgger till punkt nummer 1 på positionen 0,0
            simplePath.AddPoint(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
        }
    }
}
