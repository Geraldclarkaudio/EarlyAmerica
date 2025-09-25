using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private bool canBurn;
        [SerializeField]
        private bool canMove;
        [SerializeField]
        private GameObject _shipBurnCut;

        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }
        void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
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
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //set the cut scene active..it starts dialogue and increments phase step 
                    if (canBurn) {
                        _shipBurnCut.SetActive(true);
                        canMove = false;
                    }
                }
            }
        }

        public void SetCanBurn(bool canBurnShip)
        {
            canBurn = canBurnShip;
        }
        public bool GetCanBurn()
        {
            return canBurn;
        }

    }
}