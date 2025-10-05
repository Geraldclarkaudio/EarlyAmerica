using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public enum MovementMode
    {
        TowardTarget,
        FixedDirection,
        UseRotation,
        PlayerInput
    }

    public enum FixedDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class MovingObject : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] protected float _speed = 5f;
        [SerializeField] protected MovementMode movementMode = MovementMode.TowardTarget;

        [Header("Target Settings")]
        [SerializeField] protected GameObject targetObject;

        [Header("Fixed Direction Settings")]
        [SerializeField] protected FixedDirection fixedDirection = FixedDirection.Right;

        protected Vector3 _targetDirection;
        protected bool isMoving = false;

        protected Rigidbody2D _rb;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Reset()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            }
        }



        protected virtual void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleGameStateChanged;

            CalculateDirection();
            StartMoving();
        }

        protected virtual void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleGameStateChanged;
        }

        protected virtual void Update()
        {
            if (!isMoving || movementMode == MovementMode.PlayerInput) return;

            transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.World);
        }

        protected virtual void FixedUpdate()
        {
            if (!isMoving || movementMode != MovementMode.PlayerInput || _rb == null) return;

            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
            _targetDirection = inputDirection.normalized;

            Vector2 movement = _targetDirection * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + movement);
        }

        public virtual void StartMoving()
        {
            isMoving = true;
            CalculateDirection();
        }

        public virtual void StopMoving()
        {
            isMoving = false;
        }

        protected void CalculateDirection()
        {
            if (movementMode == MovementMode.PlayerInput)
            {
                _targetDirection = Vector3.zero; // Let Update handle it
                return;
            }

            switch (movementMode)
            {
                case MovementMode.TowardTarget:
                    if (targetObject != null)
                        _targetDirection = (targetObject.transform.position - transform.position).normalized;
                    else
                        _targetDirection = Vector3.zero;
                    break;

                case MovementMode.FixedDirection:
                    _targetDirection = fixedDirection switch
                    {
                        FixedDirection.Left => Vector3.left,
                        FixedDirection.Right => Vector3.right,
                        FixedDirection.Up => Vector3.up,
                        FixedDirection.Down => Vector3.down,
                        _ => Vector3.zero
                    };
                    break;

                case MovementMode.UseRotation:
                    _targetDirection = transform.up;
                    break;
            }
        }

        protected virtual void HandleGameStateChanged(GameStateMachine.GameState newState)
        {

        }
    }
}