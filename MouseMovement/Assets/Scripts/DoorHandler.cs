using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public bool isOpen = false;
    public bool open = false;

    void FixedUpdate()
    {
        if (open && !isOpen)
        {
            OpenDoor();
        }
        else if (!open && isOpen)
        {
            CloseDoor();
        }
    }
    public void OpenDoor()
    {
        isOpen = true;
        // Add logic to open the door (e.g., play animation, disable collider, etc.)
    }
    public void CloseDoor()
    {
        isOpen = false;
        // Add logic to close the door (e.g., play animation, enable collider, etc.)
    }
}
