using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Seek.LobbyScene
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private Button backButton;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private TMP_Text sfxText;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private TMP_Text bgmText;

        private void Start()
        {
            backButton.onClick.AddListener(OnClickBackButton);
        }

        private void Update()
        {
            sfxText.text = Mathf.Floor(sfxSlider.value * 100f) + "%";
            bgmText.text = Mathf.Floor(bgmSlider.value * 100f) + "%";
        }

        private void OnClickBackButton()
        {
            mainPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}