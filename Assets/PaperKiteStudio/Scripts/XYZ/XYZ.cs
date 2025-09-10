using System;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZ : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        public static event Action onSteal;
        private void OnEnable()
        {
            //spawn at a random location
            transform.position = new Vector2(0, 5);    
        }
        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0,0), _speed *Time.deltaTime);

            if(transform.position == Vector3.zero)
            {
                onSteal?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}