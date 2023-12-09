using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Seek.LobbyScene
{
    public class CardLibraryPanel : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private Button backButton;
        [SerializeField] private string cardDataFilePath;
        [SerializeField] private GameObject cardNameButton;
        [SerializeField] private GameObject cardList;
        [SerializeField] private Image cardImage;
        
        public string selectedId;
        
        public Dictionary<string, CardData> CardDataList { get; private set; }
        public Dictionary<string, Sprite> CardSpriteList { get; private set; }

        private void Awake()
        {
            CardDataList = new Dictionary<string, CardData>();
            CardSpriteList = new Dictionary<string, Sprite>();
            LoadCardDataList();
            LoadCardSpriteList();
        }

        private void Start()
        {
            backButton.onClick.AddListener(OnClickBackButton);
            foreach (var cardData in CardDataList)
            {
                GameObject go = Instantiate(cardNameButton, cardList.transform);
                go.GetComponent<CardNameButton>().SetCardData(this, cardData.Value);
            }
        }

        private void Update()
        {
            if (CardSpriteList.TryGetValue(selectedId, out var value))
            {
                cardImage.gameObject.SetActive(true);
                cardImage.sprite = value;
            }
            else
            {
                cardImage.gameObject.SetActive(false);
            }
        }

        private void LoadCardDataList()
        {
            var textAsset = Resources.Load<TextAsset>(cardDataFilePath);
            var cardData = JsonConvert.DeserializeObject<CardData[]>(textAsset.text);

            foreach (var i in cardData)
            {
                CardDataList.Add(i.Id, i);
            }
        }
        
        private void LoadCardSpriteList()
        {
            foreach (var i in CardDataList)
            {
                CardSpriteList.Add(i.Key, Resources.Load<Sprite>(i.Value.Image));
            }
        }

        private void OnClickBackButton()
        {
            mainPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}