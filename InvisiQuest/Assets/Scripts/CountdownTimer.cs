using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public TMP_Text timerText;
    //public GameObject agent;
    public AudioSource audioSource;

    private bool isCountingDown = false;
    private float timeRemaining;

    private void Start()
    {
        // Initialize the timer
        timeRemaining = totalTime;
        UpdateTimerText();
    }

    private void Update()
    {
        if (isCountingDown)
        {
            // Update the timer countdown
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            // Check if the time has run out
            if (timeRemaining <= 0f)
            {
                // Stop the countdown
                isCountingDown = false;

                // Set the time remaining to 0
                timeRemaining = 0f;

                audioSource.Play();

                // Activate the agent
                //agent.SetActive(true);
            }
        }
    }

    public void StartCountdown()
    {
        // Start the countdown timer
        isCountingDown = true;
    }

    private void UpdateTimerText()
    {
        // Convert time remaining to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        // Ensure the timer doesn't display negative values
        if (minutes < 0)
            minutes = 0;
        if (seconds < 0)
            seconds = 0;

        // Update the timer text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
