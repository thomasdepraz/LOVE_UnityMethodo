using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public enum RayDirection
    {
        Left, 
        Right, 
        Up, 
        Down
    }
    /// <summary>
    /// R�utilisation d'un script d�ja fait sur un autre projet
    /// </summary>

    public class Raycaster2D : MonoBehaviour
    {
        public float rayDistance = 0.1f;
        [Range(1, 7)] public int accuracy = 3;
        public float skinWidth = 0.02f;

        public Transform self;
        public BoxCollider2D selfBox;

        List<Vector2> origins = new List<Vector2>();
        public LayerMask mask;
        public bool ThrowRays(RayDirection rayDir)
        {
            origins.Clear();

            //Get rays origins
            float offsetX = selfBox.size.x * 0.5f * self.lossyScale.x;
            float offsetY = selfBox.size.y * 0.5f * self.lossyScale.y;

            Vector2 startCorner = new Vector2(self.position.x + selfBox.offset.x, self.position.y + selfBox.offset.y);
            Vector2 endCorner = new Vector2(self.position.x + selfBox.offset.x, self.position.y + selfBox.offset.y); ;



            //Get Directions
            Vector2 rayDirection = Vector2.zero;
            switch (rayDir)
            {
                case RayDirection.Left:
                    startCorner.x -= offsetX;
                    startCorner.y -= offsetY;
                    endCorner.x -= offsetX;
                    endCorner.y += offsetY;

                    startCorner.x -= skinWidth;
                    endCorner.x -= skinWidth;

                    rayDirection = Vector2.right * -1;
                    break;
                case RayDirection.Right:
                    startCorner.x += offsetX;
                    startCorner.y += offsetY;
                    endCorner.x += offsetX;
                    endCorner.y -= offsetY;

                    startCorner.x += skinWidth;
                    endCorner.x += skinWidth;

                    rayDirection = Vector2.right;
                    break;
                case RayDirection.Up:
                    startCorner.x -= offsetX;
                    startCorner.y += offsetY;
                    endCorner.x += offsetX;
                    endCorner.y += offsetY;

                    startCorner.y += skinWidth;
                    endCorner.y += skinWidth;

                    rayDirection = Vector2.up;
                    break;
                case RayDirection.Down:
                    startCorner.x -= offsetX;
                    startCorner.y -= offsetY;
                    endCorner.x += offsetX;
                    endCorner.y -= offsetY;

                    startCorner.y -= skinWidth;
                    endCorner.y -= skinWidth;

                    rayDirection = Vector2.up * -1;
                    break;
                default:
                    break;
            }

            if (accuracy == 1)
            {
                origins.Add(Vector2.Lerp(startCorner, endCorner, 0.5f));
            }
            else
            {
                for (int i = 0; i < accuracy; i++)
                {
                    origins.Add(Vector2.Lerp(startCorner, endCorner, ((float)i / (float)(accuracy - 1))));
                }
            }

            //Throw rays
            for (int i = 0; i < origins.Count; i++)
            {
                if (Physics2D.Raycast(origins[i], rayDirection, rayDistance, mask))
                {
                    Debug.DrawRay(origins[i], rayDirection * rayDistance, Color.green);
                    return true;
                }
                else
                {
                    Debug.DrawRay(origins[i], rayDirection * rayDistance, Color.red);
                }
            }
            return false;
        }
    }

}
