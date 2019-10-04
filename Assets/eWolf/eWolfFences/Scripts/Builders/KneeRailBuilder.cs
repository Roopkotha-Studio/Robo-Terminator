using System.Collections.Generic;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class KneeRailBuilder : WallBuilderBase
    {
        /// <summary>
        /// The height of the wall pannel
        /// </summary>
        public float Height;

        /// <summary>
        /// The length of the wall pannel
        /// </summary>
        public float SectionLength;

        /// <summary>
        /// The thickness of the post
        /// </summary>
        public float Thickness;

        /// <summary>
        /// The standard consturctor
        /// </summary>
        /// <param name="meshBuilder">The mesh builder</param>
        /// <param name="wallType">The type of the wall</param>
        public KneeRailBuilder(WallMeshBuilder yardBuilder, WallDetails.WallTypes wallType) : base(yardBuilder, wallType)
        {
        }

        /// <summary>
        /// Build the wall
        /// </summary>
        /// <param name="wd">The wall data</param>
        public override void BuildWall(WallData wd, GameObject gameObject)
        {
            PartBuilder[] parts = CreatePartsList(wd, gameObject);

            foreach (PartBuilder part in parts)
            {
                part.Builder(part);
            }
        }

        private PartBuilder[] CreatePartsList(WallData wd, GameObject gameObject)
        {
            List<PartBuilder> partList = new List<PartBuilder>();

            Vector3 start = wd.Start;
            start += Vector3.up * Height;
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
                firstPart.Builder = AddHorizontalSection;
                firstPart.Start = start;
                firstPart.Direction = direction;
                firstPart.Length = firstLen;

                start = gateStart + (gateHalfDir * 2);
                start += Vector3.up * Height;
                firstLen = (gateStart - wd.End).magnitude;
                firstLen -= featureLength;

                PartBuilder sectionPart = new PartBuilder();
                sectionPart.StartCap = true;
                sectionPart.EndCap = true;
                sectionPart.WallData = wd;
                sectionPart.Builder = AddHorizontalSection;
                sectionPart.Start = start;
                sectionPart.Direction = direction;
                sectionPart.Length = firstLen;
                
                partList.Add(firstPart);
                partList.Add(sectionPart);
            }
            else
            {
                PartBuilder part = new PartBuilder();
                part.StartCap = true;
                part.EndCap = true;
                part.WallData = wd;
                part.Builder = AddHorizontalSection;
                part.Start = start;
                part.Direction = direction;
                part.Length = wd.Length;
                partList.Add(part);
            }

            return partList.ToArray();
        }

        private void AddHorizontalSection(PartBuilder part)
        {
            Vector3 start = part.Start;
            int walls = WallSectionCount(part.Length);
            float endWallSize = EndWallLength(part.Length);

            bool two = false;
            if (walls > 0)
            {
                walls -= 1;
                endWallSize += SectionLength;
                endWallSize /= 2;
                two = true;
            }

            bool cap = part.StartCap;
            for (int i = 0; i < walls; i++)
            {
                AddHorizontal(start, part.Direction, SectionLength, cap, false);
                cap = false;
                start += part.Direction * SectionLength;
            }

            AddHorizontal(start, part.Direction, endWallSize, cap, false);
            start += part.Direction * endWallSize;
            AddHorizontal(start, part.Direction, 0, false, two == false);

            if (two)
            {
                endWallSize -= Thickness * 2.15f;
                AddHorizontal(start, part.Direction, endWallSize, false, false);
                start += part.Direction * endWallSize;
                AddHorizontal(start, part.Direction, 0, false, part.EndCap);
            }
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

        private void AddHorizontal(Vector3 start, Vector3 direction, float length, bool capStart, bool capEnd)
        {
            float uvLength = 1 / SectionLength;
            uvLength = uvLength * length;

            Vector3 rightDir = Quaternion.AngleAxis(-90, Vector3.up) * direction;
            Vector3 leftDir = Quaternion.AngleAxis(90, Vector3.up) * direction;
            Vector3 rightDownDir = Vector3.down + rightDir;
            Vector3 leftDownDir = Vector3.down + leftDir;

            float sectionLength = length - (Thickness * 2.15f);
            start += direction * (Thickness * 2.15f);
            // create one post.
            Vector3 topStart = start;
            Vector3 topEnd = start + (direction * sectionLength);

            Vector3 rightStart = start + (rightDownDir * Thickness);
            Vector3 rightEnd = rightStart + (direction * sectionLength);

            Vector3 bottomStart = start - (Vector3.up * Thickness * 2);
            Vector3 bottomEnd = bottomStart + (direction * sectionLength);

            Vector3 leftStart = start + (leftDownDir * Thickness);
            Vector3 leftEnd = leftStart + (direction * sectionLength);

            if (uvLength != 0)
            {
                MeshBuilders.BuildQuad(rightStart, bottomStart, rightEnd, bottomEnd, GetUVSide(0, uvLength));
                MeshBuilders.BuildQuad(bottomStart, leftStart, bottomEnd, leftEnd, GetUVSide(3, uvLength));
                MeshBuilders.BuildQuad(topStart, rightStart, topEnd, rightEnd, GetUVSide(1, uvLength));
                MeshBuilders.BuildQuad(leftStart, topStart, leftEnd, topEnd, GetUVSide(2, uvLength));
            }

            if (capEnd)
            {
                MeshBuilders.BuildQuad(leftStart, topStart, bottomStart, rightStart, GetUVEnd());
            }

            float offSetStart = Thickness * 2.15f;
            leftEnd = leftStart;
            leftStart -= (direction * offSetStart);
            rightEnd = rightStart;
            rightStart -= (direction * offSetStart);
            topEnd = topStart;
            topStart -= (direction * offSetStart);
            bottomStart -= (direction * offSetStart);
            UVSet uvs = GetUVSidesVerticalTops(0);
            uvs.Flipvertical();

            MeshBuilders.BuildQuad(topStart, rightStart, topEnd, rightEnd, uvs);
            MeshBuilders.BuildQuad(leftStart, topStart, leftEnd, topEnd, GetUVSidesVerticalTops(0));

            if (capStart)
                MeshBuilders.BuildQuad(topStart, leftStart, rightStart, bottomStart, GetUVEnd());

            PostVerticalPost(rightStart, leftStart, direction);
        }

        private void PostVerticalPost(Vector3 rightStart, Vector3 leftStart, Vector3 direction)
        {
            float backThingness = Thickness * 2.15f;
            Vector3 rightDir = Quaternion.AngleAxis(-90, Vector3.up) * direction;

            Vector3 rightBackStart = rightStart + (direction * backThingness);
            Vector3 leftBackStart = leftStart + (direction * backThingness);

            Vector3 rightEnd = rightStart - (Vector3.up * (Height - Thickness));
            Vector3 leftEnd = leftStart - (Vector3.up * (Height - Thickness));

            Vector3 rightBackEnd = rightBackStart - (Vector3.up * (Height - Thickness));
            Vector3 leftBackEnd = leftBackStart - (Vector3.up * (Height - Thickness));

            // right
            MeshBuilders.BuildQuad(rightBackStart, rightStart, rightBackEnd, rightEnd, GetUVSidesVertical(0));

            Vector3 rightStartMid = rightStart - (rightDir * Thickness) - (Vector3.up * Thickness);
            Vector3 rightEndMid = rightEnd - (rightDir * Thickness);

            MeshBuilders.BuildQuad(rightStart, rightStartMid, rightEnd, rightEndMid, GetUVSidesVertical(1));
            MeshBuilders.BuildQuad(rightStartMid, leftStart, rightEndMid, leftEnd, GetUVSidesVertical(1));

            // left
            MeshBuilders.BuildQuad(leftStart, leftBackStart, leftEnd, leftBackEnd, GetUVSidesVertical(2));

            Vector3 leftStartMid = leftBackStart + (rightDir * Thickness) - (Vector3.up * Thickness);
            Vector3 leftEndMid = leftBackEnd + (rightDir * Thickness);

            MeshBuilders.BuildQuad(leftBackStart, leftStartMid, leftBackEnd, leftEndMid, GetUVSidesVertical(3));
            MeshBuilders.BuildQuad(leftStartMid, rightBackStart, leftEndMid, rightBackEnd, GetUVSidesVertical(3));
        }

        private IFeature GetFeature(GameObject gameObject)
        {
            IFeature feature = null;
            if (WallType == WallDetails.WallTypes.KneeRailFence)
                feature = gameObject.GetComponent<KneeRailFeature>();

            return feature;
        }
        
        /// <summary>
        /// Get the post sides UVs
        /// </summary>
        /// <param name="index">The post side index</param>
        /// <returns>The uv set to use</returns>
        private UVSet GetUVSide(int index, float length)
        {
            UVSet u = new UVSet(0, 0);
            index += 1;
            u.BL = new Vector2(length, 1 - (0.125f * index));
            u.BR = new Vector2(0, 1 - (0.125f * index));

            u.TL = new Vector2(length, u.BL.y + 0.124f);
            u.TR = new Vector2(0, u.BL.y + 0.124f);
            return u;
        }

        /// <summary>
        /// Get the UV set for the id
        /// </summary>
        /// <param name="uv">The uv set to get</param>
        /// <returns>The UV set</returns>
        private UVSet GetUVEnd()
        {
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, 0f);
            u.TR = new Vector2(0.49f, 0f);

            u.BL = new Vector2(0, 0.5f);
            u.BR = new Vector2(0.49f, 0.5f);

            return u;
        }

        /// <summary>
        /// Get the UV set for the id
        /// </summary>
        /// <param name="uv">The uv set to get</param>
        /// <returns>The UV set</returns>
        private UVSet GetUVSidesVertical(int index)
        {
            float uvHeight = 0.5f;
            
            UVSet u = new UVSet(0, 0);
            index += 1;
            u.TL = new Vector2(0.5f + (index * 0.125f), 0.5f - uvHeight);
            u.TR = new Vector2(0.5f + (index * 0.125f), 0.5f);

            u.BL = new Vector2(u.TL.x - 0.125f, 0.5f - uvHeight);
            u.BR = new Vector2(u.TR.x - 0.125f, 0.5f);
            return u;
        }

        /// <summary>
        /// Get the UV set for the id
        /// </summary>
        /// <param name="uv">The uv set to get</param>
        /// <returns>The UV set</returns>
        private UVSet GetUVSidesVerticalTops(int index)
        {
            index += 1;
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0.5f + (index * 0.125f), 0.5f);
            u.TR = new Vector2(0.5f, 0.5f);

            u.BL = new Vector2(0.5f + (index * 0.125f), 0.5f - 0.125f);
            u.BR = new Vector2(0.5f, 0.5f - 0.125f);
            return u;
        }
    }
}
