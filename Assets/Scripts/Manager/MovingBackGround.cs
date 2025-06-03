using UnityEngine;

public class MovingBackGround : MonoBehaviour
{
    [SerializeField] private GameObject BackGround1;
    [SerializeField] private GameObject BackGround2;
    [SerializeField] private GameObject BackGround3;

    private void Start()
    {
        // Set initial background state
        UpdateBackground(0);
    }

    private void Update()
    {
        if (PlayerCollusion.Instance != null)
        {
            BackGroundChanged();
        }
    }

    private void BackGroundChanged()
    {
        int starCollected = PlayerCollusion.Instance.GetStarCollectCount();
        UpdateBackground(starCollected);
    }

    private void UpdateBackground(int starCount)
    {
        // Set active backgrounds based on star count
        BackGround1.SetActive(starCount < 10);
        BackGround2.SetActive(starCount >= 10 && starCount <= 19);
        BackGround3.SetActive(starCount >= 20);
    }
}