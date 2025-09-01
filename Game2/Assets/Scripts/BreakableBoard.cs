using UnityEngine;

public class BreakableBoard : MonoBehaviour
{
    private bool isBroken = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBroken) return;

        
        if (collision.gameObject.CompareTag("Player"))
        {
            isBroken = true;
            Destroy(gameObject); 
        }
    }
}
