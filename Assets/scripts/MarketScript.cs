using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketScript : MonoBehaviour
{
    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI speedPriceText;
    [SerializeField] private TextMeshProUGUI penetrationPriceText;
    [SerializeField] private TextMeshProUGUI fireRatePriceText;
    [SerializeField] private TextMeshProUGUI actualSpeedText;
    [SerializeField] private TextMeshProUGUI actualPenetrationText;
    [SerializeField] private TextMeshProUGUI actualFireRateText;

    [Header("Initial Prices")]
    [SerializeField] private int initialSpeedPrice = 20;
    [SerializeField] private int initialPenetrationPrice = 15;
    [SerializeField] private int initialFireRatePrice = 20;
    
    [Header("Price Scaling")]
    [SerializeField] private int priceIncreasePerUpgrade = 5;
    
    [Header("Object References")]
    [SerializeField] private Gun playerGun;

    private int currentSpeedPrice;
    private int currentPenetrationPrice;
    private int currentFireRatePrice;

    private int currentSpeedLevel;
    private int currentPenetrationLevel;
    private int currentFireRateLevel;

    private Button speedUpgradeButton;
    private Button penetrationUpgradeButton;
    private Button fireRateUpgradeButton;

    public static MarketScript Market { get; private set; }

    private void Awake()
    {
        if (Market == null)
            Market = this;
    }

    void Start()
    {
        InitializePrices();
        FindUIElementsIfNeeded();
        UpdatePriceDisplay();

        speedUpgradeButton = GameObject.Find("SpeedBtn").GetComponent<Button>();
        penetrationUpgradeButton = GameObject.Find("PenetrationBtn").GetComponent<Button>();
        fireRateUpgradeButton = GameObject.Find("FireRateBtn").GetComponent<Button>();

        currentSpeedLevel = 0;
        currentPenetrationLevel = 0;
        currentFireRateLevel = 0;
    }
    
    private void InitializePrices()
    {
        currentSpeedPrice = initialSpeedPrice;
        currentPenetrationPrice = initialPenetrationPrice;
        currentFireRatePrice = initialFireRatePrice;
    }

    private void FindUIElementsIfNeeded()
    {
        if (speedPriceText == null || penetrationPriceText == null || fireRatePriceText == null)
            FindUIElements();
    }

    private void FindUIElements()
    {
        if (speedPriceText == null)
        {
            GameObject speedPriceObj = GameObject.FindGameObjectWithTag("SpeedPriceUI") ?? GameObject.Find("PriceSpeed");
            if (speedPriceObj != null)
                speedPriceText = speedPriceObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogError("Speed price UI object not found in scene!");
        }

        if (penetrationPriceText == null)
        {
            GameObject penetrationPriceObj = GameObject.FindGameObjectWithTag("PenetrationPriceUI") ?? GameObject.Find("PricePenetration");
            if (penetrationPriceObj != null)
                penetrationPriceText = penetrationPriceObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogError("Penetration price UI object not found in scene!");
        }

        if (fireRatePriceText == null)
        {
            GameObject fireRatePriceObj = GameObject.FindGameObjectWithTag("FireRatePriceUI") ?? GameObject.Find("PriceFireRate");
            if (fireRatePriceObj != null)
                fireRatePriceText = fireRatePriceObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogError("Fire rate price UI object not found in scene!");
        }

        if (actualSpeedText == null)
        {
            GameObject actualSpeedObj = GameObject.Find("ActualSpeed");
            if (actualSpeedObj != null)
                actualSpeedText = actualSpeedObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogError("Actual speed UI object not found in scene!");
        }

        if (actualPenetrationText == null)
        {
            GameObject actualPenetrationObj = GameObject.Find("ActualPenetration");
            if (actualPenetrationObj != null)
                actualPenetrationText = actualPenetrationObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogError("Actual penetration UI object not found in scene!");
        }

        if (actualFireRateText == null)
        {
            GameObject actualFireRateObj = GameObject.Find("ActualFireRate");
            if (actualFireRateObj != null)
                actualFireRateText = actualFireRateObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogError("Actual fire rate UI object not found in scene!");
        }
    }

    public void UpdatePriceDisplay()
    {
        if (ScoreNGold.InstanceSNG == null)
        {
            Debug.LogError("ScoreNGold instance not found!");
            return;
        }

        if (speedPriceText != null)
            speedPriceText.text = currentSpeedPrice.ToString();

        if (penetrationPriceText != null)
            penetrationPriceText.text = currentPenetrationPrice.ToString();

        if (fireRatePriceText != null)
            fireRatePriceText.text = currentFireRatePrice.ToString();

        if (actualSpeedText != null)
            actualSpeedText.text = currentSpeedLevel.ToString();

        if (actualPenetrationText != null)
            actualPenetrationText.text = currentPenetrationLevel.ToString();

        if (actualFireRateText != null)
            actualFireRateText.text = currentFireRateLevel.ToString();
    }

    public void PurchaseSpeedUpgrade()
    {
        if (CanAffordUpgrade(currentSpeedPrice) && CanUpgradeSpeed())
        {
            ProcessPurchase(currentSpeedPrice);
            playerGun.UpgradeBulletSpeed();
            currentSpeedPrice += priceIncreasePerUpgrade;
            currentSpeedLevel++;
            UpdatePriceDisplay();

            Debug.Log($"Speed upgrade purchased! New price: {currentSpeedPrice}");
        }
        else
            HandlePurchaseFailure("speed upgrade");
    }

    public void PurchasePenetrationUpgrade()
    {
        if (CanAffordUpgrade(currentPenetrationPrice) && CanUpgradePenetration())
        {
            ProcessPurchase(currentPenetrationPrice);
            playerGun.UpgradeBulletPenetration();
            currentPenetrationPrice += priceIncreasePerUpgrade;
            currentPenetrationLevel++;
            UpdatePriceDisplay();

            Debug.Log($"Penetration upgrade purchased! New price: {currentPenetrationPrice}");
        }
        else
            HandlePurchaseFailure("penetration upgrade");
    }

    public void PurchaseFireRateUpgrade()
    {
        if (CanAffordUpgrade(currentFireRatePrice) && CanUpgradeFireRate())
        {
            ProcessPurchase(currentFireRatePrice);
            playerGun.UpgradeFireRate();
            currentFireRatePrice += priceIncreasePerUpgrade;
            currentFireRateLevel++;
            UpdatePriceDisplay();

            Debug.Log($"Fire rate upgrade purchased! New price: {currentFireRatePrice}");
        }
        else
            HandlePurchaseFailure("fire rate upgrade");
    }

    private bool CanAffordUpgrade(int price)
    {
        if (ScoreNGold.InstanceSNG == null)
        {
            Debug.LogError("ScoreNGold instance not found!");
            return false;
        }
        return ScoreNGold.InstanceSNG.GetGold() >= price;
    }
    
    private bool CanUpgradeSpeed()
    {
        if (playerGun == null)
        {
            Debug.LogError("Player gun reference not set!");
            return false;
        }
        return playerGun.CanUpgradeSpeed();
    }
    
    private bool CanUpgradePenetration()
    {
        if (playerGun == null)
        {
            Debug.LogError("Player gun reference not set!");
            return false;
        }
        return playerGun.CanUpgradePenetration();
    }
    
    private bool CanUpgradeFireRate()
    {
        if (playerGun == null)
        {
            Debug.LogError("Player gun reference not set!");
            return false;
        }
        return playerGun.CanUpgradeFireRate();
    }

    private void ProcessPurchase(int cost)
    {
        ScoreNGold.InstanceSNG.AddSnG(0, -cost);
    }

    private void HandlePurchaseFailure(string upgradeType)
    {
        if (!CanAffordUpgrade(GetUpgradePrice(upgradeType)))
            Debug.Log($"Not enough gold to purchase {upgradeType}!");
        else
            Debug.Log($"{upgradeType} is already at maximum level!");
    }
    
    private int GetUpgradePrice(string upgradeType)
    {
        switch (upgradeType)
        {
            case "speed upgrade":
                return currentSpeedPrice;
            case "penetration upgrade":
                return currentPenetrationPrice;
            case "fire rate upgrade":
                return currentFireRatePrice;
            default:
                return 0;
        }
    }

    public void UpdateMarketBtn()
    {
        int currentGold = ScoreNGold.InstanceSNG.GetGold();
        
        speedUpgradeButton.interactable = (currentSpeedPrice <= currentGold) && CanUpgradeSpeed();
        penetrationUpgradeButton.interactable = (currentPenetrationPrice <= currentGold) && CanUpgradePenetration();
        fireRateUpgradeButton.interactable = (currentFireRatePrice <= currentGold) && CanUpgradeFireRate();
    }

    public int GetCurrentSpeedPrice() => currentSpeedPrice;
    public int GetCurrentPenetrationPrice() => currentPenetrationPrice;
    public int GetCurrentFireRatePrice() => currentFireRatePrice;
    
    public bool IsSpeedUpgradeAvailable() => CanUpgradeSpeed() && CanAffordUpgrade(currentSpeedPrice);
    public bool IsPenetrationUpgradeAvailable() => CanUpgradePenetration() && CanAffordUpgrade(currentPenetrationPrice);
    public bool IsFireRateUpgradeAvailable() => CanUpgradeFireRate() && CanAffordUpgrade(currentFireRatePrice);
}