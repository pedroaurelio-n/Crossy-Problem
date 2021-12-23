using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class WorldObject : MonoBehaviour
{
    public abstract void EnterEffect(PlayerMain player);
    public abstract void ExitEffect(PlayerMain player);

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMain player))
        {
            EnterEffect(player);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMain player))
        {
            ExitEffect(player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMain player))
        {
            EnterEffect(player);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMain player))
        {
            ExitEffect(player);
        }
    }
}
