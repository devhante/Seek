using System;
using TMPro;
using UnityEngine;

namespace Seek.UI
{
    public class DayText : MonoBehaviour
    {
        private TMP_Text _text;
        private int _dayCount;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _dayCount = 1;
        }

        private void Update()
        {
            _text.text = "DAY " + _dayCount;
        }

        public void AddDay()
        {
            _dayCount++;
        }
    }
}
