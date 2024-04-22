using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrein : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public int numberOfEnemies = 10;
    public Terrain terrain;
    public float spawnHeight = 1f;

    void Start()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            float randomX = Random.Range(0f, terrainSize.x);
            float randomZ = Random.Range(0f, terrainSize.z);
            Vector3 randomPosition = new Vector3(randomX, spawnHeight, randomZ);

            GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            newEnemy.transform.parent = transform; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
