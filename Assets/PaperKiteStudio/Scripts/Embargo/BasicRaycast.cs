using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class BasicRaycast : MonoBehaviour
    {
        [SerializeField] private string raycastName = "Unnamed Raycast";
        public string RaycastName => raycastName;

        [Header("Raycast Settings")]
        [SerializeField] LayerMask crateLayer; // Assign only the crate layer in Inspector

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Debug.DrawRay(mouseWorldPos, Vector2.right * 0.5f, Color.red, 1f);

                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, crateLayer);
                if (hit.collider != null)
                {
                    GameObject crate = hit.collider.gameObject;
                    crate.SetActive(false);

                    if (crate.CompareTag("American"))
                    {
                        if (hit.collider.TryGetComponent<CrateScoreTrigger>(out var trigger))
                        {
                            trigger.ApplyScore();
                            hit.collider.gameObject.SetActive(false); // Return to pool
                        }
                    }
                }
            }
        }


    }
}