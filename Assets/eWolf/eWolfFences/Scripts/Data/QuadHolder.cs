using System;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class QuadHolder : ICloneable
    {
        public Vector3 LeftBack;
        public Vector3 LeftFront;
        public Vector3 RightBack;
        public Vector3 RightFront;

        public void AddHeight(Vector3 height)
        {
            LeftBack += height;
            LeftFront += height;
            RightBack += height;
            RightFront += height;
        }

        public object Clone()
        {
            QuadHolder qh = new QuadHolder();
            qh.LeftBack = LeftBack;
            qh.RightBack = RightBack;
            qh.LeftFront = LeftFront;
            qh.RightFront = RightFront;
            return qh;
        }

        public void Draw(WallMeshBuilder meshBuilders, UVSet uVSet)
        {
            meshBuilders.BuildQuad(LeftBack, RightBack, LeftFront, RightFront, uVSet);
        }

        public void DrawFilp(WallMeshBuilder meshBuilders, UVSet uVSet)
        {
            meshBuilders.BuildQuad(RightBack, LeftBack, RightFront, LeftFront, uVSet);
        }
    }
}
