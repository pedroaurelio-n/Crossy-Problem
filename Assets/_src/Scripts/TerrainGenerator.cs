using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("TerrainType References")]
    [SerializeField] private List<TerrainType> startTerrainTypes;
    [SerializeField] private List<TerrainType> standardTerrainTypes;

    [Header("Spawn Trigger Reference")]
    [SerializeField] private GameObject spawnTerrainTrigger;

    [Header("Start Spawn Configs")]
    [SerializeField] private int terrainToSpawnOnStart;
    [SerializeField] private int maxTerrainPrefabsOnStart;

    private List<GameObject> spawnedTerrains = new List<GameObject>();
    private float currentZPosition = -3;

    private void Start()
    {
        SpawnStartTerrain();
    }

    private void SpawnStartTerrain()
    {
        SpawnTerrain(startTerrainTypes);

        for (int i = 0; i < terrainToSpawnOnStart; i++)
        {
            if (spawnedTerrains.Count > maxTerrainPrefabsOnStart)
            {
                Debug.Log("Max Terrains Reached");
                break;
            }

            SpawnTerrain(standardTerrainTypes);
        }
    }

    private void SpawnTerrain(List<TerrainType> terrainTypes)
    {
        var terrains = GetTerrainPrefabs(terrainTypes);

        GameObject tempObject;

        for (int i = 0; i < terrains.Length; i++)
        {
            
            var spawnPosition = transform.position + new Vector3(0, 0, 1 * currentZPosition);
            tempObject = Instantiate(terrains[i], spawnPosition, Quaternion.identity, transform);

            spawnedTerrains.Add(tempObject);

            if (i == terrains.Length - 1)
            {
                var tempTrigger = Instantiate(spawnTerrainTrigger, spawnPosition, Quaternion.identity, tempObject.transform);

                if (terrains.Length <= 5)
                    tempTrigger.GetComponent<TriggerTerrainSpawn>().SetDeletionQuantity(terrains.Length);
                else
                    tempTrigger.GetComponent<TriggerTerrainSpawn>().SetDeletionQuantity(0);
            }

            currentZPosition++;
        }
    }

    private void DeleteAndGenerateTerrain(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Destroy(spawnedTerrains[0]);
            spawnedTerrains.RemoveAt(0);
        }

        SpawnTerrain(standardTerrainTypes);
    }

    private GameObject[] GetTerrainPrefabs(List<TerrainType> terrainTypes)
    {
        var randomTerrain = terrainTypes[Random.Range(0, terrainTypes.Count)];
        var consecutiveSpawnQuantity = Random.Range(randomTerrain.MinimumTerrainsToSpawn, randomTerrain.MaximumTerrainsToSpawn + 1);

        GameObject[] randomPrefabs;

        if (randomTerrain.isRandom)
        {
            randomPrefabs = new GameObject[consecutiveSpawnQuantity];

            for (int i = 0; i < consecutiveSpawnQuantity; i++)
            {
                randomPrefabs[i] = randomTerrain.PossibleTerrainPrefabs[Random.Range(0, randomTerrain.PossibleTerrainPrefabs.Count)];
            }
        }
        else
        {
            randomPrefabs = new GameObject[randomTerrain.PossibleTerrainPrefabs.Count];

            for (int i = 0; i < randomTerrain.PossibleTerrainPrefabs.Count; i++)
            {
                randomPrefabs[i] = randomTerrain.PossibleTerrainPrefabs[i];
            }
        }


        return randomPrefabs;
    }

    private void OnEnable()
    {
        TriggerTerrainSpawn.OnNewTerrainCreation += DeleteAndGenerateTerrain;
    }

    private void OnDisable()
    {
        TriggerTerrainSpawn.OnNewTerrainCreation -= DeleteAndGenerateTerrain;
    }
}
