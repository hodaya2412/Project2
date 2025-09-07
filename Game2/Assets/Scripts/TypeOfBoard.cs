using UnityEngine;
using UnityEngine.Rendering;

public class TypeOfBoard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 2f;

    private double standPlatformPercent = 0.6;
    private double HorizontallyPlatformPercent = 0.8;
    private bool moveHorizontally = false;
    private bool moveVertically = false;

    private Vector2 startPosition;


    void Start()
    {
        ChooseType(); 
    }

    public void ChooseType()
    {
        startPosition = transform.position;
        float roll = Random.value;


        if (roll < standPlatformPercent)
        {
            moveHorizontally = false;
            moveVertically = false;
        }

        else if (roll < HorizontallyPlatformPercent)
        {
            moveHorizontally = true;
            moveVertically = false;
        }
        else
        {
            moveHorizontally = false;
            moveVertically = true;
        }
    }

    private void Update()
    {
        if (moveHorizontally)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
            transform.position = new Vector2(startPosition.x + offset, startPosition.y);

        }
        else if (moveVertically)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
            transform.position = new Vector2(startPosition.x , startPosition.y + offset);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            collision.transform.SetParent(transform);
        }
    }


}