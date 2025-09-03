using UnityEngine;

public class ItemFromBored : MonoBehaviour
{
    public enum ItemType { 
        Gift, 
        Bomb 
    }
    public ItemType itemType;
    public float slowCameraFactor = 0.5f;
    public float slowCameraDuration = 5f;

    public float jumpMultiplier = 1.5f;
    public float boostJumpDuration = 5f;

    public float speedMultiplier = 1.5f;
    public float speedBoostDuration = 5f;

    private enum GiftType { SlowCamera, BoostJump, SpeedBoost }
    private GiftType chosenGift;
    private void Awake()
    {
        if (itemType == ItemType.Gift)
        {
            
            chosenGift = (GiftType)Random.Range(0, 3);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemType == ItemType.Bomb)
            {
                GameEvents.OnGameOver?.Invoke();
            }
            else if (itemType == ItemType.Gift)
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();

                switch (chosenGift)
                {
                    case GiftType.SlowCamera:
                        Camera.main.GetComponent<CamraController>().ApplySlowEffect(slowCameraFactor, slowCameraDuration);
                        break;
                    case GiftType.BoostJump:
                        player.StartCoroutine(player.ApplyJumpBoost(jumpMultiplier, boostJumpDuration));
                        break;
                    case GiftType.SpeedBoost:
                        player.StartCoroutine(player.ApplySpeedBoost(speedMultiplier, speedBoostDuration));
                        break;
                }

                Destroy(gameObject);
            }
        }
    }

void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
