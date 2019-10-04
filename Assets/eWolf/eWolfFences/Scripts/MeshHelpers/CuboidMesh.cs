using eWolfFences.Builders;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences
{
    public class CuboidMesh : IApplyShapes
    {
        public CuboidMesh(QuadHolder bottom, float height)
        {
            _bottomSection = bottom;
            _topSection = (QuadHolder)bottom.Clone();
            _topSection.AddHeight(new Vector3(0, 1, 0) * height);
        }

        public CuboidMesh(QuadHolder bottom, Vector3 height)
        {
            _bottomSection = bottom;
            _topSection = (QuadHolder)bottom.Clone();
            _topSection.AddHeight(height);
        }

        public CuboidMesh(QuadHolder bottom, QuadHolder top)
        {
            _bottomSection = bottom;
            _topSection = top;
        }

        public bool BottomCap
        {
            set
            {
                _bottomCap = value;
            }
        }
        public bool TopCap
        {
            set
            {
                _topCap = value;
            }
        }

        public void ApplyAgePost(float age)
        {
            age *= 100;
            _topSection.LeftBack += GetRandom(age);
            _topSection.LeftFront += GetRandom(age);

            _topSection.RightBack += GetRandom(age);
            _topSection.RightFront += GetRandom(age);
        }

        private Vector3 GetRandom(float age)
        {
            return new Vector3(
                    UnityEngine.Random.Range(-age, age) / 100,
                    UnityEngine.Random.Range(-age, age) / 100,
                    UnityEngine.Random.Range(-age, age) / 100
                );
        }

        public void CreateApplyShape(WallMeshBuilder meshBuilders, CuboidMeshUVs cuboidMeshUVs)
        {
            if (_bottomCap)
                _bottomSection.DrawFilp(meshBuilders, cuboidMeshUVs.EndCapB());

            meshBuilders.BuildQuad(
                _bottomSection.LeftBack, _bottomSection.RightBack,
                _topSection.LeftBack, _topSection.RightBack, cuboidMeshUVs.PannelA());

            meshBuilders.BuildQuad(
                _bottomSection.LeftFront, _bottomSection.LeftBack,
                _topSection.LeftFront, _topSection.LeftBack, cuboidMeshUVs.PannelB());

            meshBuilders.BuildQuad(
                _bottomSection.RightFront, _bottomSection.LeftFront,
                _topSection.RightFront, _topSection.LeftFront, cuboidMeshUVs.PannelC());

            meshBuilders.BuildQuad(
                _bottomSection.RightBack, _bottomSection.RightFront,
                _topSection.RightBack, _topSection.RightFront, cuboidMeshUVs.PannelD());

            if (_topCap)
                _topSection.Draw(meshBuilders, cuboidMeshUVs.EndCapA());
        }

        #region Private Fields
        private QuadHolder _bottomSection;
        private QuadHolder _topSection;
        private bool _bottomCap = false;
        private bool _topCap = true;
        #endregion
    }
}

