using UnityEngine;
using System;

namespace eWolfFences
{
    [Serializable]
    public class WallData
    {
        [SerializeField]
        [HideInInspector]
        public Vector3 Start = new Vector3(0, 0, 10);

        [SerializeField]
        [HideInInspector]
        public Vector3 End = new Vector3(0, 0, -10);

        /// <summary>
        /// Get the direction of the wall
        /// </summary>
        public Vector3 Direction
        {
            get
            {
                Vector3 d = End - Start;
                d.Normalize();
                return d;
            }
        }

        /// <summary>
        /// Gets the lenth of the wall
        /// </summary>
        public float Length
        {
            get
            {
                return (End - Start).magnitude;
            }
           
        }

        /// <summary>
        /// The number of wall sections
        /// </summary>
        /// <param name="wallWidth">Thw width of the wall section</param>
        /// <returns>The number of wall sections</returns>
        public int WallSectionCount(float wallWidth)
        {
            Vector3 d = End - Start;
            float mag = d.magnitude;
            return (int)(mag / wallWidth);
        }

        /// <summary>
        /// The length of the last section
        /// </summary>
        /// <param name="wallWidth">The width of the wall sections</param>
        /// <returns>The length of the last wall sections</returns>
        public float EndWallLength(float wallWidth)
        {
            Vector3 d = End - Start;
            float mag = d.magnitude;
            return mag % wallWidth;
        }
    }
}
