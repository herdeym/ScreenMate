using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace ScreenMate.Controller.Components
{
    [Component("Disappear")]
    public class DisappearComponent : MovementComponentBase
    {
        private List<(Point position, float distance)> newTargets;
        private bool outOfBounds=false;

        public override void ResumeComponent()
        {
            var bounds = Screen.PrimaryScreen.Bounds;
            newTargets = new List<(Point, float)>()
            {
                GetTargetWithDistance(-100, mate.Position.Y),
                GetTargetWithDistance(mate.Position.X, -100),
                GetTargetWithDistance(bounds.Width+100, mate.Position.Y),
                GetTargetWithDistance(mate.Position.X, bounds.Height+100),
            };
            var closestTarget = newTargets.OrderBy(t => t.distance).First();
            destination = closestTarget.position;
            outOfBounds = true;
            base.ResumeComponent();
        }

        public override void RunComponent()
        {
            if (GetDistanceFromMate(destination) < stoppingDistance*2 && outOfBounds)
            {
                var random = new Random();
                var index = random.Next(3) + 1;
                mate.Position=newTargets.OrderBy(t => t.distance).ElementAt(index).position;
                var bounds = Screen.PrimaryScreen.Bounds;
                destination = new Point(200+random.Next(bounds.Width-400), 100 + random.Next(bounds.Height - 200));
                outOfBounds = false;
            }
            Debug.WriteLine($"Disappear");
            base.RunComponent();
        }

        private (Point, float) GetTargetWithDistance(int X, int Y)
        {
            var point = new Point(X, Y);
            return (point, GetDistanceFromMate(point));
        }
    }
}
