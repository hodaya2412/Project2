using UnityEngine;

public class GameManager : MonoBehaviour

{

    private const float TIME_STOPPED = 0f;   
    
    private void OnEnable()
    {
        GameEvents.OnGameOver += HandleGameOver;
        GameEvents.OnPlayerWin += HandleWinGame;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= HandleGameOver;
        GameEvents.OnPlayerWin -= HandleWinGame;
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over You Lose!");
        Time.timeScale = TIME_STOPPED; 
    }

    void HandleWinGame()
    {
        Debug.Log("Game Over You Win!");
        Time.timeScale = TIME_STOPPED;

    }

}
