using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Seek.LobbyScene
{
    public class SaveSlot : MonoBehaviour
    {
        [HideInInspector] public bool isSaved;
        [HideInInspector] public int progress;
        [HideInInspector] public string time;

        [SerializeField] private TMP_Text progressText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text newGameText;
        
        private void Update()
        {
            if (isSaved)
            {
                progressText.gameObject.SetActive(true);
                timeText.gameObject.SetActive(true);
                newGameText.gameObject.SetActive(false);
                progressText.text = $"{progress}% Complete";
                timeText.text = $"Time: {time}";
            }
            else
            {
                {
                    progressText.gameObject.SetActive(false);
                    timeText.gameObject.SetActive(false);
                    newGameText.gameObject.SetActive(true);
                }
            }
        }
    }
}