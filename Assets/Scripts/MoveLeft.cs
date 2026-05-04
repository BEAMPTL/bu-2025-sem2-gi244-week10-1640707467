using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float startSpeed = 10f;
    public float speedIncreaseRate = 0.2f;
    public float maxSpeed = 30f;
    private bool hasScored = false;

    private float currentSpeed;

    private float leftBound = -15;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        currentSpeed = startSpeed;
    }

    void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            return;
        }

        if (!playerController.gameOver)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, startSpeed, maxSpeed);

            float moveSpeed = currentSpeed;

            if (playerController.isDashing)
            {
                moveSpeed *= 2f;
            }

            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }

        if (!hasScored && transform.position.x < 0 && gameObject.CompareTag("Obstacle"))
        {
            playerController.AddScore(50);
            hasScored = true;
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}