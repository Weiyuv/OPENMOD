using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public Terrain terrain;               // Referência ao Terrain na cena
    public GameObject mobPrefab;          // Prefab do mob a ser spawnado
    public int mobCount = 20;             // Quantidade de mobs para spawnar
    public float spawnAreaMargin = 5f;    // Margem para não spawnar na borda do terreno
    public float minDistanceBetweenMobs = 10f; // Distância mínima entre os mobs

    void Start()
    {
        SpawnMobs();
    }

    void SpawnMobs()
    {
        if (terrain == null || mobPrefab == null)
        {
            Debug.LogError("Terrain ou mobPrefab não está definido no inspector.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;
        List<Vector3> spawnedPositions = new List<Vector3>();

        for (int i = 0; i < mobCount; i++)
        {
            bool positionFound = false;
            int attempts = 0;
            int maxAttempts = 100;

            while (!positionFound && attempts < maxAttempts)
            {
                attempts++;

                float randomX = Random.Range(terrainPos.x + spawnAreaMargin, terrainPos.x + terrainData.size.x - spawnAreaMargin);
                float randomZ = Random.Range(terrainPos.z + spawnAreaMargin, terrainPos.z + terrainData.size.z - spawnAreaMargin);
                float y = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrainPos.y;

                Vector3 spawnPos = new Vector3(randomX, y, randomZ);

                bool tooClose = false;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(pos, spawnPos) < minDistanceBetweenMobs)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    Instantiate(mobPrefab, spawnPos, Quaternion.identity);
                    spawnedPositions.Add(spawnPos);
                    positionFound = true;
                }
            }

            if (!positionFound)
            {
                Debug.LogWarning($"Não conseguiu achar espaço para mob número: {i}");
            }
        }
    }
}
