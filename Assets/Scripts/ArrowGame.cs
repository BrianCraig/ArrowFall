using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace ArrowFall
{
    public class ArrowGame
    {
        internal static bool IsConnected()
        {
            return NetworkManager.singleton.isNetworkActive;
        }
        
        internal static List<PlayerBehaviour> PlayersOnline()
        {
            return new List<PlayerBehaviour>(Object.FindObjectsOfType<PlayerBehaviour>());
        }

        internal static PlayerBehaviour Player()
        {
            return PlayersOnline().Find(player => player.isLocalPlayer);
        }
    }
}