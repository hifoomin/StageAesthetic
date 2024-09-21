using UnityEngine;

namespace StageAesthetic.Variants.Stage5
{
  internal class HelminthHatchery
  {
    public static void VanillaChanges(RampFog fog)
    {
      fog.fogColorStart.value = new Color(0.5453f, 0.4527f, 0.3988f, 0.15f);
      GameObject cameraParticles = GameObject.Find("CAMERA PARTICLES: AshParticles");
      if (cameraParticles)
      {
        cameraParticles.transform.GetChild(0).gameObject.SetActive(false);
      }
    }

    public static void Lunar(RampFog fog)
    {
      GameObject cameraParticles = GameObject.Find("CAMERA PARTICLES: AshParticles");
      if (cameraParticles)
      {
        cameraParticles.transform.GetChild(0).gameObject.SetActive(false);
      }
      // fog end 0.3208 0.1234 0.1044 1
      // fog mid 0.5176 0.3338 0.2706 0.4471
      // fog start 0.7453 0.3527 0.2988 0
      fog.fogColorEnd.value = new Color(0.5f, 0.5f, 0.6f, 1);
      fog.fogColorMid.value = new Color(0f, 0.42f, 0.57f, 0.4471f);
      fog.fogColorStart.value = new Color(0f, 0.45f, 0.47f, 0.1f);
      // fog intensity 0.588
      // fog one 0.601
      // fog power 2.27
      // fog zero -0.48
      // skybox strength 0.092
      // 0.9608 0.4704 0 1 lava
      // HRSection temple stuff
      // HRTerrain terrain
      // HRWorm da worm
      // HRRock
      // HRLava
      // "FLOWMAP", "FRESNEL_EMISSION", "SPLATMAP", "USE_VERTEX_COLORS"
      Material terrainMat = new Material(Main.helminthTerrainMat);
      Texture2D blueTex = Main.moonTerrainMat.GetTexture("_BlueChannelTex") as Texture2D;
      Texture2D greenTex = Main.moonTerrainMat.GetTexture("_GreenChannelTex") as Texture2D;
      terrainMat.SetTexture("_GreenChannelTex", blueTex);
      terrainMat.SetTexture("_BlueChannelTex", greenTex);
      // HRFireStatic
      ParticleSystemRenderer[] fireList = Resources.FindObjectsOfTypeAll(typeof(ParticleSystemRenderer)) as ParticleSystemRenderer[];
      foreach (ParticleSystemRenderer psr in fireList)
      {
        GameObject fireBase = psr.gameObject;
        if (fireBase != null)
        {
          if (fireBase.name.Contains("HRFireStatic") && psr.sharedMaterial)
          {
            psr.sharedMaterial = Main.blueFireMat;
            if (psr.transform.GetChild(0))
            {
              psr.transform.GetChild(0).GetComponent<Light>().color = new Color(0f, 0.4704f, 0.9608f, 1);
            }
          }
        }
      }
      MeshRenderer[] meshList = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer)) as MeshRenderer[];
      foreach (MeshRenderer renderer in meshList)
      {
        GameObject meshBase = renderer.gameObject;
        if (meshBase != null)
        {
          if ((meshBase.name.Contains("HRSection") || meshBase.name.Contains("HRGroundCover")) && renderer.sharedMaterial)
          {
            renderer.sharedMaterial = Main.moonDetailMat2;
          }
          if (((meshBase.name.Contains("HRTerrain") && !meshBase.name.Contains("Lava")) || meshBase.name.Contains("HRStalagmitesCombined")) && renderer.sharedMaterial)
          {
            renderer.sharedMaterial = terrainMat;
          }
          if (meshBase.name.Contains("HRWorm") && renderer.sharedMaterial)
          {
            renderer.sharedMaterial = Main.moonDetailMat3;
          }
          if ((meshBase.name.Contains("HRRock") || meshBase.name.Contains("Volcanoid") || meshBase.name.Contains("HRObisidian")) && renderer.sharedMaterial)
          {
            renderer.sharedMaterial = Main.moonDetailMat;
          }
        }
      }
    }
  }
}