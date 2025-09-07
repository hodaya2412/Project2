using System.Collections;
using System.Net;
using UnityEngine;

public class CamraController : MonoBehaviour
{
    public Transform player;
    public Transform endPointY;
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
            GameEvents.OnGameOver?.Invoke();
        }
    }
    public void ApplySlowEffect(float factor, float duration)
    {
        StartCoroutine(SlowCameraRoutine(factor, duration));
    }

    private IEnumerator SlowCameraRoutine(float factor, float duration)
    {
        Debug.Log("you got Slower Camera!");
        float originalSpeed = scrollSpeed;
        scrollSpeed *= factor; 
        yield return new WaitForSeconds(duration);
        scrollSpeed = originalSpeed;
    }

}
