using UnityEngine;
using UnityEngine.InputSystem;

public class FollowHandler : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;

    void FixedUpdate()
    {
        movement = GameObject.Find("Elvis").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, movement, speed * Time.fixedDeltaTime);
    }
}
