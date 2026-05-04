using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public Vector3 spawnPos = new Vector3(25, 0, 0);

    public float startDelay = 2f;

    public float startMinDelay = 1f;
    public float startMaxDelay = 3f;

    public float minLimitDelay = 0.5f;
    public float difficultyRate = 0.1f;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (!playerController.gameOver)
        {
            SpawnObstacle();

            float minDelay = Mathf.Max(
                minLimitDelay,
                startMinDelay - Time.timeSinceLevelLoad * difficultyRate
            );

            float maxDelay = Mathf.Max(
                minDelay + 0.5f,
                startMaxDelay - Time.timeSinceLevelLoad * difficultyRate
            );

            float randomDelay = Random.Range(minDelay, maxDelay);

            yield return new WaitForSeconds(randomDelay);
        }
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefab.Length);
        GameObject selectedObstacle = obstaclePrefab[index];
        Instantiate(selectedObstacle, spawnPos, selectedObstacle.transform.rotation);
    }
}