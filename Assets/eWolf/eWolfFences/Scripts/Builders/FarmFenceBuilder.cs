using System.Collections.Generic;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class FarmFenceBuilder : WallBuilderBase
    {
        #region Public Fields
        /// <summary>
        /// THe height of the wall
        /// </summary>
        public float WallHeight;

        /// <summary>
        /// The length of each section
        /// </summary>
        public float SectionLength;

        /// <summary>
        /// The thinkness of the post
        /// </summary>
        public float PostThinkness;

        /// <summary>
        /// The depth of the post
        /// </summary>
        public float PostDepth;

        /// <summary>
        /// Is the fence aged
        /// </summary>
        public bool Aged;
        #endregion

        /// <summary>
        /// The standard consturctor
        /// </summary>
        /// <param name="meshBuilder">The mesh builder</param>
        /// <param name="wallType">The type of the wall</param>
        public FarmFenceBuilder(WallMeshBuilder meshBuilder, WallDetails.WallTypes wallType) : base(meshBuilder, wallType)
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
            _oppersite = false;

            foreach (PartBuilder part in parts)
            {
                part.Builder(part);
            }
        }

        private void FenceSet(Vector3 start, Vector3 direction, float sectionLength)
        {
            Vector3 end = start + (direction * PostThinkness);
            Vector3 endFlat = end;
            endFlat.y = start.y;
            Vector3 directionFlat = (endFlat - start);
            directionFlat.Normalize();

            Vector3 upDirection = new Vector3(0, 1, 0);
            Vector3 rightAnglesFlat = Quaternion.AngleAxis(-90, Vector3.up) * directionFlat;

            Vector3 bottomBegin = start;
            Vector3 bottomEnd = bottomBegin + (directionFlat * PostThinkness);
            Vector3 bottomRightBegin = bottomBegin + (rightAnglesFlat * PostDepth);
            Vector3 bottomRightEnd = bottomEnd + (rightAnglesFlat * PostDepth);

            QuadHolder qh = new QuadHolder();
            qh.LeftBack = bottomBegin;
            qh.RightBack = bottomRightBegin;
            qh.LeftFront = bottomEnd;
            qh.RightFront = bottomRightEnd;
            CuboidMeshUVs cmuvs = new CuboidMeshUVs(0.125f);

            float postHeight = WallHeight;
            if (Aged)
            {
                float valHeight = (WallHeight * 0.1f) * 100;
                postHeight += UnityEngine.Random.Range(-valHeight, valHeight) / 100;
            }

            IApplyShapes verticalPostStart = new CuboidMesh(qh, (upDirection * postHeight));
            if (Aged)
                verticalPostStart.ApplyAgePost(WallHeight / 100);
            verticalPostStart.CreateApplyShape(MeshBuilders, cmuvs);

            if (_oppersite)
                start += (rightAnglesFlat * (PostDepth * 2));

            // Add the horizontal posts
            float postOverHang = 0.025f;
            Vector3 leftBack = start - (direction * (sectionLength * postOverHang));
            leftBack -= (rightAnglesFlat * PostDepth);

            float val = (WallHeight * 0.01f) * 100;
            if (Aged)
                val = (WallHeight * 0.1f) * 100;

            float railHeight = (WallHeight * 0.70f) + UnityEngine.Random.Range(-val, val) / 100;
            float railGap = (PostThinkness * 2.0f) + UnityEngine.Random.Range(-val, val) / 100;

            leftBack += (upDirection * railHeight);
            Vector3 rightBack = leftBack + (rightAnglesFlat * PostDepth);
            Vector3 leftFront = leftBack + (upDirection * (PostThinkness));
            Vector3 rightFront = rightBack + (upDirection * (PostThinkness));

            qh = new QuadHolder();
            qh.LeftBack = leftFront;
            qh.LeftFront = leftBack;
            qh.RightBack = rightFront;
            qh.RightFront = rightBack;
            float horizontalPostLength = sectionLength * (1 + (postOverHang * 2)) + PostThinkness;

            cmuvs = new CuboidMeshUVs(0.125f); // randomize the uvs
            IApplyShapes horizontalPostTop = new CuboidMesh(qh, (direction * horizontalPostLength));
            horizontalPostTop.BottomCap = true;
            horizontalPostTop.TopCap = true;
            horizontalPostTop.CreateApplyShape(MeshBuilders, cmuvs);

            qh = new QuadHolder();
            leftFront -= (upDirection * railGap);
            leftBack -= (upDirection * railGap);
            rightFront -= (upDirection * railGap);
            rightBack -= (upDirection * railGap);
            qh.LeftBack = leftFront;
            qh.LeftFront = leftBack;
            qh.RightBack = rightFront;
            qh.RightFront = rightBack;

            cmuvs = new CuboidMeshUVs(0.125f); // randomize the uvs
            horizontalPostTop = new CuboidMesh(qh, (direction * horizontalPostLength));
            horizontalPostTop.BottomCap = true;
            horizontalPostTop.TopCap = true;
            horizontalPostTop.CreateApplyShape(MeshBuilders, cmuvs);
        }

        private void AddFarmFenceStile(PartBuilder part)
        {
            _oppersite = !_oppersite;
            float sectionLength = part.Length;
            Vector3 start = part.Start;
            Vector3 direction = part.Direction;

            Vector3 end = start + (direction * PostThinkness);
            Vector3 endFlat = end;
            endFlat.y = start.y;
            Vector3 directionFlat = (endFlat - start);
            directionFlat.Normalize();

            Vector3 upDirection = new Vector3(0, 1, 0);
            Vector3 rightAnglesFlat = Quaternion.AngleAxis(-90, Vector3.up) * directionFlat;

            Vector3 bottomBegin = start;
            Vector3 bottomEnd = bottomBegin + (directionFlat * PostThinkness);
            Vector3 bottomRightBegin = bottomBegin + (rightAnglesFlat * PostDepth);
            Vector3 bottomRightEnd = bottomEnd + (rightAnglesFlat * PostDepth);

            QuadHolder qh = new QuadHolder();
            qh.LeftBack = bottomBegin;
            qh.RightBack = bottomRightBegin;
            qh.LeftFront = bottomEnd;
            qh.RightFront = bottomRightEnd;
            CuboidMeshUVs cmuvs = new CuboidMeshUVs(0.125f);

            float postHeight = WallHeight;
            if (Aged)
            {
                float valHeight = (WallHeight * 0.1f) * 100;
                postHeight += UnityEngine.Random.Range(-valHeight, valHeight) / 100;
            }

            if (_oppersite)
                start += (rightAnglesFlat * (PostDepth * 2));

            // Add the horizontal posts
            float postOverHang = 0.025f;
            Vector3 leftBack = start - (direction * (sectionLength * postOverHang));
            leftBack -= (rightAnglesFlat * PostDepth);

            float val = (WallHeight * 0.01f) * 100;
            if (Aged)
                val = (WallHeight * 0.1f) * 100;

            float railHeight = (WallHeight * 0.25f) + UnityEngine.Random.Range(-val, val) / 100;
            // float railGap = (PostThinkness * 2.0f) + UnityEngine.Random.Range(-val, val) / 100;

            leftBack += (upDirection * railHeight);
            Vector3 rightBack = leftBack + (rightAnglesFlat * PostDepth);
            Vector3 leftFront = leftBack + (upDirection * (PostThinkness));
            Vector3 rightFront = rightBack + (upDirection * (PostThinkness));

            qh = new QuadHolder();
            qh.LeftBack = leftFront;
            qh.LeftFront = leftBack;
            qh.RightBack = rightFront;
            qh.RightFront = rightBack;
            float horizontalPostLength = sectionLength * (1 + (postOverHang * 2)) + PostThinkness;

            cmuvs = new CuboidMeshUVs(0.125f); // randomize the uvs
            IApplyShapes horizontalPostTop = new CuboidMesh(qh, (direction * horizontalPostLength));
            horizontalPostTop.BottomCap = true;
            horizontalPostTop.TopCap = true;
            horizontalPostTop.CreateApplyShape(MeshBuilders, cmuvs);


            Vector3 offsetLeft = start + (directionFlat * sectionLength/2);
            offsetLeft += (rightAnglesFlat * sectionLength / 5);
            offsetLeft += (upDirection * (railHeight + PostThinkness));

            leftBack = offsetLeft ;

            rightBack = leftBack + (directionFlat * PostThinkness*2);
            leftFront = leftBack + (upDirection * (PostDepth/2));
            rightFront = rightBack + (upDirection * (PostDepth/2));
            horizontalPostLength = (sectionLength * 0.75f);

            qh = new QuadHolder();
            qh.LeftBack = leftFront;
            qh.LeftFront = leftBack;
            qh.RightBack = rightFront;
            qh.RightFront = rightBack;
            cmuvs = new CuboidMeshUVs(0.125f); // randomize the uvs
            IApplyShapes horizontalPostStep = new CuboidMesh(qh, (rightAnglesFlat * -horizontalPostLength));
            horizontalPostStep.BottomCap = true;
            horizontalPostStep.TopCap = true;
            horizontalPostStep.CreateApplyShape(MeshBuilders, cmuvs);

            offsetLeft = start + (directionFlat * ((sectionLength / 2)+(PostDepth * 0.5f)));
            offsetLeft -= (rightAnglesFlat * sectionLength *0.45f);

            leftBack = offsetLeft;
            rightBack = leftBack + (directionFlat * (PostDepth * 1.5f));
            leftFront = leftBack + (rightAnglesFlat * (PostDepth * 1.5f));
            rightFront = rightBack + (rightAnglesFlat * (PostDepth * 1.5f));
            horizontalPostLength = (sectionLength * 0.75f);

            qh = new QuadHolder();
            qh.LeftBack = leftFront;
            qh.LeftFront = leftBack;
            qh.RightBack = rightFront;
            qh.RightFront = rightBack;
            cmuvs = new CuboidMeshUVs(0.125f); // randomize the uvs
            IApplyShapes horizontalPostStepPost = new CuboidMesh(qh, (upDirection * (railHeight+ PostThinkness)));
            horizontalPostStepPost.BottomCap = false;
            horizontalPostStepPost.TopCap = false;
            horizontalPostStepPost.CreateApplyShape(MeshBuilders, cmuvs);
            
            _oppersite = !_oppersite;
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
                firstPart.Builder = AddFarmFenceNew;
                firstPart.Start = start;
                firstPart.Direction = direction;
                firstPart.Length = firstLen;

                start = gateStart + (gateHalfDir * 2);
                firstLen = (gateStart - wd.End).magnitude;
                firstLen -= featureLength;

                partList.Add(firstPart);
                if (featureStyle == FarmFenceFeature.FarmFenceFeatureStyles.Stile.ToString())
                {
                    PartBuilder stylePart = new PartBuilder();
                    stylePart.StartCap = true;
                    stylePart.EndCap = true;
                    stylePart.WallData = wd;
                    stylePart.Builder = AddFarmFenceStile;
                    stylePart.Start = gateStart - (direction * (PostThinkness*1.5f));
                    stylePart.Direction = direction;
                    stylePart.Length = featureLength + (PostThinkness * 2);
                    partList.Add(stylePart);
                }

                PartBuilder sectionPart = new PartBuilder();
                sectionPart.StartCap = true;
                sectionPart.EndCap = true;
                sectionPart.WallData = wd;
                sectionPart.Builder = AddFarmFenceNew;
                sectionPart.Start = start;
                sectionPart.Direction = direction;
                sectionPart.Length = firstLen;

                partList.Add(sectionPart);
            }
            else
            {
                PartBuilder part = new PartBuilder();
                part.StartCap = true;
                part.EndCap = true;
                part.WallData = wd;
                part.Builder = AddFarmFenceNew;
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
            if (WallType == WallDetails.WallTypes.FarmFence)
                feature = gameObject.GetComponent<FarmFenceFeature>();

            return feature;
        }

        private void AddFarmFenceNew(PartBuilder part)
        {
            Vector3 start = part.Start;
            Vector3 direction = part.Direction;

            float length = part.Length - (PostThinkness * 1.5f);

            int walls = WallSectionCount(length);
            float endWallSize = EndWallLength(length);

            bool two = false;
            if (walls > 0)
            {
                walls -= 1;
                endWallSize += SectionLength;
                endWallSize /= 2;
                two = true;
            }

            for (int i = 0; i < walls; i++)
            {
                FenceSet(start, direction, SectionLength);
                _oppersite = !_oppersite;
                start += direction * SectionLength;
            }

            FenceSet(start, direction, endWallSize);
            _oppersite = !_oppersite;
            start += direction * endWallSize;

            if (two)
            {
                FenceSet(start, direction, endWallSize);
                start += direction * endWallSize;
            }

            VerticalPost(start, direction);
        }

        private void VerticalPost(Vector3 start, Vector3 direction)
        {
            Vector3 end = start + (direction * PostThinkness);
            Vector3 endFlat = end;
            endFlat.y = start.y;
            Vector3 directionFlat = (endFlat - start);
            directionFlat.Normalize();

            Vector3 upDirection = new Vector3(0, 1, 0);
            Vector3 rightAnglesFlat = Quaternion.AngleAxis(-90, Vector3.up) * directionFlat;

            Vector3 bottomBegin = start;
            Vector3 bottomEnd = bottomBegin + (directionFlat * PostThinkness);
            Vector3 bottomRightBegin = bottomBegin + (rightAnglesFlat * PostDepth);
            Vector3 bottomRightEnd = bottomEnd + (rightAnglesFlat * PostDepth);

            QuadHolder qh = new QuadHolder();
            qh.LeftBack = bottomBegin;
            qh.RightBack = bottomRightBegin;
            qh.LeftFront = bottomEnd;
            qh.RightFront = bottomRightEnd;
            CuboidMeshUVs cmuvs = new CuboidMeshUVs(0.125f);

            IApplyShapes verticalPostStart = new CuboidMesh(qh, (upDirection * WallHeight));
            verticalPostStart.CreateApplyShape(MeshBuilders, cmuvs);
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

        #region Private Fields
        private bool _oppersite = false;
        #endregion
    }
}
