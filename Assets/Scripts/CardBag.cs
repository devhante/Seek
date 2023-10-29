using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Seek
{
    public class CardBag : MonoBehaviour
    {
        [SerializeField] private float spawnDistance;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardParent;

        private void OnMouseDown()
        {
            SpawnCard();
        }

        private void SpawnCard()
        {
            Vector3 randPos = Random.insideUnitCircle;
            Instantiate(cardPrefab, transform.position + randPos, Quaternion.identity, cardParent);
        }
    }
}