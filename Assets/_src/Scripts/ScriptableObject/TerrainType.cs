using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TerrainType", menuName = "Terrain Type")]
public class TerrainType : ScriptableObject
{
    public List<GameObject> PossibleTerrainPrefabs;
    public bool AllowRepetition;
    public bool isRandom;
    [Range(1, 5)] public int MinimumTerrainsToSpawn;
    [Range(1, 5)] public int MaximumTerrainsToSpawn;
}
