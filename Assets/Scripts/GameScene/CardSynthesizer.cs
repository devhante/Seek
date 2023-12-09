using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Seek.GameScene
{
    public class CardSynthesizer : MonoBehaviour
    {
        [SerializeField] private Slider synthesizeBar;

        private Card _card;
        private Coroutine _synthesizeRoutine;
        private bool _isSynthesizing;
        private SynthesizeData _synthesizingCardData;
        private float _synthesizingTime;
        private CardManager _cardManager;

        private void Awake()
        {
            _card = GetComponent<Card>();
            _isSynthesizing = false;
            _synthesizingTime = 0;
            _cardManager = FindObjectOfType<CardManager>();
        }

        private void Update()
        {
            if (_isSynthesizing)
            {
                SynthesizeData synthesizeData = GetSynthesizeData();
                if (synthesizeData.Materials != _synthesizingCardData.Materials)
                {
                    _synthesizingCardData = synthesizeData;
                    StopSynthesize();
                }
            }
            else
            {
                SynthesizeData synthesizeData = GetSynthesizeData();
                if (synthesizeData.Materials.Length != 0)
                {
                    _synthesizingCardData = synthesizeData;
                    _synthesizingTime = _synthesizingCardData.Time;
                    StartSynthesize();
                }
            }
        }

        private SynthesizeData GetSynthesizeData()
        {
            SynthesizeData result = new SynthesizeData();
            if (_card.ChildCards.Count > 0)
            {
                var cardIds = new List<string>();
                cardIds.Add(_card.CardId);
                foreach (var card in _card.ChildCards)
                {
                    cardIds.Add(card.CardId);
                }

                foreach (var data in SynthesizeDataManager.instance.SynthesizeDataList)
                {
                    bool isCorrectData = data.Materials.ToList().OrderBy(a => a).SequenceEqual(cardIds.OrderBy(a => a));
                    Debug.Log(isCorrectData);

                    if (isCorrectData)
                    {
                        result = data;
                        break;
                    }
                }
            }

            return result;
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
            _card.CardId = _synthesizingCardData.Results[0];
            if (_card.ChildCards.Count > 0)
            {
                List<Card> cards = _card.ChildCards.ToList();
                _card.ChildCards[0].DetachParent();
                for (int i = 0; i < cards.Count; i++)
                {
                    _cardManager.DestroyCard(cards[i]);
                }
            }
        }
    }
}