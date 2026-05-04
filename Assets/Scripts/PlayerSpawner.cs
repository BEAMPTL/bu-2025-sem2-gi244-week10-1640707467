using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);

        Instantiate(
            playerPrefabs[selectedIndex],
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}