using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class CrateRaycastManager : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] LayerMask crateLayer; // Assign only the crate layer in Inspector

        //void Update()
        //{
        //    if (Input.GetMouseButtonDown(0)) // Left click
        //    {
        //        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, crateLayer);

        //        if (hit.collider != null)
        //        {
        //            GameObject crate = hit.collider.gameObject;

        //            // Optional: check tag or component
        //        }
        //    }
        //}
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log($"Mouse World Position: {mouseWorldPos}");

                Debug.DrawRay(mouseWorldPos, Vector2.right * 0.5f, Color.red, 1f);

                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log($"Hit object: {hit.collider.name}");
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
                else
                {
                    Debug.Log("Raycast hit nothing.");
                }
            }
        }


    }
}