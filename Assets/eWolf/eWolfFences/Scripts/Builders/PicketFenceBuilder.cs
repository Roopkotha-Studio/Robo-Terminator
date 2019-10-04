using System.Collections.Generic;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class PicketFenceBuilder : WallBuilderBase
    {
        /// <summary>
        /// The height of the wall pannel
        /// </summary>
        public float PannelHeight;

        /// <summary>
        /// The length of the wall pannel
        /// </summary>
        public float SectionLength;

        /// <summary>
        /// The standard consturctor
        /// </summary>
        /// <param name="meshBuilder">The mesh builder</param>
        /// <param name="wallType">The type of the wall</param>
        public PicketFenceBuilder(WallMeshBuilder yardBuilder, WallDetails.WallTypes wallType) : base(yardBuilder, wallType)
        {
        }

        /// <summary>
        /// Build the wall
        /// </summary>
        /// <param name="wd">The wall data</param>
        /// <param name="gameObject">The game object</param>
        public override void BuildWall(WallData wd, GameObject gameObject)
        {
            PartBuilder[] parts = CreatePartsList(wd, gameObject);

            foreach (PartBuilder part in parts)
            {
                part.Builder(part);
            }
        }

        private void AddPicketFenceRow(PartBuilder part)
        {
            Vector3 start = part.Start;
            Vector3 direction = part.Direction;

            float length = part.Length;

            int walls = WallSectionCount(length);
            float endWallSize = EndWallLength(length);
            
            for (int i = 0; i < walls; i++)
            {
                PannelSet(start, direction, SectionLength);
            
                start += direction * SectionLength;
            }
            
            PannelSet(start, direction, endWallSize);
        }

        private void PannelSet(Vector3 start, Vector3 direction, float sectionLength)
        {
            float uvx = (SectionLength /100) * sectionLength;
            UVSet uvs = GetUVFull(uvx);

            BuilderHelper.AddWall(MeshBuilders, start, start + (direction * sectionLength), uvs, PannelHeight);
        }

        private UVSet GetUVFull(float x)
        {
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(x, 1);
            u.TR = new Vector2(x, 0);

            u.BL = new Vector2(0, 1);
            u.BR = new Vector2(0, 0);

            return u;
        }

        private PartBuilder[] CreatePartsList(WallData wd, GameObject gameObject)
        {
            List<PartBuilder> partList = new List<PartBuilder>();
            Vector3 start = wd.Start;
            Vector3 direction = wd.Direction;

            IFeature feature = GetFeature(gameObject);
            if (feature != null)
            {
                string featureStyle = feature.StyleType;
                float featureLength = feature.FeatureWidth;
                float per = feature.Percentage;

                Vector3 gateHalfDir = (featureLength / 2) * direction;
                Vector3 gateStart = Vector3.Lerp(wd.Start, wd.End, per);
                gateStart -= gateHalfDir;
                float firstLen = (gateStart - wd.Start).magnitude;

                PartBuilder firstPart = new PartBuilder();
                firstPart.StartCap = true;
                firstPart.EndCap = true;
                firstPart.WallData = wd;
                firstPart.Builder = AddPicketFenceRow;
                firstPart.Start = start;
                firstPart.Direction = direction;
                firstPart.Length = firstLen;

                start = gateStart + (gateHalfDir * 2);
                firstLen = (gateStart - wd.End).magnitude;
                firstLen -= featureLength;

                partList.Add(firstPart);
                // add any features here

                PartBuilder sectionPart = new PartBuilder();
                sectionPart.StartCap = true;
                sectionPart.EndCap = true;
                sectionPart.WallData = wd;
                sectionPart.Builder = AddPicketFenceRow;
                sectionPart.Start = start;
                sectionPart.Direction = direction;
                sectionPart.Length = firstLen;

                partList.Add(sectionPart);
            }
            else
            {
                PartBuilder part = new PartBuilder();
                part.WallData = wd;
                part.Builder = AddPicketFenceRow;
                part.Start = start;
                part.Direction = direction;
                part.Length = wd.Length;
                partList.Add(part);
            }
            return partList.ToArray();
        }

        /// <summary>
        /// Gets the feature for the wall
        /// </summary>
        /// <param name="gameObject">The main wall object</param>
        /// <returns>The feature for this wall</returns>
        private IFeature GetFeature(GameObject gameObject)
        {
            IFeature feature = null;
            //if (WallType == WallDetails.WallTypes.PicketFence)
                //feature = gameObject.GetComponent<PicketFenceFeature>();

            return feature;
        }
        
        /// <summary>
        /// The number of wall sections
        /// </summary>
        /// <param name="length">The length of thsi part</param>
        /// <returns>The number of wall section in the part</returns>
        private int WallSectionCount(float length)
        {
            return (int)(length / SectionLength);
        }

        /// <summary>
        /// The length of the last section
        /// </summary>
        /// <param name="length">The length of this part</param>
        /// <returns>The length of the last section</returns>
        private float EndWallLength(float length)
        {
            return length % SectionLength;
        }
    }
}
