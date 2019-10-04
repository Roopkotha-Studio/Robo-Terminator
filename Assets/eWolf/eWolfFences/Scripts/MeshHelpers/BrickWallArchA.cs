using eWolfFences.Builders;
using UnityEngine;

namespace eWolfFences
{
    public class BrickWallArchA
    {
        public static void BuildArchA(BrickWallBuilder wallbuilder, Vector3 start, Vector3 direction, float length)
        {
            Vector3 direction90Dec = direction;
            direction90Dec = Quaternion.AngleAxis(-90, Vector3.up) * direction90Dec;
            start += direction90Dec * (wallbuilder.WallWidth / 2);

            Vector3 tapurIn = direction;
            tapurIn = Quaternion.AngleAxis(-90, Vector3.up) * tapurIn;
            tapurIn *= wallbuilder.Tapur;

            Vector3 tapurOut = direction;
            tapurOut = Quaternion.AngleAxis(90, Vector3.up) * tapurOut;
            tapurOut *= wallbuilder.Tapur;

            int totalLength = BrickWallArchA.MeshArchAMain.Length;
            int totalDepthLength = BrickWallArchA.MeshArchADepth.Length;

            for (int i = 0; i < totalDepthLength; i += 3)
                ApplyDepth(wallbuilder, i, start, direction, direction90Dec, length, tapurIn);

            for (int i = 0; i < totalDepthLength; i += 3)
                ApplyDepthFlip(wallbuilder, i, start, direction, direction90Dec, length, tapurOut);

            for (int i = 0; i < totalLength; i += 3)
                Apply3(wallbuilder, i, start, direction, length, tapurIn);

            Vector3 startEnd = start - direction90Dec * (wallbuilder.WallWidth);

            for (int i = 0; i < totalLength; i += 3)
                Apply3Flip(wallbuilder, i, startEnd, direction, length, tapurOut);

            float percent = length / wallbuilder.WallLength;

            UVSet topUVs = GetUVTop();
            topUVs.BL.x = percent/2;
            topUVs.BR.x = percent/2;

            Vector3 startTemp = start;
            startTemp.y += wallbuilder.WallHeight;

            float f = wallbuilder.WallHeight * (1);
            float width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));

            Vector3 dir = direction;
            dir = Quaternion.AngleAxis(-90, Vector3.up) * dir;

            startTemp -= dir * (wallbuilder.Tapur * (f / wallbuilder.WallHeight));
            BuilderHelper.AddFloor(wallbuilder.MeshBuilders, startTemp, direction, dir * width, topUVs, length/2);

            startTemp += direction * (length / 2);
            topUVs.TL.x = percent / 2;
            topUVs.TR.x = percent / 2;

            topUVs.BL.x = percent;
            topUVs.BR.x = percent;
            BuilderHelper.AddFloor(wallbuilder.MeshBuilders, startTemp, direction, dir * width, topUVs, length / 2);
        }

        private static void ApplyDepth(BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, Vector3 direction90Dec, float length, Vector3 tapur)
        {
            Vector3 a = BrickWallArchA.MeshArchADepth[0 + indexOffSet];
            Vector3 b = BrickWallArchA.MeshArchADepth[1 + indexOffSet];
            Vector3 c = BrickWallArchA.MeshArchADepth[2 + indexOffSet];

            float f = wallbuilder.WallHeight * (1 - a.y);
            float width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 af = start + GetOffSet(wallbuilder, a, length, width, direction, direction90Dec);
            af -= (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - b.y);
            width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 bf = start + GetOffSet(wallbuilder, b, length, width, direction, direction90Dec);
            bf -= (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - c.y);
            width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 cf = start + GetOffSet(wallbuilder, c, length, width, direction, direction90Dec);
            cf -= (tapur * (f / wallbuilder.WallHeight));

            // need to flap the UV Y
            a.y = 1 - a.y;
            b.y = 1 - b.y;
            c.y = 1 - c.y;

            a.x = (a.z) * 0.25f;
            b.x = (b.z) * 0.25f;
            c.x = (c.z) * 0.25f;

            wallbuilder.MeshBuilders.BuildTri(af, bf, cf, c, a, b);
        }

        private static void ApplyDepthFlip(BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, Vector3 direction90Dec, float length, Vector3 tapur)
        {
            Vector3 a = BrickWallArchA.MeshArchADepth[0 + indexOffSet];
            Vector3 b = BrickWallArchA.MeshArchADepth[1 + indexOffSet];
            Vector3 c = BrickWallArchA.MeshArchADepth[2 + indexOffSet];

            float f = wallbuilder.WallHeight * (1 - a.y);
            float width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 af = start + GetOffSetFlip(wallbuilder, a, length, width, direction, direction90Dec);
            af += (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - b.y);
            width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 bf = start + GetOffSetFlip(wallbuilder, b, length, width, direction, direction90Dec);
            bf += (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - c.y);
            width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 cf = start + GetOffSetFlip(wallbuilder, c, length, width, direction, direction90Dec);
            cf += (tapur * (f / wallbuilder.WallHeight));

            // need to flap the UV Y
            a.y = 1 - a.y;
            b.y = 1 - b.y;
            c.y = 1 - c.y;

            a.x = (a.z) * 0.25f;
            b.x = (b.z) * 0.25f;
            c.x = (c.z) * 0.25f;

            wallbuilder.MeshBuilders.BuildTri(cf, bf, af, a, c, b);
        }

        private static void Apply3(BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, float length, Vector3 tapur)
        {
            Vector2 uvA;
            Vector3 posA;
            GetApply3Values(out uvA, out posA, wallbuilder, 0 + indexOffSet,direction, length, tapur);
            posA += start;

            Vector2 uvB;
            Vector3 posB;
            GetApply3Values(out uvB, out posB, wallbuilder, 1 + indexOffSet, direction, length, tapur);
            posB += start;

            Vector2 uvC;
            Vector3 posC;
            GetApply3Values(out uvC, out posC, wallbuilder, 2 + indexOffSet, direction, length, tapur);
            posC += start;

            wallbuilder.MeshBuilders.BuildTri(posA, posB, posC, uvC, uvA, uvB);
        }

        private static void GetApply3Values(out Vector2 uvA, out Vector3 posA, BrickWallBuilder wallbuilder, int index, Vector3 direction, float length, Vector3 tapur)
        {
            uvA = BrickWallArchA.MeshArchAMain[index];
            float f = wallbuilder.WallHeight * (1 - uvA.y);
            // float width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            posA = GetOffSet(wallbuilder, uvA, length, direction);
            posA -= (tapur * (f / wallbuilder.WallHeight));
            uvA.y = wallbuilder.WallUVHeight - (uvA.y * wallbuilder.WallUVHeight);
        }


        private static void Apply3Flip(BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, float length, Vector3 tapur)
        {
            Vector2 a = BrickWallArchA.MeshArchAMain[0 + indexOffSet];
            Vector2 b = BrickWallArchA.MeshArchAMain[1 + indexOffSet];
            Vector2 c = BrickWallArchA.MeshArchAMain[2 + indexOffSet];

            float f = wallbuilder.WallHeight * (1 - a.y);
            //float width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 af = start + GetOffSetFlip(wallbuilder, a, length, direction);
            af -= (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - b.y);
            //width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 bf = start + GetOffSetFlip(wallbuilder, b, length, direction);
            bf -= (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - c.y);
            //width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            Vector3 cf = start + GetOffSetFlip(wallbuilder, c, length, direction);
            cf -= (tapur * (f / wallbuilder.WallHeight));

            // need to flap the UV Y
            a.y = wallbuilder.WallUVHeight - (a.y * wallbuilder.WallUVHeight);
            b.y = wallbuilder.WallUVHeight - (b.y * wallbuilder.WallUVHeight);
            c.y = wallbuilder.WallUVHeight - (c.y * wallbuilder.WallUVHeight);

            wallbuilder.MeshBuilders.BuildTri(af, bf, cf, c, a, b);
        }

        private static Vector3 GetOffSet(BrickWallBuilder wallbuilder, Vector2 main, float length, Vector3 dir)
        {
            Vector3 ax = (main.x * length) * dir;
            float ay = (main.y * wallbuilder.WallHeight);
            ax.y += wallbuilder.WallHeight - (ay);
            return ax;
        }

        private static Vector3 GetOffSetFlip(BrickWallBuilder wallbuilder, Vector2 main, float length, Vector3 dir)
        {
            main.x = 1 - main.x;
            Vector3 ax = (main.x * length) * dir;
            float ay = (main.y * wallbuilder.WallHeight);
            ax.y += wallbuilder.WallHeight - (ay);
            return ax;
        }

        private static Vector3 GetOffSet(BrickWallBuilder wallbuilder, Vector3 main, float length, float width, Vector3 dir, Vector3 dir90)
        {
            Vector3 ax = (main.x * length) * dir;
            float ay = (main.y * wallbuilder.WallHeight);
            ax.y += wallbuilder.WallHeight - (ay);

            if (main.z == 1)
                ax -= dir90 * width;

            return ax;
        }

        private static Vector3 GetOffSetFlip(BrickWallBuilder wallbuilder, Vector3 main, float length, float width, Vector3 dir, Vector3 dir90)
        {
            main.x = 1 - main.x;
            Vector3 ax = (main.x * length) * dir;
            float ay = (main.y * wallbuilder.WallHeight);
            ax.y += wallbuilder.WallHeight - (ay);

            if (main.z == 1)
                ax -= dir90 * width;

            return ax;
        }

        private static UVSet GetUVTop()
        {
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, 1);
            u.TR = new Vector2(0, 1 - 0.15f);

            u.BL = new Vector2(1, 1);
            u.BR = new Vector2(1, 1 - 0.15f);

            return u;
        }

        private static Vector2[] MeshArchAMain = {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 0.50f),
            new Vector2(0.09f, 0.27f),

            new Vector2(0.0f, 0.0f),
            new Vector2(0.09f, 0.27f),
            new Vector2(0.29f, 0.13f),

            new Vector2(0.0f, 0.0f),
            new Vector2(0.29f, 0.13f),
            new Vector2(0.50f, 0.0f),

            new Vector2(0.50f, 0.0f),
            new Vector2(0.29f, 0.13f),
            new Vector2(0.50f, 0.11f),
            
            // flip
            new Vector2(1-0.50f, 0.0f),
            new Vector2(1-0.50f, 0.11f),
            new Vector2(1-0.29f, 0.13f),

            new Vector2(1-0.0f, 0.0f),
            new Vector2(1-0.50f, 0.0f),
            new Vector2(1-0.29f, 0.13f),

            new Vector2(1-0.0f, 0.0f),
            new Vector2(1-0.29f, 0.13f),
            new Vector2(1-0.09f, 0.27f),

            new Vector2(1-0.0f, 0.0f),
            new Vector2(1-0.09f, 0.27f),
            new Vector2(1-0.0f, 0.5f),
        };

        private static Vector3[] MeshArchADepth = {
            new Vector3(0.0f, 0.50f,1),
            new Vector3(0.0f, 0.50f,0),
            new Vector3(0.0f, 1.0f,0),

            new Vector3(0.0f, 0.50f,1),
            new Vector3(0.0f, 1f,0),
            new Vector3(0.0f, 1f,1),

            new Vector3(0.09f, 0.27f,0),
            new Vector3(0.0f, 0.50f,0),
            new Vector3(0.0f, 0.50f,1),

            new Vector3(0.0f, 0.50f,1),
            new Vector3(0.09f, 0.27f,1),
            new Vector3(0.09f, 0.27f,0),


            new Vector3(0.29f, 0.13f,0),
            new Vector3(0.09f, 0.27f,0),
            new Vector3(0.09f, 0.27f,1),

            new Vector3(0.09f, 0.27f,1),
            new Vector3(0.29f, 0.13f,1),
            new Vector3(0.29f, 0.13f,0),


            new Vector3(0.50f, 0.11f,0),
            new Vector3(0.29f, 0.13f,0),
            new Vector3(0.29f, 0.13f,1),

            new Vector3(0.29f, 0.13f,1),
            new Vector3(0.50f, 0.11f,1),
            new Vector3(0.50f, 0.11f,0),
        };
    }
}
