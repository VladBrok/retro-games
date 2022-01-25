using UnityEngine;
using Asteroids.Extensions;

namespace Asteroids
{
    public class Wraparound<T> : WraparoundBase<T> where T : IWrapable
    {
        public Wraparound(T self, Bounds viewArea)
            : base(self, viewArea)
        {
        }

        protected override void ApplyWraparound()
        {
            float newX = ViewArea.ClosestPoint(Self.Center).x;
            float newY = ViewArea.ClosestPoint(Self.Center).y;
            Vector2 selfExtents = new Vector2(
                Self.Extents.x.Percentage(80),
                Self.Extents.y.Percentage(80));

            if (Self.Center.x < ViewArea.min.x)
            {
                newX = ViewArea.max.x + selfExtents.x;
            }
            else if (Self.Center.y < ViewArea.min.y)
            {
                newY = ViewArea.max.y + selfExtents.y;
            }
            else if (Self.Center.x > ViewArea.max.x)
            {
                newX = ViewArea.min.x - selfExtents.y;
            }
            else if (Self.Center.y > ViewArea.max.y)
            {
                newY = ViewArea.min.y - selfExtents.y;
            }

            Self.Center = new Vector2(newX, newY);
        }
    }
}
