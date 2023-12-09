using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seek.GameScene.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float period;

        private Slider _slider;
        private float _sliderValue;
        private DayText _dayText;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _dayText = FindObjectOfType<DayText>();
            StartCoroutine(SliderRoutine());
        }

        private IEnumerator SliderRoutine()
        {
            while (true)
            {
                if (Mathf.Approximately(_sliderValue, 1f))
                {
                    _sliderValue = 0;
                    _dayText.AddDay();
                }

                _sliderValue += 1 / period;
                _slider.value = _sliderValue;
                yield return new WaitForSeconds(1);
            }
        }
    }
}