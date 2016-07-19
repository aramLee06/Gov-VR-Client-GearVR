﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;


namespace Prototype.NetworkLobby 
{
    public class LobbyInfoPanel : MonoBehaviour
    {
		public GameObject OVRCamera;
        public Text infoText;
        public Text buttonText;
        public Button singleButton;

        public void Display(string info, string buttonInfo, UnityEngine.Events.UnityAction buttonClbk)
        {
            infoText.text = info;

            buttonText.text = buttonInfo;

            singleButton.onClick.RemoveAllListeners();

            if (buttonClbk != null)
            {
                singleButton.onClick.AddListener(buttonClbk);

            }

			singleButton.onClick.AddListener(() => { gameObject.SetActive(false); OVRCamera.transform.DOMoveZ (-1.2f, 0.5f);});

            gameObject.SetActive(true);
        }
    }
}