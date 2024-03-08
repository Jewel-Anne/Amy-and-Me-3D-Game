using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject whatCanIPickup;
    public GameObject dropOffPoint;
    public float speed = 5f;

    private bool isPickedUp = false;

    void Update()
    {
        if (!isPickedUp)
        {
            // Calculate the direction to move away from the player
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();

            // Move the AI enemy
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPickedUp && other.CompareTag("PickableObject"))
        {
            // Pick up the ball
            whatCanIPickup = other.gameObject;
            whatCanIPickup.transform.SetParent(transform);
            whatCanIPickup.transform.localScale = Vector3.one;
            whatCanIPickup.transform.localPosition = new Vector3(0.01f, 0.23f, 1.66f);
            isPickedUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPickedUp && other.gameObject == whatCanIPickup)
        {
            // Drop the ball if the AI enemy moves away from it
            DropObject();
        }
    }

    private void DropObject()
    {
        whatCanIPickup.transform.SetParent(null);
        whatCanIPickup.transform.localPosition = dropOffPoint.transform.position;
        isPickedUp = false;
    }
}
