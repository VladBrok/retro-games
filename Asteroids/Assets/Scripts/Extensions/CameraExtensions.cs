using UnityEngine;

namespace Asteroids.Extensions
{
    public static class CameraExtensions
    {
        public static Bounds GetViewBounds2D(this Camera camera)
        {
            if (!camera.orthographic)
            {
                Debug.LogErrorFormat("<color=red>Camera {0} is not orthographic.</color>", camera.name);
                return new Bounds();
            }

            float screenAspect = (float)Screen.width / (float)Screen.height;
            float height = camera.orthographicSize * 2;
            float width = screenAspect * height;
            Vector2 center = camera.transform.position;

            return new Bounds(center, new Vector2(width, height));
        }
    } 
}