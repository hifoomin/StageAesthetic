using UnityEngine;

namespace StageAesthetic.Variants.Stage1
{
    internal class VerdantFalls
    {

        public static void Sunny(RampFog fog)
        {
            GameObject.Find("Directional Light (SUN)").GetComponent<Light>().color = new Color(0.9333f, 0.8275f, 0.3361f, 1);
            Skybox.DaySky();
        }

        public static void Purple(RampFog fog)
        {
            Skybox.VoidSky();
            GameObject.Find("TLTerrainOuterDistant").SetActive(false);

            AddSnow(SnowType.Moderate);
        }
        /*
        // this gets most of the objects in the map though there's crates and maybe a few minor things that arent covered
        public static void Falls(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Color32 color)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("TLTerrain") && !meshBase.name.Contains("Vines") && !meshBase.name.Contains("GiantFlower") && !meshBase.name.Contains("Ship") && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        // the big flowers dont play well with material changes, so I added a color change to match the theme
                        if ((meshBase.name.Contains("TLTerrain_GiantFlower") || meshBase.name.Contains("TLTerrain_TreePads")) && renderer.sharedMaterial)
                        {
                            foreach (Material mat in renderer.sharedMaterials)
                            {
                                mat.color = color;
                            }
                        }
                        if (meshBase.name.Contains("Vines") || meshBase.name.Contains("TLRoot") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if ((meshBase.name.Contains("TLTerrain_Ship") || meshBase.name.Contains("TLArchi") || meshBase.name.Contains("TLDoor")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if (meshBase.name.Contains("TLRock") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                    }
                }
            }
        */
    }
}