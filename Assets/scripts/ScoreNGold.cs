using TMPro;
using UnityEngine;

public class ScoreNGold : MonoBehaviour
{
    [Header("Game Settings")]
    private int score = 0;
    private int gold = 0;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI goldText;

    void Start()
    {
        FindUIElements();
        UpdateUI();
    }

    private void FindUIElements()
    {
        GameObject scoreObj = GameObject.Find("Score");
        GameObject goldObj = GameObject.Find("Gold");

        if (scoreObj != null)
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogError("Objet 'score' non trouvé dans la scène!");

        if (goldObj != null)
            goldText = goldObj.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogError("Objet 'golad' non trouvé dans la scène!");
    }

    public void AddSnG(int amountScore, int amountGold)
    {
        score += amountScore;
        gold += amountGold;

        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
        goldText.text = gold.ToString();
    }
}
