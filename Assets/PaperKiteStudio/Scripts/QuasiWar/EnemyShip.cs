using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class EnemyShip : MonoBehaviour
    {
        [SerializeField]
        private QuasiGameManager _quasiGameManager;
        [SerializeField]
        private DialogueManager _dialogueManager;

        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _screenShakeStrength;

        public static event Action onHitPlayer;

        [SerializeField]
        private float _canFire;
        [SerializeField]
        private float _fireRate;
        [SerializeField]
        private GameObject _fireVFX;
        [SerializeField]
        private Animator _anim;
        [SerializeField]
        private bool isHit;

        private void OnEnable()
        {
            transform.position = new Vector2(UnityEngine.Random.Range(-9f, 9f), 8);
            isHit = false;
            _anim.Play("Move");
        }
        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _quasiGameManager = FindAnyObjectByType<QuasiGameManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                //SEIZE SHIP!
                PlayerShip player = other.GetComponent<PlayerShip>();
                player.DisableMove(); // basically restart the game.. display restart prompt
                Camera.main.DOShakePosition(0.5f, _screenShakeStrength, 10, 90, true, ShakeRandomnessMode.Full);
                onHitPlayer?.Invoke();
                _quasiGameManager.gameOver = true;
            }
        }
        void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }
            if (!_quasiGameManager.gameOver)
            {
                if(isHit == false)
                {
                    Movement();
                    Fire();
                }

            }
        }
        private void Movement()
        {
            _anim.SetBool("moving", true);
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            if (transform.position.y < -6f)
            {
                gameObject.SetActive(false);
            }
        }
        private void Fire()
        {
            if (Time.time > _canFire)
            {
                _fireRate = UnityEngine.Random.Range(3f, 7f);
                _canFire = Time.time + _fireRate;
                GameObject bullet = ProjectilePool.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    CannonBall projectile = bullet.GetComponent<CannonBall>();
                    bullet.transform.position = transform.position; // can assign specific fire points if we get time. 
                    bullet.transform.rotation = transform.rotation;
                    bullet.SetActive(true);

                    StartCoroutine(VFX());
                }
            }
        }
        IEnumerator VFX()
        {
            _fireVFX.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _fireVFX.SetActive(false);
        }

        public void Damage()
        {
            isHit = true;
            _anim.SetTrigger("Sink");
            StartCoroutine(ResetGO());
        }

        IEnumerator ResetGO()
        {
            yield return new WaitForSeconds(1.15f);
            gameObject.SetActive(false);
        }
    }
}
