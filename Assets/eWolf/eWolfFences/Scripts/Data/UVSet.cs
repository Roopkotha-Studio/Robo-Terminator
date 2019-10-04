using System;
using UnityEngine;

namespace eWolfFences
{
    /// <summary>
    /// The uv Set
    /// </summary>
    public class UVSet : ICloneable
    {
        #region Public Fields
        /// <summary>
        /// The top left position
        /// </summary>
        public Vector2 TL;

        /// <summary>
        /// The top right position
        /// </summary>
        public Vector2 TR;

        /// <summary>
        /// The Bottom left position
        /// </summary>
        public Vector2 BL;

        /// <summary>
        /// The bottom right postion
        /// </summary>
        public Vector2 BR;

        public void Flipvertical()
        {
            Vector3 store = TL;
            TL.y = BL.y;
            BL.y = store.y;

            store = TR;
            TR.y = BR.y;
            BR.y = store.y;
        }

        public void FlipHorizontal()
        {
            Vector3 store = TL;
            TL.x = TR.x;
            TR.x = store.x;

            store = BL;
            BL.x = BR.x;
            BR.x = store.x;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The Standard constructor
        /// </summary>
        /// <param name="topLeft">The top left UV</param>
        /// <param name="topRight">The top right UV</param>
        /// <param name="botLeft">The bottom left uv</param>
        /// <param name="botRight">The bottom right uv</param>
        public UVSet(Vector2 topLeft, Vector2 topRight, Vector2 botLeft, Vector3 botRight)
        {
            TL = topLeft;
            TR = topRight;
            BL = botLeft;
            BR = botRight;
        }

        /// <summary>
        /// The Standard constructor
        /// </summary>
        public UVSet()
        {
        }

        /// <summary>
        /// The Standard constructor
        /// </summary>
        /// <param name="x">The X position</param>
        /// <param name="y">The Y position</param>
        public UVSet(int x, int y)
        {
            const float unit = 0.250f;
            const float off = 0.004f;
            const float offSet = 0.248f;

            TL = new Vector2((x * unit) + off, 1 - (y * unit) - off);
            TR = new Vector2((x * unit) + off, 1 - ((y * unit) + offSet));
            BL = new Vector2((x * unit) + offSet, 1 - (y * unit) - off);
            BR = new Vector2((x * unit) + offSet, 1 - ((y * unit) + offSet));
        }

        /// <summary>
        /// Rotation the UV set
        /// </summary>
        /// <param name="rotation">The number of roatations</param>
        /// <returns>The rotated UV set</returns>
        public UVSet Rotate(int rotation)
        {
            UVSet rots = (UVSet)Clone();
            for (int i = 0; i < rotation; i++)
            {
                UVSet old = (UVSet)rots.Clone();
                rots.TR = old.TL;
                rots.BR = old.TR;
                rots.BL = old.BR;
                rots.TL = old.BL;
            }
            return rots;
        }

        /// <summary>
        /// The cloner
        /// </summary>
        /// <returns>A clone of the object</returns>
        public object Clone()
        {
            return new UVSet(TL, TR, BL, BR);
        }
        #endregion
    }

    /// <summary>
    /// Vector holder
    /// </summary>
    public class VectorSet
    {
        public Vector3 A;
        public Vector3 B;
        public Vector3 C;
        public Vector3 D;
    }
}
