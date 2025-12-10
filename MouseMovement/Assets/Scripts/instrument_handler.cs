using UnityEngine;
using pem = patrol_enemy_movement;

public class instrument_handler : MonoBehaviour
{
    public AudioSource instrumentSound;
    public AudioClip soundClip;
    bool isPressed = false;

    public patrol_enemy_movement patrol_Enemy_Movement;
    public static Vector2 instrumentPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instrumentSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            instrumentPress();
            instrumentSound.PlayOneShot(soundClip, 1.0f);
            instrumentPos = other.transform.position;
            pem enemy = GameObject.Find("patrol_enemy").GetComponent<pem>();
            enemy.heardInstrument = true;
            enemy.currentIndex = -1;
            enemy.speed = 2;
        }
    }

    void instrumentPress()
    {
        Debug.Log("Instrument pressed");
        isPressed = true;
    }

}