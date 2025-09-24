using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    void Update()
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
    }
}
