using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seek.LobbyScene
{
    public class LoadGamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private Button backButton;
        [SerializeField] private SaveSlot[] saveSlots;
        
        private SaveManager saveManager;

        private void Awake()
        {
            saveManager = FindObjectOfType<SaveManager>();
        }

        private void Start()
        {
            backButton.onClick.AddListener(OnclickBackButton);
            SetSaveSlot();
        }

        private void OnclickBackButton()
        {
            mainPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        private void SetSaveSlot()
        {
            for (int i = 0; i < saveSlots.Length; i++)
            {
                saveSlots[i].isSaved = saveManager.saveDataList[i].Saved;
                saveSlots[i].progress = saveManager.saveDataList[i].Progress;
                saveSlots[i].time = saveManager.saveDataList[i].Time;
            }
        }
    }
}
