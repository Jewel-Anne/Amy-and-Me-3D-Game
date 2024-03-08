using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObjectPickup : MonoBehaviour
{
    public GameObject whatCanIPickup;
    public GameObject enemyRightHand;
    public GameObject dropOffPoint;
    public GameObject player;

    public bool isPickedUp = false; // Track the object's pickup state
    private NavMeshAgent navMeshAgent;

    [SerializeField] private AudioSource KinuhaNiAI;
    [SerializeField] private AudioSource DaldalYarn;

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        DaldalYarn.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isPickedUp)
        {
            PickUpObject();
        }
        else
        {
            RunAwayFromPlayer();
        }

        // Check if the AI enemy is holding the object and collided with the player
        if (isPickedUp && whatCanIPickup != null && whatCanIPickup.CompareTag("Player"))
        {
            Debug.Log("AI Enemy dropped object: " + whatCanIPickup.name);
        }
        else if (isPickedUp && whatCanIPickup != null && !whatCanIPickup.transform.IsChildOf(transform))
        {
            isPickedUp = false; // Object is no longer a child, set isPickedUp to false
            Debug.Log("Object is no longer a child of the AI Enemy. Resetting pickup state.");
        }
    }

    public void PickUpObject()
    {
        if (!isPickedUp && whatCanIPickup != null && whatCanIPickup.CompareTag("PickableObject"))
        {
            float distance = Vector3.Distance(transform.position, whatCanIPickup.transform.position);

            // Set your desired range value here (e.g., 5 units)
            float pickupRange = 2f;

            if (distance <= pickupRange)
            {
                whatCanIPickup.transform.SetParent(enemyRightHand.transform);
                whatCanIPickup.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                whatCanIPickup.transform.localPosition = new Vector3(0f, 0f, 0f);
                whatCanIPickup.transform.localRotation = Quaternion.identity; // Set rotation to zero
                isPickedUp = true; // Set the pickup state to true
                KinuhaNiAI.Play();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableObject"))
        {
            whatCanIPickup = other.gameObject;
            if (!isPickedUp)
            {
                // Check if the AI enemy collided with the object
                if (other.gameObject.CompareTag("PickableObject"))
                {
                    PickUpObject();
                    Debug.Log("AI Enemy picked up object: " + other.gameObject.name);
                }
            }
        }
    }

    private void RunAwayFromPlayer()
    {
        Vector3 direction = transform.position - player.transform.position;
        Vector3 newPosition = transform.position + direction.normalized * 5f; // Adjust the multiplier to control the distance the AI enemy runs away

        navMeshAgent.SetDestination(newPosition);
    }
}
