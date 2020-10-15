using System;
using Mirror;
using UnityEngine;
using UnityEngine.Animations;

namespace ArrowFall
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        public Transform transform;
        public Rigidbody rigidbody;
        public float movementSpeed = 0.2f;
        private float rotX = 0f, rotY = 0f;
        public float mouseSensitivity = 10f;
        public float jumpForce = 300f;
        public float arrowForce = 500f;
        public float arrowDistance = 1f;
        public GameObject arrowPrefab;
        [SyncVar] public int arrows = 3;
        [SyncVar] public int points = 0;

        private void Start()
        {
            if (isLocalPlayer)
            {
                PositionConstraint cameraPositionConstraint = Camera.main.GetComponent<PositionConstraint>();
                RotationConstraint cameraRotationConstraint = Camera.main.GetComponent<RotationConstraint>();
                ConstraintSource constraintSource = new ConstraintSource();
                constraintSource.weight = 1;
                constraintSource.sourceTransform = transform;
                cameraPositionConstraint.AddSource(constraintSource);
                cameraRotationConstraint.AddSource(constraintSource);
            }
        }

        void Update()
        {
            if (isLocalPlayer)
            {
                rotX += Input.GetAxis("Mouse X") * mouseSensitivity;
                rotY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotY = Mathf.Clamp(rotY, -89f, 89f);
                transform.rotation = Quaternion.Euler(0f, rotX, 0f);
                Camera.main.GetComponent<RotationConstraint>().rotationOffset = new Vector3(-rotY, 0f, 0f);

                if (Input.GetButtonDown("Fire1"))
                {
                    SpawnArrow(transform.position, Camera.main.transform.forward);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    rigidbody.AddForce(Vector3.up * jumpForce);
                }

            }
        }

        [Command]
        void SpawnArrow(Vector3 position, Vector3 forward)
        {
            if (arrows > 0)
            {
                GameObject arrow = Instantiate(arrowPrefab, position + (forward * arrowDistance),
                Quaternion.LookRotation(forward));
                ArrowBehaviour arrowBehaviour = arrow.GetComponent<ArrowBehaviour>();
                arrow.GetComponent<Rigidbody>().AddForce(forward * arrowForce);
                arrowBehaviour.player = this;
                NetworkServer.Spawn(arrow);
                //RpcUseArrow();
                arrows -= 1;
            }
            
        }


        void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                rigidbody.MovePosition(
                    transform.position +
                    transform.forward * (Input.GetAxis("Vertical") * movementSpeed) +
                    transform.right * (Input.GetAxis("Horizontal") * movementSpeed)
                );
            }
        }
        
        [TargetRpc]
        protected internal void RpcFindArrow()
        {
            arrows += 1;
        }
        [TargetRpc]
        private void RpcUseArrow()
        {
            arrows -= 1;
        }
    }
}