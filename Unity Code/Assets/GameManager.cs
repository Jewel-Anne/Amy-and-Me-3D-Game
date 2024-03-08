using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CountDownTimer countdownTimer;
    public Transform PlayerRightHand;
    public Transform EnemyRightHand;
    public GameObject objectToHold;

    private bool isGameActive = true;
    private bool isRoundActive = true;

    private void Start()
    {
        // Set up the initial state
        isGameActive = true;
        isRoundActive = true;
    }

    private void Update()
    {
        if (isRoundActive && isGameActive)
        {
            // Check if the round time has exceeded the round duration
            if (countdownTimer.HasTimeEnded)
            {
                // Round end condition reached
                isRoundActive = false;
                DetermineWinner();
            }
        }
    }

    private void DetermineWinner()
    {
        // Check if the object is held by the player or the AI enemy
        bool isObjectHeldByPlayer = objectToHold.transform.IsChildOf(PlayerRightHand);
        bool isObjectHeldByEnemy = objectToHold.transform.IsChildOf(EnemyRightHand);

        if (isObjectHeldByPlayer && !isObjectHeldByEnemy)
        {
            Debug.Log("Player is the winner!");
            SceneManager.LoadScene("Victory");
        }
        else if (!isObjectHeldByPlayer && isObjectHeldByEnemy)
        {
            Debug.Log("AI Enemy is the winner!");
            SceneManager.LoadScene("Defeat");
        }
        else
        {
            Debug.Log("It's a tie! No winner.");
            SceneManager.LoadScene("Tie");
        }

        // End the game
        isGameActive = false;
        isRoundActive = false;
    }

}
