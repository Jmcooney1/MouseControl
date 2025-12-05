using UnityEngine;

public class instrument_handler : MonoBehaviour
{
    public AudioSource instrumentSound;
    public AudioClip soundClip;
    bool isPressed = false;
    
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
            instrumentRelease();
        }
    }

    void instrumentPress()
    {
        Debug.Log("Instrument pressed");
        isPressed = true;
    }

    void instrumentRelease()
    {
        Debug.Log("Instrument released");
        isPressed = false;
    }

}
