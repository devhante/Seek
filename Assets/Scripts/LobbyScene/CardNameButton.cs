using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Seek.LobbyScene
{
    public class CardNameButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private Image image;
        private Button button;
        private CardLibraryPanel cardLibraryPanel;
        private CardData cardData;

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
        }
        private void Start()
        {
            button.onClick.AddListener(onClickButton);
        }

        private void Update()
        {
            var color = image.color;
            bool selected = cardLibraryPanel.selectedId == cardData.Id;
            image.color = new Color(color.r, color.g, color.b, selected ? 1 : 0);
        }

        private void onClickButton()
        {
            cardLibraryPanel.selectedId = cardData.Id;
        }
        
        public void SetCardData(CardLibraryPanel clp, CardData value)
        {
            cardLibraryPanel = clp;
            cardData = value;
            text.text = value.Name;
        }
    }
}