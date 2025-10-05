using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace PaperKiteStudio.Dangers
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private void Update()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalMovement, 1, verticalMovement);
            transform.Translate(direction * _speed * Time.deltaTime);

            //clamp x and z positions
            Vector3 clampedPosition = transform.position;
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, -10f, -2.0f);
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -17f, 17f);
            clampedPosition.y = 1f; // just in case the player wants to fly away for some reason.
        }
    }
}