using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seek
{
    public class CardSynthesizer : MonoBehaviour
    {
        [SerializeField] private Slider synthesizeBar;

        private Card _card;
        private Coroutine _synthesizeRoutine;
        
        public bool IsSynthesizing { get; private set; }

        private void Awake()
        {
            _card = GetComponent<Card>();
            IsSynthesizing = false;
        }

        public void StartSynthesize()
        {
            IsSynthesizing = true;
            synthesizeBar.gameObject.SetActive(true);
            _synthesizeRoutine = StartCoroutine(SynthesizeRoutine());
        }

        public void StopSynthesize()
        {
            IsSynthesizing = false;
            synthesizeBar.gameObject.SetActive(false);
            StopCoroutine(_synthesizeRoutine);
        }

        private IEnumerator SynthesizeRoutine()
        {
            synthesizeBar.value = 0;
            yield return null;
        }
    }
}