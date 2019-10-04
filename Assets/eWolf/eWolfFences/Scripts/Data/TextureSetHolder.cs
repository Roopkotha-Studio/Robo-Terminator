using System.Collections.Generic;
using UnityEngine;

namespace eWolfFences
{
    /// <summary>
    /// The texture set holder
    /// </summary>
    public class TextureSetHolder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        private TextureSetHolder()
        {
            _uVList.Clear();
            _uVList.Add(TextUV.GroundFloorWall, new UVSet(0, 0));
            _uVList.Add(TextUV.SecondFloorWall, new UVSet(1, 0));

            _uVList.Add(TextUV.Door_A, new UVSet(0, 1));
            _uVList.Add(TextUV.Door_B, new UVSet(1, 1));
            _uVList.Add(TextUV.Door_C, new UVSet(2, 1));

            _uVList.Add(TextUV.GroundFloorWindow, new UVSet(0, 2));
            _uVList.Add(TextUV.SecondFloorWindow, new UVSet(1, 2));
            
            _uVList.Add(TextUV.WallEndSlope, new UVSet(3, 2));
            _uVList.Add(TextUV.WallEndSlopePoint, new UVSet(
                new Vector2(0.751f, 1 - 0.50f),
                new Vector2(0.878f, 1 - (0.75f - 0.128f)),
                new Vector2(0.751f, 1 - (0.75f - 0.128f)),
                new Vector2(1f, 1 - 0.75f)
            ));

            _uVList.Add(TextUV.RoofTiles, new UVSet(3, 1));
            _uVList.Add(TextUV.RoofTilesTop, new UVSet(3, 0));
            _uVList.Add(TextUV.FlatRoofMiddle, new UVSet(3, 2));

            _uVList.Add(TextUV.RoofTilesTopPoint, new UVSet(
                new Vector2(0.75f, 1 - 0.128f),
                new Vector2(1f, 1 - 0.128f),
                new Vector2(0.75f, 1 - 0),
                new Vector2(1f, 1 - 0)
            ));

            _uVList.Add(TextUV.GarageDoors, new UVSet(0, 3));

            _uVList.Add(TextUV.GarageRoof, new UVSet(1, 3));

            _uVList.Add(TextUV.WallEndSlopeMiddle, new UVSet(3, 3));
        }

        /// <summary>
        /// Get the instance of this class
        /// </summary>
        public static TextureSetHolder Instance
        {
            get
            {
                if (_textureSetHolder == null)
                    _textureSetHolder = new TextureSetHolder();
                return _textureSetHolder;
            }
        }

        /// <summary>
        /// Get the UV data for the ID
        /// </summary>
        /// <param name="uv">The Id of the set</param>
        /// <returns>The UV data set</returns>
        internal UVSet GetUvData(TextUV uv)
        {
           return _uVList[uv];
        }

        #region Private Fields
        /// <summary>
        /// The instance of the texture holder
        /// </summary>
        private static TextureSetHolder _textureSetHolder;

        /// <summary>
        /// The list of UV sets
        /// </summary>
        private Dictionary<TextUV, UVSet> _uVList = new Dictionary<TextUV, UVSet>(); 
        #endregion
    }
}
