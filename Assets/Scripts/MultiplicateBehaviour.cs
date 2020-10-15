using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArrowFall
{
    
    public class MultiplicateBehaviour : MonoBehaviour
    {
        public GameObject prefab;
        public float size = 10f;
        private List<KeyValuePair<GameObject, Vector3>> clones = new List<KeyValuePair<GameObject, Vector3>>();
        private void Start()
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int z = -1; z < 2; z++)
                    {
                        if(x != 0 || y != 0 || z != 0)
                        {
                            Vector3 vector = new Vector3(x*size, y*size, z*size);
                            GameObject clone = Instantiate(prefab, GetPosition(vector), transform.rotation);
                            clones.Add(new KeyValuePair<GameObject, Vector3>(clone, vector));
                        }
                    }
                }
            }
        }

        private void Update()
        {
            ClampPosition();
            UpdateClones();
        }
        void ClampPosition()
        {
            Vector3 transformPosition = transform.position;
            for (int i = 0; i < 3; i++)
            {
                transformPosition[i] = Mathf.Repeat(transformPosition[i], size);
            }
            transform.position = transformPosition;
        }

        void UpdateClones()
        {
            clones.ForEach( clone =>
            {
                clone.Key.transform.position = GetPosition(clone.Value);
                clone.Key.transform.rotation = transform.rotation;
            });
        }
        private Vector3 GetPosition(Vector3 with)
        {
            return transform.position + with;
        }

        private void OnDestroy()
        {
            clones.ForEach( clone => Destroy(clone.Key));
        }
    }
}