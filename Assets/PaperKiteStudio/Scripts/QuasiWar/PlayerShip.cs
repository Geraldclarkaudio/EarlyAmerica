using DG.Tweening;
using System.Collections;
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
        [SerializeField]
        private GameObject _leftFire;
        [SerializeField]
        private GameObject _rightFire;

        bool isfacingleft;
        bool isfacingright;
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

                float zRot = transform.eulerAngles.z;
                if(zRot > 180) zRot -= 360f;
                isfacingleft = zRot > 0;
                isfacingright = zRot < 0;
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
                //clamppin and flampin
                transform.position = new Vector2(
                    Mathf.Clamp(transform.position.x, -9.5f, 9.5f),
                    Mathf.Clamp(transform.position.y, -3.5f, 5.5f));

            }
        }
        private void Fire()
        {
            //if facing left 
            if (isfacingleft)
            {
                StartCoroutine(VFX(_rightFire));
            }
            else if (isfacingright)
            {
                //if facing right 
                StartCoroutine(VFX(_leftFire));
            }
 
            _canFire = Time.time + _fireRate;
            Instantiate(_cannonBallPrefab, transform.position, Quaternion.identity);
            Camera.main.DOShakePosition(duration, strength, vibrato, 45, true, ShakeRandomnessMode.Full);
        }
        IEnumerator VFX(GameObject dirFire)
        {
            dirFire.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            dirFire.SetActive(false);
        }
        public void DisableMove()
        {
            canMove = false;
        }
    }
}