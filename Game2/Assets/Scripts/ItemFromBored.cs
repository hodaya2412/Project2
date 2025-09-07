using UnityEngine;

public class ItemFromBored : MonoBehaviour
   
{
    private float minRange = 0;
    private float maxRange = 3;
    public enum ItemType
    {
        Gift,
        Poison
    }
    public ItemType itemType;
    public float slowCameraFactor = 0.5f;
    public float slowCameraDuration = 5f;

    public float jumpMultiplier = 1.5f;
    public float boostJumpDuration = 2f;

    public float speedMultiplier = 1.5f;
    public float speedBoostDuration = 5f;

    private enum GiftType { SlowCamera, BoostJump, SpeedBoost }
    private GiftType chosenGift;
    private void Awake()
    {
        if (itemType == ItemType.Gift)
        {
            
            chosenGift = (GiftType)Random.Range(minRange, maxRange);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemType == ItemType.Poison)
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
