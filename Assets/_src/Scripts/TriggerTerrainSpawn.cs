using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTerrainSpawn : MonoBehaviour
{
    public delegate void SpawnNewTerrain();
    public static event SpawnNewTerrain OnNewTerrainSpawn;

    private bool _canActivate = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player) && _canActivate)
        {
            if (OnNewTerrainSpawn != null)
            {
                _canActivate = false;
                OnNewTerrainSpawn();
            }
        }
    }
}
