using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScore : MonoBehaviour
{
    public delegate void AddScore();
    public static event AddScore OnScoreAdded;

    private bool _canActivate = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMain player) && _canActivate)
        {
            if (OnScoreAdded != null)
            {
                _canActivate = false;
                OnScoreAdded();
            }
        }
    }
}
