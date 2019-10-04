using System;

namespace eWolfFences
{
    [Serializable]
    public class LightingOptions
    {
        /// <summary>
        /// Weather we are using Backed lighting for this object.
        /// </summary>
        public bool BakedLighting;

        /// <summary>
        /// This angle (in degrees) or greater between triangles will cause seam to be created
        /// </summary>
        public float HardAngle = 88;

        /// <summary>
        /// How much uv-islands will be padded
        /// </summary>
        public float PackMargin = 4;

        /// <summary>
        /// Maximum allowed area distortion (0..1)
        /// </summary>
        public float AngleError = 8;

        /// <summary>
        /// Maximum allowed angle distortion (0..1)
        /// </summary>
        public float AreaError = 15;
    }
}
