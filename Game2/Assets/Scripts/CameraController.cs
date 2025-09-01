using UnityEngine;

public class CemraController : MonoBehaviour
{
    public Transform player;
    public float verticalOffset;
    public float scrollSpeed;
    [SerializeField] private float extraThreshold = 0.5f; 

    void LateUpdate()
    {
        Vector3 newPosition = transform.position;

       
        if (player.position.y + verticalOffset > transform.position.y)
        {
            newPosition.y = player.position.y + verticalOffset;
        }

        
        newPosition.y += scrollSpeed * Time.deltaTime;
        transform.position = newPosition;

       
        float cameraBottom = transform.position.y - Camera.main.orthographicSize;

        
        if (player.position.y < cameraBottom - extraThreshold)
        {
            GameEvents.GameOver?.Invoke();
        }
    }
}
