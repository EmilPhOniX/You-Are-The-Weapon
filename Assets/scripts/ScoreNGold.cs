using TMPro;
using UnityEngine;

public class ScoreNGold : MonoBehaviour
{
    [Header("Game Settings")]
    private int score = 0;
    private int gold = 0;

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI goldText;

    public void AddSnG(int amountScore, int amountGold)
    {
        score += amountScore;
        gold += amountGold;

        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        goldText.text = "Gold: " + gold.ToString();
    }
}
