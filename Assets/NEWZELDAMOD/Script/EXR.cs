using UnityEngine;

public class EXR : MonoBehaviour
{
    public Terrain terrain;
    public Texture2D heightmapEXR;
    public float heightMultiplier = 10f;

    void Start()
    {
        if (terrain == null || heightmapEXR == null)
        {
            Debug.LogError("Terrain ou Heightmap EXR não definidos!");
            return;
        }

        int width = heightmapEXR.width;
        int height = heightmapEXR.height;

        float[,] heights = new float[height, width];

        Color[] pixels = heightmapEXR.GetPixels();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                float heightValue = pixels[index].r; // Canal Red como altura
                heights[y, x] = heightValue;
            }
        }

        terrain.terrainData.heightmapResolution = width + 1;
        terrain.terrainData.size = new Vector3(width, heightMultiplier, height);
        terrain.terrainData.SetHeights(0, 0, heights);

        Debug.Log("Heightmap EXR aplicado com sucesso!");
    }
}
