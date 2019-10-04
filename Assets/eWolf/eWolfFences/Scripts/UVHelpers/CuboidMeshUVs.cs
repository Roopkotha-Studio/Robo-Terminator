using UnityEngine;

namespace eWolfFences
{
    public class CuboidMeshUVs
    {
        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="size">The size of the uvs</param>
        public CuboidMeshUVs(float size)
        {
            _width = size;
            _pannelOffSet = UnityEngine.Random.Range(0, 100);
            _swapEnds = UnityEngine.Random.Range(0, 100) > 50;

            _postEndA = UnityEngine.Random.Range(0, 4);
            _postEndB = UnityEngine.Random.Range(0, 4);
        }

        #region Public Methods
        public UVSet EndCapA()
        {
            return GetCap(_postEndA);
        }

        public UVSet EndCapB()
        {
            return GetCap(_postEndB);
        }

        public UVSet PannelA()
        {
            return GetindexPannel((0 + _pannelOffSet) % 4);
        }

        public UVSet PannelB()
        {
            return GetindexPannel((1 + _pannelOffSet) % 4);
        }

        public UVSet PannelC()
        {
            return GetindexPannel((2 + _pannelOffSet) % 4);
        }

        public UVSet PannelD()
        {
            return GetindexPannel((3 + _pannelOffSet) % 4);
        }
        #endregion

        #region Private Methods
        private UVSet GetCap(int index)
        {
            UVSet u = new UVSet();
            u.TL = new Vector2(0.49f, 0.49f);
            u.TR = new Vector2(0, 0.49f);
            u.BL = new Vector2(0.49f, 0);
            u.BR = new Vector2(0, 0);

            return u.Rotate(index);
        }

        private UVSet GetindexPannel(int index)
        {
            float left = _swapEnds ? 1 : 0;
            float right = _swapEnds ? 0 : 1;

            UVSet u = new UVSet();
            u.TL = new Vector2(left, 1 - (_width * index));
            u.TR = new Vector2(right, 1 - (_width * index));
            u.BL = new Vector2(left, u.TL.y - _width);
            u.BR = new Vector2(right, u.TR.y - _width);
            return u;
        }
        #endregion

        #region Private Fields
        private float _width;
        private Vector2 _topLeft;
        private Vector2 _bottomRight;
        private int _pannelOffSet;
        private bool _swapEnds;
        private int _postEndA;
        private int _postEndB;
        #endregion
    }
}
