using System.Collections.Generic;
using System.Data;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace ArrowFall
{
    public class UIBehaviour : MonoBehaviour
    {
        public Text arrowCounter;
        public Text pointsCounter;
        public List<GameObject> connectedPanels;
        public List<GameObject> disconnectedPanels;
        public GameObject mainPanel;
        private bool _enabledUi = true;

        public bool enabledUI
        {
            get => _enabledUi;
            set
            {
                mainPanel.SetActive(value);
                Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.Locked;
                _enabledUi = value;
            }
        }

        private void Update()
        {
            bool connected = ArrowGame.IsConnected();
            connectedPanels.ForEach(panel => panel.SetActive(connected));
            disconnectedPanels.ForEach(panel => panel.SetActive(!connected));
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                enabledUI = !enabledUI;
            };
            UpdateStatus();
        }

        public void OnHostClick(Text name)
        {
            NetworkManager.singleton.StartHost();
            enabledUI = false;
        }
        
        public void OnJoinClick(Text name)
        {
            NetworkManager.singleton.StartClient();
            enabledUI = false;
        }

        private void UpdateStatus()
        {
            PlayerBehaviour player = ArrowGame.Player();
            if (ArrowGame.IsConnected() && player != null)
            {
                arrowCounter.text = player.arrows.ToString();
                pointsCounter.text = player.points.ToString();
            }
        }
        
        public void OnDisconnect()
        {
            NetworkManager.singleton.StopHost();
        }
        
        public void SetNetworkAddress(string text)
        {
            NetworkManager.singleton.networkAddress = text;
        }
    }
}