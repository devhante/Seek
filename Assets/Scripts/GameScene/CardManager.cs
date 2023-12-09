using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Seek.GameScene
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

        public void SpawnCard(string cardId, Vector3 spawnPos)
        {
            if (!CanSpawnCard()) return;

            GameObject card = Instantiate(cardPrefab, spawnPos, Quaternion.identity, cardParent);
            card.GetComponent<Card>().CardId = cardId;
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

        public string GetRandomCardId()
        {
            Random rand = new Random();
            var dict = CardDataManager.instance.CardDataList;
            return dict.ElementAt(rand.Next(0, dict.Count)).Value.Id;
        }
        
        public string GetRandomBaggableCardId()
        {
            bool baggable = false;
            Random rand = new Random();
            CardData result = null;
            while (!baggable)
            {
                var dict = CardDataManager.instance.CardDataList;
                result = dict.ElementAt(rand.Next(0, dict.Count)).Value;
                baggable = result.Bag;
            }

            return result.Id;
        }

        public void DestroyCard(Card card)
        {
            var sr = card.GetComponent<SpriteRenderer>();
            _cardSpriteRenderers.Remove(sr);
            Destroy(card.gameObject);
        }
    }
}
