using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTerrainSpawn : MonoBehaviour
{
    public delegate void CreateNewTerrain(int value);
    public static event CreateNewTerrain OnNewTerrainCreation;

    private bool _canActivate = true;
    private int _terrainDeletionQuantity;

    public void SetDeletionQuantity(int quantity)
    {
        _terrainDeletionQuantity = quantity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player) && _canActivate)
        {
            if (OnNewTerrainCreation != null)
            {
                _canActivate = false;
                OnNewTerrainCreation(_terrainDeletionQuantity);
            }
        }
    }
}
