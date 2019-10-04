using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace eWolfFences
{
    public class DebugDrawHelper
    {
        /// <summary>
        /// Draw a circle button
        /// </summary>
        /// <param name="position">The center position</param>
        /// <param name="size">The size</param>
        /// <returns>Whether the button was pressed or not</returns>
        public static bool DrawCircleButton(Vector3 position, float size)
        {
            Quaternion q = Quaternion.Euler(90, 0, 0);
            Handles.color = Color.cyan;
            return Handles.Button(position, q , size, size, Handles.CircleHandleCap);
        }

                /// <summary>
        /// Draw a 3D cross
        /// </summary>
        /// <param name="midPoint">The center position of the cross</param>
        /// <param name="size">The size of the crosss</param>
        public static void DrawCross(Vector3 midPoint, float size)
        {
            size = size / 2;
            Vector3 offSet1 = midPoint;
            offSet1.y -= size;
            Vector3 offSet2 = midPoint;
            offSet2.y += size;
            Handles.DrawLine(offSet1, offSet2);

            offSet1 = midPoint;
            offSet2 = midPoint;
            offSet1.x -= size;
            offSet2.x += size;
            Handles.DrawLine(offSet1, offSet2);

            offSet1 = midPoint;
            offSet2 = midPoint;
            offSet1.z -= size;
            offSet2.z += size;
            Handles.DrawLine(offSet1, offSet2);
        }
    }
}
