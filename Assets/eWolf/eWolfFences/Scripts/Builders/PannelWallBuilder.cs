using System.Collections.Generic;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class PannelWallBuilder : WallBuilderBase
    {
        /// <summary>
        /// The height of the wall pannel
        /// </summary>
        public float PannelHeight;

        /// <summary>
        /// The height of the gate pannel
        /// </summary>
        public float PannelGateHeight;

        /// <summary>
        /// The length of the wall pannel
        /// </summary>
        public float PannelLength;

        /// <summary>
        /// The standard consturctor
        /// </summary>
        /// <param name="meshBuilder">The mesh builder</param>
        /// <param name="wallType">The type of the wall</param>
        public PannelWallBuilder(WallMeshBuilder yardBuilder, WallDetails.WallTypes wallType) : base(yardBuilder, wallType)
        {
        }

        /// <summary>
        /// Build the wall
        /// </summary>
        /// <param name="wd">The wall data</param>
        public override void BuildWall(WallData wd, GameObject gameObject)
        {
            Vector3 direction = wd.Direction;

            bool hasGateFeature = HasGateFeature(gameObject);
            if (hasGateFeature)
            {
                IFeature gate = GetFeature(gameObject);
                string featureStyle = gate.StyleType;
                float gatelength = gate.FeatureWidth;
                float per = gate.Percentage;

                Vector3 gateHalfDir = (gatelength / 2) * direction;
                Vector3 gateStart = Vector3.Lerp(wd.Start, wd.End, per);
                gateStart -= gateHalfDir;
                float firstLen = (gateStart - wd.Start).magnitude;
                
                Vector3 start = wd.Start;
                DrawPannelLength(start, direction, firstLen);

                start = gateStart;
                if (WallType == WallDetails.WallTypes.WoodenFence)
                {
                    if (featureStyle == PannelFeature.PannelFeatureStyles.Gate.ToString())
                        DrawPannelGateLength(start, direction, gatelength, PannelGateHeight);
                }
                if (WallType == WallDetails.WallTypes.MetalRailings)
                {
                    if (featureStyle == MetalRailingFeature.MetalRailingFeatureStyles.Gate.ToString())
                        DrawPannelGateLength(start, direction, gatelength, PannelGateHeight);
                }

                start += (gateHalfDir*2);
                firstLen = (gateStart - wd.End).magnitude;
                firstLen -= gatelength;
                DrawPannelLength(start, direction, firstLen);
            }
            else
            {
                float firstLen = (wd.End - wd.Start).magnitude;
                Vector3 start = wd.Start;
                DrawPannelLength(start, direction, firstLen);
            }
        }

        /// <summary>
        /// Get the feature for this wall, if any
        /// </summary>
        /// <param name="gameObject">The parenent object</param>
        /// <returns>The feature</returns>
        private IFeature GetFeature(GameObject gameObject)
        {
            IFeature feature = null;
            if (WallType == WallDetails.WallTypes.WoodenFence)
                feature = gameObject.GetComponent<PannelFeature>();

            if (WallType == WallDetails.WallTypes.MetalRailings)
                feature = gameObject.GetComponent<MetalRailingFeature>();

            return feature;
        }

        /// <summary>
        /// Draw a pannel Gate
        /// </summary>
        /// <param name="start">The start position</param>
        /// <param name="direction">The direction</param>
        /// <param name="fenceLength">The length</param>
        private void DrawPannelGateLength(Vector3 start, Vector3 direction, float fenceLength, float height)
        {
            List<PannelSet> pannels = new List<PannelSet>();

            PannelSet ps = new PannelSet() { PostPosition = start, Height = height, UVs = (UVSet)GetWoodenFenceGateUVs() };
            pannels.Add(ps);

            start += (direction * fenceLength);
            
            ps = new PannelSet() { PostPosition = start, Height = height, UVs = (UVSet)GetWoodenFenceGateUVs() };
            pannels.Add(ps);

            ProcessPairs(pannels);
        }

        /// <summary>
        /// Draw a pannel section
        /// </summary>
        /// <param name="start">The start position</param>
        /// <param name="direction">The direction</param>
        /// <param name="fenceLength">The length</param>
        private void DrawPannelLength(Vector3 start, Vector3 direction, float fenceLength)
        {
            // need to create an array of pannels
            // need to insert the gate at the correct point
            List<PannelSet> pannels = new List<PannelSet>();
            int walls = WallSectionCount(fenceLength);
            float endWallSize = EndWallLength(fenceLength);

            PannelSet ps = new PannelSet() { PostPosition = start, Height = PannelHeight, UVs = GetWoodenFenceUVs() };
            pannels.Add(ps);

            for (int i = 0; i < walls; i++)
            {
                start += (direction * PannelLength);
                ps = new PannelSet() { PostPosition = start, Height = PannelHeight, UVs = GetWoodenFenceUVs() };
                pannels.Add(ps);
            }

            start += (direction * endWallSize);
            float percent = endWallSize / PannelLength;

            UVSet WallUVs = (UVSet)GetWoodenFenceUVs();
            WallUVs.BL.x = WallUVs.BL.x * percent;
            WallUVs.BR.x = WallUVs.BR.x * percent;
            ps = new PannelSet() { PostPosition = start, Height = PannelHeight, UVs = WallUVs };
            pannels.Add(ps);

            ProcessPairs(pannels);
        }

        /// <summary>
        /// Draw the array of pannel sets
        /// </summary>
        /// <param name="pannelSets">The list of pannels to draw</param>
        private void ProcessPairs(List<PannelSet> pannelSets)
        {
            bool first = true;
            Vector3 start = new Vector3();
            foreach (PannelSet pannels in pannelSets)
            {
                Vector3 temp = start;
                start = pannels.PostPosition;
                if (!first)
                    AddPannel(temp, pannels.PostPosition, pannels.UVs, pannels.Height);
                else
                    first = false;
            }            
        }

        private void AddPannel(Vector3 start, Vector3 end, UVSet uvs, float pannelHeight)
        {
            BuilderHelper.AddWall(MeshBuilders, start, end, uvs, pannelHeight);
        }

        private bool HasGateFeature(GameObject gameObject)
        {
            IFeature feature = null;
            if (WallType == WallDetails.WallTypes.WoodenFence)
                feature = gameObject.GetComponent<PannelFeature>();

            if (WallType == WallDetails.WallTypes.MetalRailings)
                feature = gameObject.GetComponent<MetalRailingFeature>();

            return feature != null;
        }

        /// <summary>
        /// Gets the number of sections in this wall
        /// </summary>
        /// <param name="length">The length of the wall</param>
        /// <returns>The number of sections in the wall</returns>
        private int WallSectionCount(float length)
        {
            return (int)(length / PannelLength);
        }

        /// <summary>
        /// Gets the length of the last wall section
        /// </summary>
        /// <param name="length">The length of the wall</param>
        /// <returns>The lenth of the final section for the wall</returns>
        private float EndWallLength(float length)
        {
            return length % PannelLength;
        }
        
        /// <summary>
        /// THe wooden fence Uv set
        /// </summary>
        /// <returns>UV set for the woodend fence</returns>
        private UVSet GetWoodenFenceUVs()
        {
            UVSet u = new UVSet();
            u.TL = new Vector2(0.001f, 1);
            u.TR = new Vector2(0.001f, 0);

            u.BL = new Vector2(0.649f, 1);
            u.BR = new Vector2(0.649f, 0);

            return u;
        }

        /// <summary>
        /// THe wooden get fence Uv set
        /// </summary>
        /// <returns>UV set for the woodend fence</returns>
        private UVSet GetWoodenFenceGateUVs()
        {
            UVSet u = new UVSet();
            u.TL = new Vector2(0.65f, 1);
            u.TR = new Vector2(0.65f, 0);

            u.BL = new Vector2(0.995f, 1);
            u.BR = new Vector2(0.995f, 0);

            return u;
        }

        #region Helper Class
        private class PannelSet
        {
            public Vector3 PostPosition;
            public UVSet UVs;
            public float Height;
        }
        #endregion

    }
}
