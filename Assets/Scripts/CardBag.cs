using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardBag : MonoBehaviour
{
    [SerializeField] private float spawnDistance;
    [SerializeField] private GameObject cardPrafab;
    [SerializeField] private Transform cardParent;

    private void Start()
    {
        StartCoroutine(SpawnCardRoutine());
    }

    private IEnumerator SpawnCardRoutine()
    {
        while (true)
        {
            SpawnCard();
            yield return new WaitForSeconds(10);
        }
    }

    private void SpawnCard()
    {
        Vector3 randPos = Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(cardPrafab, transform.position + randPos, Quaternion.identity, cardParent);
    }
}
