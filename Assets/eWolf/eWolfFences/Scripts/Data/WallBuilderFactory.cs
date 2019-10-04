using eWolfFences.Builders;

namespace eWolfFences
{
    public class WallBuilderFactory
    {
        public static WallBuilderBase Create(WallMeshBuilder _meshBuilder, WallDetails.WallTypes wallType, BuildingUnits builderUnits)
        {
            /*switch (wallType)
            {
                case WallDetails.WallTypes_iold.BrickWallLarge:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.15f;
                        bwb.WallHeight = builderUnits.Height;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallUVHeight = 1;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.BrickWallMedium:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.15f;
                        bwb.WallHeight = builderUnits.Height * 0.65f;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallUVHeight = 0.75f;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.BrickWallSmall:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.15f;
                        bwb.WallHeight = builderUnits.Height * 0.45f;
                        bwb.WallUVHeight = 0.45f;
                        bwb.WallLength = builderUnits.Width;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.BrickWallSmallTowers:
                    {
                        BrickWallTowersBuilder bwb = new BrickWallTowersBuilder(_meshBuilder, wallType, builderUnits);
                        bwb.WallWidth = builderUnits.Width * 0.15f;
                        bwb.WallHeight = builderUnits.Height * 0.45f;
                        bwb.WallUVHeight = 0.45f;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallTowerHeight = builderUnits.Height * 0.45f;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.BrickWallSmallTowersTiny:
                    {
                        BrickWallTowersBuilder bwb = new BrickWallTowersBuilder(_meshBuilder, wallType, builderUnits);
                        bwb.WallWidth = builderUnits.Width * 0.15f;
                        bwb.WallHeight = builderUnits.Height * 0.45f;
                        bwb.WallUVHeight = 0.45f;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallTowerHeight = builderUnits.Height * 0.25f;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.HedgeLarge:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.35f;
                        bwb.WallHeight = builderUnits.Height;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallUVHeight = 1;
                        bwb.Tapur = builderUnits.Width * 0.05f;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.HedgeMedium:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.30f;
                        bwb.WallHeight = builderUnits.Height * 0.65f;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallUVHeight = 0.75f;
                        bwb.Tapur = builderUnits.Width * 0.02f;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.HedgeSmall:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.25f;
                        bwb.WallHeight = builderUnits.Height * 0.45f;
                        bwb.WallLength = builderUnits.Width ;
                        bwb.WallUVHeight = 0.35f;
                        bwb.Tapur = builderUnits.Width * 0.01f;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.HedgeLargeUnkept:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.35f;
                        bwb.WallHeight = builderUnits.Height;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallUVHeight = 1;
                        bwb.Tapur = builderUnits.Width * 0.05f;
                        bwb.Unkept = 45;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.HedgeMediumUnkept:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.30f;
                        bwb.WallHeight = builderUnits.Height * 0.65f;
                        bwb.WallLength = builderUnits.Width;
                        bwb.WallUVHeight = 0.75f;
                        bwb.Tapur = builderUnits.Width * 0.02f;
                        bwb.Unkept = 55;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.HedgeSmallUnkept:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = builderUnits.Width * 0.25f;
                        bwb.WallHeight = builderUnits.Height * 0.45f;
                        bwb.WallLength = builderUnits.Width ;
                        bwb.WallUVHeight = 0.35f;
                        bwb.Tapur = builderUnits.Width * 0.01f;
                        bwb.Unkept = 55;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.MetalRailingsGoldTips:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = builderUnits.Height * 0.65f;
                        bwb.WallLength = builderUnits.Width;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.MetalRailingsArch:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = builderUnits.Height * 0.65f;
                        bwb.WallLength = builderUnits.Width;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.MetalRailingsOther:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = builderUnits.Height * 0.80f;
                        bwb.WallLength = builderUnits.Width;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.WoodenFenceLarge:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = builderUnits.Height;
                        bwb.WallLength = builderUnits.Width;
                        return bwb;
                    }
                case WallDetails.WallTypes_iold.WoodenFenceMedium:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = builderUnits.Height * 0.65f;
                        bwb.WallLength = builderUnits.Width;
                        return bwb;
                    }
            }*/
            return null;
        }
    }
}
