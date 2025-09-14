using DG.Tweening;
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
        [SerializeField]
        private GameObject _cannonBallPrefab;
        [Header("Camera Shake Stuff")]
        [SerializeField]
        private float duration;
        [SerializeField]
        private float strength;
        [SerializeField]
        private int vibrato;
        private float _canFire = -1;
        [SerializeField]
        private float _fireRate;

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

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if(_canFire < Time.time)
                    Fire();
                }
            }
        }
        private void Fire()
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_cannonBallPrefab, transform.position, Quaternion.identity);
            Camera.main.DOShakePosition(duration, strength, vibrato, 45, true, ShakeRandomnessMode.Full);
        }
        public void DisableMove()
        {
            canMove = false;
        }
    }
}