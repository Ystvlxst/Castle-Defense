using System.Reflection;
using TMPro;
using UnityEngine;

public class DebugButtons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    
    private WaveSpawner _waveSpawner;

    private void Start() => 
        _waveSpawner = FindObjectOfType<WaveSpawner>();

    private void Update() => 
        UpdateWaveText(AddToWave(0));

    public void AddWave() => 
        AddToWave(1);

    public void DecreaseWave() => 
        AddToWave(-1);

    public void SetTimeScale(float timeScale) => 
        Time.timeScale = timeScale;

    private void UpdateWaveText(int newValue) => 
        _waveText.text = "Wave: " + newValue;

    private int AddToWave(int i)
    {
        FieldInfo wave = _waveSpawner.GetType().GetField("_wave", BindingFlags.NonPublic | BindingFlags.Instance);
        int newValue = (int) wave.GetValue(_waveSpawner) + i;
        wave.SetValue(_waveSpawner, newValue);

        return newValue;
    }
}