using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    int pressCount = 0;
    public GameObject[] doors;

    // <-- Assign this in Inspector to the layer that IS allowed to press the button
    public LayerMask allowedLayers;

    void FixedUpdate()
    {
        bool opening = pressCount > 0;

        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorHandler>().opening = opening;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only allow activation if the object's layer matches allowedLayers
        if (((1 << collision.gameObject.layer) & allowedLayers) != 0)
        {
            pressCount++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & allowedLayers) != 0)
        {
            pressCount = Mathf.Max(pressCount - 1, 0);
        }
    }
}
