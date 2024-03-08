using UnityEngine;

public class EnemyPickupZone : MonoBehaviour
{
    private EnemyObjectPickup enemyObjectPickup;

    private void Start()
    {
        enemyObjectPickup = GetComponentInParent<EnemyObjectPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableObject"))
        {
            GameObject pickedObject = other.gameObject;

            // Set the picked object as a child of the enemy's hand
            pickedObject.transform.SetParent(enemyObjectPickup.enemyRightHand.transform);
            pickedObject.transform.localScale = Vector3.one;
            pickedObject.transform.localPosition = Vector3.zero;

            enemyObjectPickup.whatCanIPickup = pickedObject;
            enemyObjectPickup.isPickedUp = true;

            Debug.Log("Enemy picked up object: " + pickedObject.name);
        }
    }
}
