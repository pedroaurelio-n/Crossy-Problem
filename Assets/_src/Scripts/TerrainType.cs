using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Terrain Type", menuName = "New Terrain Type")]
public class TerrainType : ScriptableObject
{
    public List<GameObject> PossibleTerrainPrefabs;
    public bool isRandom;
    [Range(1, 5)] public int MinimumTerrainsToSpawn;
    [Range(1, 5)] public int MaximumTerrainsToSpawn;
}
