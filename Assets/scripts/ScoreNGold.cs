using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreNGold : MonoBehaviour
{
    [Header("Game Settings")]
    public static ScoreNGold InstanceSNG;
    private int score = 0;
    private int gold = 0;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI goldText;

    [Header("UI Text References")]
    private TextMeshProUGUI ActualSpeedText;
    private TextMeshProUGUI ActualPenetrationText;
    private TextMeshProUGUI ActualFireRateText;

    private void Awake()
    {
        if (InstanceSNG == null)
            InstanceSNG = this;
        else
            Destroy(gameObject);
    }

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

        if (MarketScript.Market != null)
            MarketScript.Market.UpdateMarketBtn();
        else
            Debug.LogWarning("MarketScript instance not found, cannot update market buttons.");
        
        
    }

    public int GetGold() => gold;
}