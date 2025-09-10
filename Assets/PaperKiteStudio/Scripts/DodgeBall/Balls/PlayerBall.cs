using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class PlayerBall : Ball
    {
        private void Awake()
        {
            ballType = BallType.Player;
            _associatedCharacter = GameObject.Find("Player");
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                HitPlayer();
                gameObject.SetActive(false);
            }
        }

        public override void Update()
        {
            if (!isThrown)
            {
                transform.position = _associatedCharacter.transform.GetChild(0).position;
            }

            else
                transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.World);


        }

        public override void Throw()
        {
            _collider.enabled = true;

            if (isThrown) return;

            _targetDirection = Vector2.right;
            isThrown = true;

            Invoke(nameof(DisableBall), 3f);
        }
    }
}