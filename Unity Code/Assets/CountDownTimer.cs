using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float startTimer;
    [SerializeField] float counter;
    [SerializeField] bool hasTimeEnded;
    [SerializeField] Text countDownTimer;

    public bool HasTimeEnded
    {
        get { return hasTimeEnded; }
    }

    // Start is called before the first frame update
    void Start()
    {
        counter = startTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= 0)
        {
            counter -= Time.deltaTime;
        }
        else if (counter <= 0)
        {
            hasTimeEnded = true;
        }
        DisplayText();
    }

    private void DisplayText()
    {
        int timeRemaining = Mathf.RoundToInt(counter);
        countDownTimer.text = string.Format("{0}:{1:00}", timeRemaining / 60, timeRemaining % 60);
    }
}
