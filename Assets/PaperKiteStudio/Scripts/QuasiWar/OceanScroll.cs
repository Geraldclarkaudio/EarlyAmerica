using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class OceanScroll : MonoBehaviour
    {
        public float scrollSpeed = 0.1f;
        private SpriteRenderer rend;
        [SerializeField]
        private Vector2 offset;

        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            offset.y += scrollSpeed * Time.deltaTime;
            rend.material.mainTextureOffset = offset;
        }
    }
}