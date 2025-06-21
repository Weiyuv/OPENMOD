using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public Terrain terrain;               // Referência ao Terrain na cena
    public GameObject mobPrefab;          // Prefab do mob a ser spawnado
    public int mobCount = 20;             // Quantidade de mobs para spawnar
    public float spawnAreaMargin = 5f;    // Margem para não spawnar na borda do terreno

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

        for (int i = 0; i < mobCount; i++)
        {
            // Pega uma posição aleatória no plano XZ dentro do terreno
            float randomX = Random.Range(terrainPos.x + spawnAreaMargin, terrainPos.x + terrainData.size.x - spawnAreaMargin);
            float randomZ = Random.Range(terrainPos.z + spawnAreaMargin, terrainPos.z + terrainData.size.z - spawnAreaMargin);

            // Pega a altura do terreno nesse ponto para o eixo Y
            float y = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrainPos.y;

            Vector3 spawnPos = new Vector3(randomX, y, randomZ);

            Instantiate(mobPrefab, spawnPos, Quaternion.identity);
        }
    }
}
