using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Vector2 movement;
    public static Vector2 InstrumentPos;
    public static bool isInstrumentActive = false;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, movement, 5f * Time.fixedDeltaTime);
    }
    
    void OnMove(InputValue value)
    {
        movement = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }

}
