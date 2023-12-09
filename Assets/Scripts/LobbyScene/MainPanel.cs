using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Seek.LobbyScene
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Button cardLibraryButton;
        [SerializeField] private Button achievementButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        [SerializeField] private GameObject loadGamePanel;
        [SerializeField] private GameObject cardLibraryPanel;
        [SerializeField] private GameObject settingsPanel;

        private void Start()
        {
            newGameButton.onClick.AddListener(OnClickNewGameButton);
            loadGameButton.onClick.AddListener(OnClickLoadGameButton);
            cardLibraryButton.onClick.AddListener(OnClickCardLibraryButton);
            achievementButton.onClick.AddListener(OnClickAchievementButton);
            settingsButton.onClick.AddListener(OnClickSettingsButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        private void OnClickNewGameButton()
        {
            SceneManager.LoadScene("OpeningScene");
        }

        private void OnClickLoadGameButton()
        {
            loadGamePanel.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnClickCardLibraryButton()
        {
            cardLibraryPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnClickAchievementButton()
        {

        }

        private void OnClickSettingsButton()
        {
            settingsPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnClickExitButton()
        {

        }
    }
}