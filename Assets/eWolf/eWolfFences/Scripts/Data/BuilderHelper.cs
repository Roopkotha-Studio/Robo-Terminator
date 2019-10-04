using UnityEngine;

namespace eWolfFences
{
    public static class BuilderHelper
    {
        public static void AddFloor(WallMeshBuilder _meshBuilder, Vector3 start, Vector3 dir, Vector3 dir2, UVSet uv, float length)
        {
            Vector3 a = start;
            Vector3 b = a + (dir * length);
            Vector3 c = start;
            c -= dir2;
            Vector3 d = c + (dir * length);

            _meshBuilder.BuildQuad(a, b, c, d, uv);
        }

        /// <summary>
        /// Add a wall section
        /// </summary>
        /// <param name="meshBuilders">The wall mesh builder</param>
        /// <param name="start">The start position</param>
        /// <param name="dir">the direction of the wall</param>
        /// <param name="uv">The UV set to use</param>
        /// <param name="height">The height of the wall</param>
        /// <param name="length">the length of the section</param>
        /// <param name="tapurDistance">tapur the top of the wall</param>
        public static void AddWall(WallMeshBuilder _meshBuilder, Vector3 start, Vector3 dir, UVSet uv, float height, float length, Vector3 tapurDistance)
        {
            Vector3 a = start;
            Vector3 b = a + (dir * length);
            Vector3 c = start;
            c.y += height;
            c -= tapurDistance;
            Vector3 d = c + (dir * length);
            
            _meshBuilder.BuildQuad(a, b, c, d, uv);
        }

        public static void AddWallEnd(WallMeshBuilder _meshBuilder, Vector3 start, Vector3 dir, UVSet uv, float height, float length, Vector3 tapurDistance)
        {
            Vector3 a = start;
            Vector3 b = a + (dir * length);
            Vector3 c = start;
            c.y += height;
            Vector3 d = c + (dir * length);
            c -= tapurDistance;
            d += tapurDistance;

            _meshBuilder.BuildQuad(a, b, c, d, uv);
        }

        public static void AddWall(WallMeshBuilder _meshBuilder, Vector3 start, Vector3 end, UVSet uv, float height)
        {
            Vector3 a = start;
            Vector3 b = end;
            Vector3 c = a;
            Vector3 d = b;
            c.y += height;
            d.y += height;

            _meshBuilder.BuildQuad(a, b, c, d, uv);
        }


        /// <summary>
        /// Add a wall section
        /// </summary>
        /// <param name="meshBuilders">The wall mesh builder</param>
        /// <param name="start">The start position</param>
        /// <param name="dir">the direction of the wall</param>
        /// <param name="uv">The UV set to use</param>
        /// <param name="height">The height of the wall</param>
        public static void AddWallFlipped(WallMeshBuilder _meshBuilder, Vector3 start, Vector3 dir, UVSet uv, float height, float length)
        {
            Vector3 a = start;
            Vector3 b = a + (dir * length);
            Vector3 c = start;
            c.y += height;
            Vector3 d = c + (dir * length);
            
            _meshBuilder.BuildQuadFlipped(a, b, c, d, uv);
        }
    }
}
