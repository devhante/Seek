using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Seek
{
    public class CardSynthesizer : MonoBehaviour
    {
        [SerializeField] private Slider synthesizeBar;

        private Card _card;
        private Coroutine _synthesizeRoutine;
        private bool _isSynthesizing;
        private CardName _synthesizingCardName;
        private float _synthesizingTime;
        private CardManager _cardManager;

        private void Awake()
        {
            _card = GetComponent<Card>();
            _isSynthesizing = false;
            _synthesizingCardName = CardName.None;
            _synthesizingTime = 0;
            _cardManager = FindObjectOfType<CardManager>();
        }

        private void Update()
        {
            if (_isSynthesizing)
            {
                CardName cardName = GetSynthesizeCard();
                if (cardName != _synthesizingCardName)
                {
                    _synthesizingCardName = cardName;
                    StopSynthesize();
                }
            }
            else
            {
                CardName cardName = GetSynthesizeCard();
                if (cardName != CardName.None)
                {
                    _synthesizingCardName = cardName;
                    _synthesizingTime = GetSynthesizeTime(_synthesizingCardName);
                    StartSynthesize();
                }
            }
        }

        private CardName GetSynthesizeCard()
        {
            CardName result = CardName.None;
            if (_card.ChildCards.Count > 0)
            {
                var cardNames = new List<CardName>();
                cardNames.Add(_card.CardName);
                foreach (var card in _card.ChildCards)
                {
                    cardNames.Add(card.CardName);
                }

                var woodCount = cardNames.Count(item => item == CardName.Wood);
                if (cardNames.Count == 3 && woodCount == 3)
                    result = CardName.Plate;
            }

            return result;
        }

        private float GetSynthesizeTime(CardName cardName)
        {
            switch (cardName)
            {
                case CardName.Plate:
                    return 5f;
                default:
                    return 0f;
            }
        }

        private void StartSynthesize()
        {
            _isSynthesizing = true;
            synthesizeBar.gameObject.SetActive(true);
            _synthesizeRoutine = StartCoroutine(SynthesizeRoutine());
        }

        private void StopSynthesize()
        {
            _isSynthesizing = false;
            synthesizeBar.gameObject.SetActive(false);
            StopCoroutine(_synthesizeRoutine);
        }

        private IEnumerator SynthesizeRoutine()
        {
            synthesizeBar.value = 0;
            _isSynthesizing = true;
            while (synthesizeBar.value < 1)
            {
                synthesizeBar.value += 1f / _synthesizingTime * Time.smoothDeltaTime;
                yield return null;
            }

            Synthesize();
        }

        private void Synthesize()
        {
            _isSynthesizing = false;
            synthesizeBar.gameObject.SetActive(false);
            _card.CardName = _synthesizingCardName;
            if (_card.ChildCards.Count > 0)
            {
                List<Card> cards = _card.ChildCards.ToList();
                _card.ChildCards[0].DetachParent();
                for (int i = 0; i < cards.Count; i++)
                {
                    _cardManager.DestoyCard(cards[i]);
                }
            }
        }
    }
}