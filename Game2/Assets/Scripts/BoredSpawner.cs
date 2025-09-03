using UnityEngine;

public class BoredSpawner : MonoBehaviour
{
    public GameObject regularBoardPrefab;   // ���������
    public Transform minX;                  // ���� �����
    public Transform maxX;                  // ���� ����
    public PlayerMovement player;           // �����
    public Transform finishY;               // ����� ���� �����

    private float lastY;                    // ����� �� ��������� �������
    private GameObject lastBoard;           // ��������� �������

    void Start()
    {
        lastY = 0f;
        SpawnAllPlatforms();                // ������ �� �� ���������� ����
    }

    void SpawnAllPlatforms()
    {
        while (lastY < finishY.position.y)
        {
            // ������ �� ������ ������
            float maxJumpHeight = (player.jumpForce * player.jumpForce) / (2f * Mathf.Abs(Physics2D.gravity.y));
            float minYStep = maxJumpHeight * 0.7f;
            float maxYStep = maxJumpHeight * 0.9f;
            float randomYStep = Random.Range(minYStep, maxYStep);

            // ������ �� �-Y ����
            float newY = lastY + randomYStep;

            // ����� �� �-Y ���� ������
            if (newY > finishY.position.y)
            {
                newY = finishY.position.y; // ����� ����� �����
                SpawnBored(newY);
                break; // �� ����� ��� �������� ���� �����
            }

            SpawnBored(newY);
        }
    }

    void SpawnBored(float newY)
    {
        // --- ���� ����� ����� ������ ---
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

        // --- ����� ��������� ---
        GameObject newBoard = Instantiate(regularBoardPrefab, spawnPosition, Quaternion.identity);

        // --- ����� ��� �������� ---
        TypeOfBoard chooser = newBoard.GetComponent<TypeOfBoard>();
        if (chooser != null)
        {
            chooser.ChooseType();
        }

        lastY = newY;
        lastBoard = newBoard;
    }
}
