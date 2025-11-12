using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Vector2 movement;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, movement, 5f * Time.fixedDeltaTime);
    }
    
    void OnMove(InputValue value)
    {
        movement = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }
}
