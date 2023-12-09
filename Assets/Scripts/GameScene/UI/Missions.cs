using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    [SerializeField] private bool isFolded;
    [SerializeField] private GameObject window;
    [SerializeField] private Button sideButton;
    [SerializeField] private Button arrowButton;

    private void Start()
    {
        sideButton.onClick.AddListener(OnClickSideButton);
        arrowButton.onClick.AddListener(OnClickArrowButton);
    }

    private void Update()
    {
        window.SetActive(!isFolded);
    }

    private void OnClickSideButton()
    {
        isFolded = true;
    }

    private void OnClickArrowButton()
    {
        isFolded = false;
    }
}
