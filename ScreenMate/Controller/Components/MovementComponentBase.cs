using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace ScreenMate.Controller.Components
{
    //will be abstract
    public class MovementComponentBase : ComponentBase
    {
        protected Point destination = new Point(0, 0);
        protected float stoppingDistance = 20f;
        protected int speed = 15;

        public override void RunComponent()
        {
            var distance = GetDistanceFromMate(destination);
            Debug.WriteLine(distance);
            Debug.WriteLine(mate.Position);
            if (mate.IsProcessor || mate.IsIdle || mate.IsRam || distance < stoppingDistance)
                return;
            mate.Position = GetNewPosition();
        }

        private Point GetNewPosition()
        {
            var dirNormalized = Vector2.Normalize(new Vector2(destination.X - mate.Position.X, destination.Y - mate.Position.Y));
            return new Point(Convert.ToInt32(mate.Position.X + dirNormalized.X * speed), Convert.ToInt32(mate.Position.Y + dirNormalized.Y * speed));
        }
        protected float GetDistanceFromMate(Point p)
        {
            return (float)Math.Sqrt((mate.Position.X - p.X) * (mate.Position.X - p.X) + (mate.Position.Y - p.Y) * (mate.Position.Y - p.Y));
        }
    }
}
