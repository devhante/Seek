using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Seek.GameScene;
using UnityEngine;

namespace Seek.GameScene
{
    public class SynthesizeDataManager : Singleton<SynthesizeDataManager>
    {
        [SerializeField] private string synthesizeDataFilePath;

        public List<SynthesizeData> SynthesizeDataList { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SynthesizeDataList = new List<SynthesizeData>();
            LoadSynthesizeDataList();
        }

        private void LoadSynthesizeDataList()
        {
            var textAsset = Resources.Load<TextAsset>(synthesizeDataFilePath);
            var itemData = JsonConvert.DeserializeObject<SynthesizeData[]>(textAsset.text);
            
            foreach (var i in itemData)
            {
                SynthesizeDataList.Add(i);
            }
        }
    }
}