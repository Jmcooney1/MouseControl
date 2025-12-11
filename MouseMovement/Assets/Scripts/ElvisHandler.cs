using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ElvisHandler : MonoBehaviour
{
    // public float speed = 5f;
    private Vector2 movement;

    public AudioSource audioSource;
    AudioClip deathClip;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(movement.x, movement.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Evil"))
        {
            audioSource.PlayOneShot(deathClip, 1.0f);
            Reset();
        }
    }

    void OnMove(InputValue value)
    {
        movement = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
