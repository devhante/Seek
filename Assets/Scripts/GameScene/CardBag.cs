using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Seek.GameScene
{
    public class CardBag : MonoBehaviour
    {
        private CardManager _cardManager;

        private void Awake()
        {
            _cardManager = FindObjectOfType<CardManager>();
        }

        private void OnMouseDown()
        {
            Vector3 randPos = Random.insideUnitCircle;
            _cardManager.SpawnCard(_cardManager.GetRandomBaggableCardId(), transform.position + randPos);
        }
    }
}