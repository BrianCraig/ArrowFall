using UnityEngine;

namespace ArrowFall
{
    public class BlockBehaviour : MonoBehaviour
    {

        public void OnPlayerCollision(PlayerBehaviour player, Collision collision)
        {
            player.canJump = true;
            player.canDash = true;
        }
    }
}
