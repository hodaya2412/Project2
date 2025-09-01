using System.Collections;
using UnityEngine;

public class BoredSpawner : MonoBehaviour
{
    public GameObject regularBoardPrefab;     // Prefab רגיל
    public GameObject breakableBoardPrefab;   // Prefab מתפרק
    public Transform minX;
    public Transform maxX;

    public PlayerMovement player;

    [SerializeField] private float minYDistanceFactor = 0.5f;
    [SerializeField] private float jumpSafetyMargin = 0.7f;     // אחוז מהגובה המקסימלי שהכדור יכול להגיע אליו
    [SerializeField] private float breakableChance = 0.5f;      // סיכוי לפלטפורמה מתפרקת (0-1)
    [SerializeField] private float spawnDelay = 1.5f;

    private float maxYDistance;
    private float minYDistance;
    private float firstPointY = 0f;
    private float lastY;

    void Start()
    {
        lastY = firstPointY;

        // חישוב המרחק המקסימלי לפי קפיצה של השחקן
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float g = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float maxJumpHeight = (player.JumpForce * player.JumpForce) / (2 * g);

        maxYDistance = maxJumpHeight * jumpSafetyMargin;
        minYDistance = maxYDistance * minYDistanceFactor;

        StartCoroutine(SpawnPlatformsRoutine());
    }

    IEnumerator SpawnPlatformsRoutine()
    {
        while (true)
        {
            SpawnBored();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnBored()
    {
        float randomX = Random.Range(minX.position.x, maxX.position.x);
        float randomY = lastY + Random.Range(minYDistance, maxYDistance);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // בחירה אקראית בין רגיל למתפרק לפי הסיכוי
        GameObject prefabToSpawn = (Random.value > breakableChance) ? regularBoardPrefab : breakableBoardPrefab;

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        lastY = randomY;
    }
}
