using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Possible Prefabs")]
    [SerializeField] private List<GameObject> prefabList;

    [Header("Position Configs")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("Movement Configs")]
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    [Header("Waiting Configs")]
    [SerializeField] private float minimumWaitTime;
    [SerializeField] private float maximumWaitTime;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        var decreasedWaitTime = Random.Range(0, maximumWaitTime - minimumWaitTime);
        yield return new WaitForSeconds(decreasedWaitTime);

        while (true)
        {
            var randomInt = Random.Range(0, prefabList.Count);
            var tempObject = Instantiate(prefabList[randomInt], startPoint.position, Quaternion.identity, transform);

            tempObject.transform.DOMoveX(endPoint.position.x, duration).SetEase(ease).OnComplete(delegate { Destroy(tempObject); });

            var randomWaitTime = Random.Range(minimumWaitTime, maximumWaitTime);
            yield return new WaitForSeconds(randomWaitTime);
            
            yield return null;

        }
    }
}
