using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Basketball : MonoBehaviour
{
    public static Basketball instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [Header("References")]
    public Transform arrowTip;     // The arrow tip (small cube at the top of the arrow)
    public GameObject basketball;  // The basketball object
    public Transform startPoint;   // The starting point where the ball will reset

    [Header("Settings")]
    public float moveSpeed = 0.1f;     // Speed at which the arrow animation speeds up
    public float shootForce = 10f;     // Base force applied to shoot the ball
    public float shootAngle = 45f;     // Angle to shoot the ball upward (adjust for higher arcs)

    [Header("UI / FX")]
    public Animator arrowAnimation;
    public AudioSource shootSound;
    public GameObject loseCanvas;
    public GameObject WinCanvas;
    public BasketBallScoreCounter counter; // To track the score

    [HideInInspector] public bool goal = false; // True if the shot was scored

    private bool isShooting = false;
    private Vector3 initialBasketballPosition; // Store initial position of the ball
    private Rigidbody rbBasketball;
    public bool isGameOver = true;

    private void Start()
    {
        // Set a slower starting speed for the arrow animation
        arrowAnimation.speed = 0.4f;

        // Store the initial position of the basketball
        initialBasketballPosition = basketball.transform.position;

        // Cache the Rigidbody of the basketball
        rbBasketball = basketball.GetComponent<Rigidbody>();

        // Make the ball kinematic and disable gravity initially
        // so it doesn't fall until the player shoots
        rbBasketball.isKinematic = true;
        rbBasketball.useGravity = false;
    }

    private void Update()
    {
        // If the ball is in the shooting phase, optionally rotate for visual effect
        if (isShooting)
        {
            basketball.transform.Rotate(Vector3.right * 0.5f);
        }
        if (Input.GetMouseButtonDown(0) && !isShooting && !isGameOver)
        {
            ShootBall();
        }
        
        print(arrowAnimation.speed);
        
        if (Input.GetKey(KeyCode.P))
        {
            isGameOver = false;
            
        }
        
    }

    // private void OnMouseDown()
    // {
    //     // If the ball isn't already in motion and the game isn't over, shoot
    //     if (!isShooting && !isGameOver)
    //     {
    //         ShootBall();
    //     }
    // }

    private void ShootBall()
    {
        if (isShooting || isGameOver) return;
        isShooting = true;

        // ---------------------------------------------------
        // 1) Get the "forward" direction from the arrow
        // ---------------------------------------------------
        Vector3 arrowDirection = (arrowTip.position - basketball.transform.position).normalized;

        // ---------------------------------------------------
        // 2) Extract horizontal direction (XZ plane)
        // ---------------------------------------------------
        Vector3 horizontalDirection = new Vector3(arrowDirection.x, 0, arrowDirection.z).normalized;

        // ---------------------------------------------------
        // 3) Create a large upward component based on shootAngle
        //    - For a higher arc, use a higher angle or multiply the tangent
        // ---------------------------------------------------
        float angleRadians = shootAngle * Mathf.Deg2Rad;
        // Multiply the upward factor by 2 for a more pronounced arc
        float upwardFactor = Mathf.Tan(angleRadians) * 2f;

        // Combine horizontal + upward
        Vector3 finalDirection = horizontalDirection + (Vector3.up * upwardFactor);
        finalDirection.Normalize();

        // ---------------------------------------------------
        // 4) Enable physics and apply an impulse in that direction
        // ---------------------------------------------------
        rbBasketball.isKinematic = false;
        rbBasketball.useGravity = true;

        rbBasketball.AddForce(finalDirection * shootForce, ForceMode.Impulse);

        // ---------------------------------------------------
        // 5) Play sound (if assigned)
        // ---------------------------------------------------
        if (shootSound != null)
            shootSound.Play();
    }

    public void IncreaseDifficulty()
    {
        // Increase arrow animation speed to make aiming trickier
        if (arrowAnimation.speed < 1f)
            arrowAnimation.speed += moveSpeed;
        else
            arrowAnimation.speed += (moveSpeed);
    }

    IEnumerator ResetBallAnimation()
    {
        goal = false;

        // Reset ball position and scale
        basketball.transform.position = initialBasketballPosition;
        basketball.transform.localScale = Vector3.zero;

        // Stop all motion
        rbBasketball.velocity = Vector3.zero;
        rbBasketball.angularVelocity = Vector3.zero;

        // Make the ball kinematic and turn off gravity again
        rbBasketball.isKinematic = true;
        rbBasketball.useGravity = false;

        // Play the "newBall" animation to enlarge the ball again
        basketball.GetComponent<Animator>().Play("newBall");

        // Wait briefly before allowing another shot
        yield return new WaitForSeconds(0.25f);
        isShooting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the ball hits the ground and we haven't scored, it's a loss
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGameLost();
            StartCoroutine(ResetBallAnimation());
        }
    }

    public void OnGameWin()
    {
        isGameOver = true;
        arrowAnimation.gameObject.SetActive(false);
        //WinCanvas.SetActive(true);
        
        int count = BB_BetScreen.instance._count;
        int earnedPoints = Mathf.FloorToInt(count * 9f);
        TotalPoints.instance.SetGamePoints(TotalPoints.instance.gamePoints + count);
        TotalPoints.instance.SetEarnedPoints(TotalPoints.instance.earnedPoints + earnedPoints);
        UpdateTransactions.Instance.UpdateGameTransactions("Win on Currency Crush", +count);
        TotalPoints.instance.UpdateWalletPoints();
        UpdateTransactions.Instance.UpdateEarnedTransactions("Win on Currency Crush", +earnedPoints);

        PopUpManager.ShowPopUp("Target achieved!", $"{earnedPoints} points Added. \ntotal Earned Points: {TotalPoints.instance.earnedPoints}");
        SceneManager.LoadScene(0);
    }

    public void OnGameLost()
    {
        // Only lose if we haven't already scored
        if (!goal)
        {
            isGameOver = true;
            arrowAnimation.gameObject.SetActive(false);
            loseCanvas.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        // Reset game state
        isGameOver = false;
        arrowAnimation.speed = 0.4f;
        WinCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        arrowAnimation.gameObject.SetActive(true);

        // Reset score
        if (counter != null)
        {
            counter.score = 0;
            counter.UpdateScoreText();
        }
        
        SceneManager.LoadScene(0);
    }
}