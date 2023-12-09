using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Seek
{
    public class CardDataManager : Singleton<CardDataManager>
    {
        [SerializeField] private string cardDataFilePath;
        
        public Dictionary<string, CardData> CardDataList { get; private set; }
        public Dictionary<string, Sprite> CardSpriteList { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CardDataList = new Dictionary<string, CardData>();
            CardSpriteList = new Dictionary<string, Sprite>();
            LoadCardDataList();
            LoadCardSpriteList();
        }

        private void LoadCardDataList()
        {
            var textAsset = Resources.Load<TextAsset>(cardDataFilePath);
            var itemData = JsonConvert.DeserializeObject<CardData[]>(textAsset.text);
            
            foreach (var i in itemData)
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
    }
}
