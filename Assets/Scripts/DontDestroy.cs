using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek
{
    public class DontDestroy : MonoBehaviour
    {
        [SerializeField] private string objectTag;

        void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(objectTag);

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }
    }
}