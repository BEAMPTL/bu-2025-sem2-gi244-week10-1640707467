using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        Instantiate(
            playerPrefabs[selectedCharacter],
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}