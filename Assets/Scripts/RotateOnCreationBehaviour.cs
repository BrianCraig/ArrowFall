using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArrowFall
{
    public class RotateOnCreationBehaviour : MonoBehaviour
    {
        private void Start()
        {
            float GetRandom() => Mathf.Floor(Random.value * 4) * 90;
            var rotation = Quaternion.identity;
            rotation *= Quaternion.Euler(GetRandom(), 0f, 0f);
            rotation *= Quaternion.Euler(0f, GetRandom(), 0f);
            rotation *= Quaternion.Euler(0f, 0f, GetRandom());
            transform.rotation = rotation;
        }
    }
}