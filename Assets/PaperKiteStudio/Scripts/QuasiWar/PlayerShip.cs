using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class PlayerShip : MonoBehaviour
    {
        [SerializeField]
        private bool canMove;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private DialogueManager _dialogueManager;

        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }
        public void EnableMovement()
        {
            canMove = true;
        }
        void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }

            if (canMove)
            {
                float hor = Input.GetAxisRaw("Horizontal");
                float vert = Input.GetAxisRaw("Vertical");

                Vector2 dir = new Vector2(hor, vert);

                if (dir.sqrMagnitude > 0.001f)
                {
                    // Normalize to prevent faster diagonal movement
                    dir.Normalize();

                    // Move the object
                    transform.Translate(dir * _speed * Time.deltaTime, Space.World);

                    // Rotate so Y axis points in direction of movement
                    float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, -angle);
                }
            }
        }

        public void DisableMove()
        {
            canMove = false;
        }
    }
}