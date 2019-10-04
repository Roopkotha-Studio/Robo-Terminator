using eWolfFences.Builders;
using UnityEngine;

namespace eWolfFences
{
    public class BrickWallArchB
    {
        public static void BuildArchB(BrickWallBuilder wallbuilder, Vector3 start, Vector3 direction, float length)
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

            int totalDepthLength = BrickWallArchB.MeshArchBDepth.Length;

            // the under side
            for (int i = 0; i < totalDepthLength; i += 3)
                ApplyDepth(MeshArchBDepth, wallbuilder, i, start, direction, direction90Dec, length, tapurIn);

            for (int i = 0; i < totalDepthLength; i += 3)
                ApplyDepthFlip(MeshArchBDepth, wallbuilder, i, start, direction, direction90Dec, length, tapurOut);
            
            int totalLength = BrickWallArchB.MeshArchBMain.Length;
            // front
            for (int i = 0; i < totalLength; i += 3)
                Apply3(wallbuilder, i, start, direction, length, tapurIn);

            Vector3 startEnd = start - direction90Dec * (wallbuilder.WallWidth);

            // back
            for (int i = 0; i < totalLength; i += 3)
                Apply3Flip(wallbuilder, i, startEnd, direction, length, tapurOut);

            // the top side
            int totalDepthCLength = BrickWallArchB.MeshArchBTop.Length;
            for (int i = 0; i < totalDepthCLength; i += 3)
                ApplyDepth(MeshArchBTop, wallbuilder, i, start, direction, direction90Dec, length, tapurIn);

            for (int i = 0; i < totalDepthCLength; i += 3)
                ApplyDepthFlip(MeshArchBTop, wallbuilder, i, start, direction, direction90Dec, length, tapurOut);
        }

        private static void ApplyDepth(Vector3[] array, BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, Vector3 direction90Dec, float length, Vector3 tapur)
        {
            Vector3 a = array[0 + indexOffSet];
            Vector3 b = array[1 + indexOffSet];
            Vector3 c = array[2 + indexOffSet];

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

            a.y = 1-(a.x + a.y);
            b.y = 1-(b.x + b.y);
            c.y = 1-(c.x + c.y);

            width = 0.25f;
            a.x = 1 - (a.z) * width;
            b.x = 1 - (b.z) * width;
            c.x = 1 - (c.z) * width;

            wallbuilder.MeshBuilders.BuildTri(af, bf, cf, c, a, b);
        }

        private static void ApplyDepthFlip(Vector3[] array, BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, Vector3 direction90Dec, float length, Vector3 tapur)
        {
            Vector3 a = array[0 + indexOffSet];
            Vector3 b = array[1 + indexOffSet];
            Vector3 c = array[2 + indexOffSet];

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

            a.y = 1 - (a.x + a.y);
            b.y = 1 - (b.x + b.y);
            c.y = 1 - (c.x + c.y);

            width = 0.25f;
            a.x = 1 - (a.z) * width;
            b.x = 1 - (b.z) * width;
            c.x = 1 - (c.z) * width;
            wallbuilder.MeshBuilders.BuildTri(cf, bf, af, a, c, b);
        }

        private static void Apply3(BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, float length, Vector3 tapur)
        {
            Vector2 uvOffSet = new Vector2(0, -0.248f);

            Vector3 posA;
            Vector2 uvA;
            GetApply3Values(out uvA, out posA, wallbuilder, 0 + indexOffSet, direction, length, tapur);
            uvA += uvOffSet;
            posA += start;

            Vector3 posB;
            Vector2 uvB;
            GetApply3Values(out uvB, out posB, wallbuilder, 1 + indexOffSet, direction, length, tapur);
            uvB += uvOffSet;
            posB += start;

            Vector3 posC;
            Vector2 uvC;
            GetApply3Values(out uvC, out posC, wallbuilder, 2 + indexOffSet, direction, length, tapur);
            uvC += uvOffSet;
            posC += start;

            wallbuilder.MeshBuilders.BuildTri(posA, posB, posC, uvC, uvA, uvB);
        }

        private static void GetApply3Values(out Vector2 uvA, out Vector3 posA, BrickWallBuilder wallbuilder, int index, Vector3 direction, float length, Vector3 tapur)
        {
            uvA = MeshArchBMain[index];
            float f = wallbuilder.WallHeight * (1 - uvA.y);
            //float width = wallbuilder.WallWidth - ((wallbuilder.Tapur * 2) * (f / wallbuilder.WallHeight));
            posA = GetOffSet(wallbuilder, uvA, length, direction);
            posA -= (tapur * (f / wallbuilder.WallHeight));
            uvA.y = wallbuilder.WallUVHeight - (uvA.y * wallbuilder.WallUVHeight);
        }


        private static void Apply3Flip(BrickWallBuilder wallbuilder, int indexOffSet, Vector3 start, Vector3 direction, float length, Vector3 tapur)
        {
            Vector2 uvOffSet = new Vector2(0, -0.248f);

            Vector2 uvA = MeshArchBMain[0 + indexOffSet];
            Vector2 uvB = MeshArchBMain[1 + indexOffSet];
            Vector2 uvC = MeshArchBMain[2 + indexOffSet];

            float f = wallbuilder.WallHeight * (1 - uvA.y);
            Vector3 af = start + GetOffSetFlip(wallbuilder, uvA, length, direction);
            af -= (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - uvB.y);
            Vector3 bf = start + GetOffSetFlip(wallbuilder, uvB, length, direction);
            bf -= (tapur * (f / wallbuilder.WallHeight));

            f = wallbuilder.WallHeight * (1 - uvC.y);
            Vector3 cf = start + GetOffSetFlip(wallbuilder, uvC, length, direction);
            cf -= (tapur * (f / wallbuilder.WallHeight));
            
            // need to flap the UV Y
            uvA.y = wallbuilder.WallUVHeight - (uvA.y * wallbuilder.WallUVHeight);
            uvB.y = wallbuilder.WallUVHeight - (uvB.y * wallbuilder.WallUVHeight);
            uvC.y = wallbuilder.WallUVHeight - (uvC.y * wallbuilder.WallUVHeight);

            uvA += uvOffSet;
            uvB += uvOffSet;
            uvC += uvOffSet;

            wallbuilder.MeshBuilders.BuildTri(af, bf, cf, uvC, uvA, uvB);
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

        public static Vector2[] MeshArchBMain = {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 0.50f),
            new Vector2(0.09f, 0.27f),
            new Vector2(0.0f, 0.0f),
            new Vector2(0.09f, 0.27f),
            new Vector2(0.09f, 0.27f-0.30f),
            

            new Vector2(0.09f, 0.27f-0.30f),
            new Vector2(0.09f, 0.27f),
            new Vector2(0.29f, 0.13f),
            new Vector2(0.29f, 0.13f),
            new Vector2(0.29f, 0.13f-0.30f),
            new Vector2(0.09f, 0.27f-0.30f),

            new Vector2(0.29f, 0.13f-0.30f),
            new Vector2(0.29f, 0.13f),
            new Vector2(0.50f, 0.11f),
            new Vector2(0.50f, 0.11f),
            new Vector2(0.50f, 0.11f-0.30f),
            new Vector2(0.29f, 0.13f-0.30f),


            new Vector2(1-0.29f, 0.13f-0.30f),
            new Vector2(1-0.50f, 0.11f),
            new Vector2(1-0.29f, 0.13f),
            new Vector2(1-0.50f, 0.11f),
            new Vector2(1-0.29f, 0.13f-0.30f),
            new Vector2(1-0.50f, 0.11f-0.30f),
            
            new Vector2(1-0.09f, 0.27f-0.30f),
            new Vector2(1-0.29f, 0.13f),
            new Vector2(1-0.09f, 0.27f),
            new Vector2(1-0.29f, 0.13f),
            new Vector2(1-0.09f, 0.27f-0.30f),
            new Vector2(1-0.29f, 0.13f-0.30f),
            
            new Vector2(1-0.0f, 0.0f),
            new Vector2(1-0.09f, 0.27f),
            new Vector2(1-0.0f, 0.50f),
            new Vector2(1-0.0f, 0.0f),
            new Vector2(1-0.09f, 0.27f-0.30f),
            new Vector2(1-0.09f, 0.27f),
        };

        public static Vector3[] MeshArchBDepth = {
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

        public static Vector3[] MeshArchBTop = {
            new Vector3(0.0f, 0.00f, 1),
            new Vector3(0.0f, 0.00f, 0),
            new Vector3(0.09f, 0.27f-0.30f, 0),

            new Vector3(0.0f, 0.00f, 1),
            new Vector3(0.09f, 0.27f-0.30f, 0),
            new Vector3(0.09f, 0.27f-0.30f, 1),


            new Vector3(0.09f, 0.27f-0.30f, 1),
            new Vector3(0.09f, 0.27f-0.30f, 0),
            new Vector3(0.29f, 0.13f-0.30f, 0),

            new Vector3(0.09f, 0.27f-0.30f, 1),            
            new Vector3(0.29f, 0.13f-0.30f, 0),
            new Vector3(0.29f, 0.13f-0.30f, 1),


            new Vector3(0.29f, 0.13f-0.30f, 1),
            new Vector3(0.29f, 0.13f-0.30f, 0),
            new Vector3(0.5f, 0.11f-0.30f, 0),

            new Vector3(0.29f, 0.13f-0.30f, 1),
            new Vector3(0.5f, 0.11f-0.30f, 0),
            new Vector3(0.5f, 0.11f-0.30f, 1),
        };
    }
}
