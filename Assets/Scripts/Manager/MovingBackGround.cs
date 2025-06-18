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
        int checkScore = ScoreUI.Instance.GetScore();
        UpdateBackground(checkScore);
    }

    private void UpdateBackground(int checkScore)
    {
        // Set active backgrounds based on star count
        BackGround1.SetActive(checkScore < 599);
        BackGround2.SetActive(checkScore >= 600 && checkScore <= 999);
        BackGround3.SetActive(checkScore >= 1000);
    }
}