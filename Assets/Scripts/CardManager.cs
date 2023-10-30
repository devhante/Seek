using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] private int maxCardNumber;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardParent;

        private List<SpriteRenderer> _cardSpriteRenderers;

        public int CardNumber { get; set; }

        private void Awake()
        {
            _cardSpriteRenderers = new List<SpriteRenderer>();
        }

        public void SpawnCard(Vector3 spawnPos)
        {
            if (!CanSpawnCard()) return;

            GameObject card = Instantiate(cardPrefab, spawnPos, Quaternion.identity, cardParent);
            _cardSpriteRenderers.Add(card.GetComponent<SpriteRenderer>());
            CardNumber++;
        }
        
        private bool CanSpawnCard()
        {
            return CardNumber < maxCardNumber;
        }

        public int GetMaxSortingOrder()
        {
            int result = 0;
            
            foreach (SpriteRenderer sr in _cardSpriteRenderers)
            {
                result = Mathf.Max(result, sr.sortingOrder);
            }
            
            return result;
        }
    }
}
