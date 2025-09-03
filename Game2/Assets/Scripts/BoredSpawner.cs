using UnityEngine;

public class BoredSpawner : MonoBehaviour
{
    public GameObject regularBoardPrefab;   // הפלטפורמה
    public Transform minX;                  // גבול שמאלי
    public Transform maxX;                  // גבול ימני
    public PlayerMovement player;           // השחקן
    public Transform finishY;               // נקודת סיום המשחק

    private float lastY;                    // הגובה של הפלטפורמה האחרונה
    private GameObject lastBoard;           // הפלטפורמה האחרונה

    void Start()
    {
        lastY = 0f;
        SpawnAllPlatforms();                // יוצרים את כל הפלטפורמות מראש
    }

    void SpawnAllPlatforms()
    {
        while (lastY < finishY.position.y)
        {
            // מחשבים את המרווח האקראי
            float maxJumpHeight = (player.jumpForce * player.jumpForce) / (2f * Mathf.Abs(Physics2D.gravity.y));
            float minYStep = maxJumpHeight * 0.7f;
            float maxYStep = maxJumpHeight * 0.9f;
            float randomYStep = Random.Range(minYStep, maxYStep);

            // מחשבים את ה-Y החדש
            float newY = lastY + randomYStep;

            // עצירה אם ה-Y חורג מהסיום
            if (newY > finishY.position.y)
            {
                newY = finishY.position.y; // חיתוך לגובה הסיום
                SpawnBored(newY);
                break; // לא נוצרת עוד פלטפורמה מעבר לסיום
            }

            SpawnBored(newY);
        }
    }

    void SpawnBored(float newY)
    {
        // --- מרחק אופקי מותאם לקפיצה ---
        float timeInAir = (2f * player.jumpForce) / Mathf.Abs(Physics2D.gravity.y);
        float maxReachX = player.speed * timeInAir;

        float minXForNext;
        float maxXForNext;

        if (lastBoard == null)
        {
            minXForNext = minX.position.x;
            maxXForNext = maxX.position.x;
        }
        else
        {
            float lastX = lastBoard.transform.position.x;
            minXForNext = Mathf.Max(minX.position.x, lastX - maxReachX);
            maxXForNext = Mathf.Min(maxX.position.x, lastX + maxReachX);
        }

        float randomX = Random.Range(minXForNext, maxXForNext);
        Vector2 spawnPosition = new Vector2(randomX, newY);

        // --- יצירת הפלטפורמה ---
        GameObject newBoard = Instantiate(regularBoardPrefab, spawnPosition, Quaternion.identity);

        // --- בחירת סוג פלטפורמה ---
        TypeOfBoard chooser = newBoard.GetComponent<TypeOfBoard>();
        if (chooser != null)
        {
            chooser.ChooseType();
        }

        lastY = newY;
        lastBoard = newBoard;
    }
}
