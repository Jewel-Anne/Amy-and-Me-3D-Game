using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public GameObject whatCanIPickup;
    public GameObject playerRightHand;
    public GameObject dropOffPoint;
    public KeyCode pickupKey = KeyCode.E; // Define the pickup key
    public float pickupDistance = 2f; // Define the maximum distance for pickup

    [SerializeField] private AudioSource BinawiNgPlayer;

    public bool isPickedUp = false; // Track the object's pickup state

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pickupKey)) // Check if the pickup key is pressed
        {
            if (isPickedUp)
            {
                DropObject();
            }
            else if (whatCanIPickup != null && IsInRange()) // Check if there is an object to pick up and within range
            {
                PickUpObject();
            }
        }
    }

    public void PickUpObject()
    {
        whatCanIPickup.transform.SetParent(playerRightHand.transform);
        whatCanIPickup.transform.localScale = Vector3.one;
        whatCanIPickup.transform.localPosition = new Vector3(0.01f, 0.23f, 1.66f);
        isPickedUp = true; // Set the pickup state to true
        BinawiNgPlayer.Play();
    }

    public void DropObject()
    {
        whatCanIPickup.transform.SetParent(null); // Set the parent to null to drop the object
        whatCanIPickup.transform.localPosition = dropOffPoint.transform.position;
        isPickedUp = false; // Set the pickup state to false
    }

    private bool IsInRange()
    {
        float distance = Vector3.Distance(transform.position, whatCanIPickup.transform.position);
        return distance <= pickupDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableObject"))
        {
            whatCanIPickup = other.gameObject;
            Debug.Log("It's Pickable: " + other.gameObject.name);
        }
    }
}
