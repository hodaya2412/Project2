using UnityEngine;
using UnityEngine.Rendering;

public class TypeOfBoard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 2f;

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


        if (roll < 0.33)
        {
            moveHorizontally = false;
            moveVertically = false;
        }

        else if (roll < 0.66)
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
}