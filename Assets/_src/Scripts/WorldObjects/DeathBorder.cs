using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    private void MoveBorderForward()
    {
        transform.position += Vector3.forward;
    }

    private void OnEnable()
    {
        TriggerScore.OnScoreAdded += MoveBorderForward;
    }

    private void OnDisable()
    {
        TriggerScore.OnScoreAdded -= MoveBorderForward;
    }
}
