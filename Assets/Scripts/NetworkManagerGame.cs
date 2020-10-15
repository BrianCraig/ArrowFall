using System;
using Mirror;
using UnityEngine;

namespace ArrowFall
{
    [AddComponentMenu("")]
    public class NetworkManagerGame : NetworkManager
    {
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // add player at correct spawn position
            GameObject player = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnStartClient() 
        {
            
        }
    }
}