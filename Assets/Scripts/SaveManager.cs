using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Seek
{
    public class SaveManager : Singleton<SaveManager>
    {
        [HideInInspector] public List<SaveData> saveDataList;
        
        [SerializeField] private string[] saveDataFilePaths;

        protected override void Awake()
        {
            base.Awake();
            LoadSaveDataList();
        }

        private void LoadSaveDataList()
        {
            foreach (var path in saveDataFilePaths)
            {
                var textAsset = Resources.Load<TextAsset>(path);
                var saveData = JsonConvert.DeserializeObject<SaveData>(textAsset.text);
                saveDataList.Add(saveData);
            }
        }
    }
}
