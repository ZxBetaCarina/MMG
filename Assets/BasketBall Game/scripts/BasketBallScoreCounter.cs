using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketBallScoreCounter : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score = 0;
    public GameObject goalParticleEffect;
    public AudioSource GoalSound;
    public GameObject bestone;
    public AudioSource BestGoalSound;

    private float animationDuration = 1f;
    private Vector3 originalScale;

    private void Start()
    {
        if (scoreText != null)
            UpdateScoreText();

        if (bestone != null)
            originalScale = bestone.transform.localScale; // Store the original scale
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone is the ball
        if (other.CompareTag("Ball"))
        {
            Basketball.instance.goal = true;
            score++;
            if (score == 10)
                Basketball.instance.OnGameWin();
            int tmp = Random.Range(1, 5);
            if (tmp == 3)
            {
                bestone.SetActive(true); // Activate the object
                BestGoalSound.Play();
                StartCoroutine(Showtext());
                Invoke("HideObject", animationDuration); // Stop animation after the duration
            }
            GoalSound.Play();
            UpdateScoreText(); // Update the score display

            if (score % 1 == 0)
            {
                Basketball.instance.IncreaseDifficulty(); // Increase arrow speed
            }

            // Activate and hide the particle effect for the goal
            if (goalParticleEffect != null)
            {
                goalParticleEffect.SetActive(true); // Activate the object
                Invoke("HideObject", animationDuration); // Hide the object after 1 second
            }
        }
    }


    // Update the text on the screen to reflect the score
    public void UpdateScoreText()
    {
        
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }


    // Function to hide the object after 1 second
    private void HideObject()
    {
        if (goalParticleEffect != null)
            goalParticleEffect.SetActive(false);

        if (bestone != null)
        {
            bestone.transform.localScale = originalScale;
            bestone.SetActive(false);
        }
    }


    IEnumerator Showtext()
    {
        float elapsedTime = 0f; // Tracks elapsed time

        // Ensure 'bestone' starts at its original scale
        if (bestone != null)
        {
            bestone.transform.localScale = originalScale;
            bestone.SetActive(true);
        }

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the scale factor using a sine wave for a pulsing effect
            float scaleOffset = Mathf.Sin((elapsedTime / animationDuration) * Mathf.PI * 2f) * 0.2f; // Adjust 0.2f for intensity
            Vector3 animatedScale = originalScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);

            // Apply the calculated scale to 'bestone'
            if (bestone != null)
                bestone.transform.localScale = animatedScale;

            yield return null; // Wait for the next frame
        }

        // Ensure the object resets to its original state
        if (bestone != null)
        {
            bestone.transform.localScale = originalScale;
            bestone.SetActive(false);
        }
    }




    //---------------------------------------------------------
    #region Unity Test Code
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnTriggerEnter(Basketball.instance.basketball.GetComponent<Collider>());
        }
    }
#endif
    #endregion

}