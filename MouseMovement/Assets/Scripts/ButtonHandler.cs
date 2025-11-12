using Unity.VisualScripting;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    bool isPressed = false;
    GameObject[] doors;
    
    void FixedUpdate()
    {
        if (isPressed)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorHandler>().open = true;
            }
        }
        else
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorHandler>().open = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        isPressed = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
    }
}
