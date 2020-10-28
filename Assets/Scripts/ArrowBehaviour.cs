using Mirror;
using UnityEngine;

namespace ArrowFall
{
    public class ArrowBehaviour : NetworkBehaviour
    {
        private Rigidbody _rigidbody;
        private bool _collided = false;
        public PlayerBehaviour player { get; set; }

        void Start()
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }

        [ServerCallback]
        private void FixedUpdate()
        {
            if (_rigidbody.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision other)
        {
            PlayerBehaviour otherPlayer = other.gameObject.GetComponent<PlayerBehaviour>();
            if (!_collided && otherPlayer != null)
            {
                if (otherPlayer == player)
                {
                    player.points -= 1;
                }
                else
                {
                    player.points += 1;
                }
                
            }
            else if(!_collided)
            {
                _collided = true;
                RpcFreezeConstraints();
            }
            else if (otherPlayer != null)
            {
                otherPlayer.arrows +=1;
                Destroy(this.gameObject);
            }
        }

        [ClientRpc]
        private void RpcFreezeConstraints()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}