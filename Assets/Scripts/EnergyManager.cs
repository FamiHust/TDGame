using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;
    public Slider energyBar;
    private float targetEnergy;
    private int currentEnergy;
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private float smoothSpeed = 0.1f;
    public GameObject Power;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentEnergy = 0;
        targetEnergy = 0;
        UpdateEnergyBarInstant();
    }

    private void Update()
    {
        energyBar.value = Mathf.Lerp(energyBar.value, targetEnergy / maxEnergy, smoothSpeed);
    }

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy == maxEnergy)
        {
            currentEnergy = maxEnergy;
            Power.SetActive(true);
            SoundManager.PlaySound(SoundType.MERGE);
        }
        targetEnergy = currentEnergy;
    }

    public bool ConsumeEnergy(int amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            targetEnergy = currentEnergy;
            return true;
        }

        return false;
    }

    private void UpdateEnergyBarInstant()
    {
        energyBar.value = (float)currentEnergy / maxEnergy;
    }

    public bool IsEnergyFull()
    {
        return currentEnergy == maxEnergy;
    }

    public void SoundPower()
    {
        SoundManager.PlaySound(SoundType.MERGE);
    }
}
