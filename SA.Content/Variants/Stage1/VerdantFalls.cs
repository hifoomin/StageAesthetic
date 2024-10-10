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
			ChangeFallMaterials(Main.distantRoostVoidTerrainMat, Main.distantRoostVoidDetailMat, Main.distantRoostVoidDetailMat2, Main.distantRoostVoidDetailMat2, new Color32(188, 162, 162, 255));
            GameObject.Find("TLTerrainOuterDistant").SetActive(false);

            try {
				var sun = GameObject.Find("Weather, Lakes").transform.GetChild(1).gameObject;
				sun.SetActive(true);
				var sunLight = sun.GetComponent<Light>();
				sunLight.color = new Color32(188, 162, 162, 255);
				sunLight.intensity = 1f;
				sunLight.shadowStrength = 0.8f;
            } catch {
				Debug.Log("Could not find the sun.");
			}
			try {
				var probe = GameObject.Find("Weather, Lakes").transform.GetChild(0).gameObject;
				probe.SetActive(true);
            } catch {
				Debug.Log("Could not find the probe.");
			}
			try {
				AddSnow(SnowType.Moderate);
            } catch {
				Debug.Log("Could not add snow.");
			}
        }

        // this gets most of the objects in the map though there's crates and maybe a few minor things that arent covered
        public static void ChangeFallMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Color32 color)
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
                        if ((meshBase.name.Contains("Grass") || meshBase.name.Contains("TLTerrain_GiantFlower") || meshBase.name.Contains("TLTerrain_TreePads")) && renderer.sharedMaterial)
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
        }
    }
}