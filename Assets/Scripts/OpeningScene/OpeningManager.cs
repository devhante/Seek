using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Seek.OpeningScene
{
    public class OpeningManager : MonoBehaviour
    {
        [SerializeField] private Button gameStartButton;

        private void Start()
        {
            gameStartButton.onClick.AddListener(OnClickGameStartButton);
        }

        private void OnClickGameStartButton()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
