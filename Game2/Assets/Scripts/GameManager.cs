using UnityEngine;

public class GameManager : MonoBehaviour

{

    private const float TIME_STOPPED = 0f;   
    
    private void OnEnable()
    {
        GameEvents.GameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameEvents.GameOver -= HandleGameOver;
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over triggered by Event!");
        Time.timeScale = TIME_STOPPED; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
