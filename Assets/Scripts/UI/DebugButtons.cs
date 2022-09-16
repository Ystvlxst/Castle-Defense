using System.Reflection;
using TMPro;
using UnityEngine;

public class DebugButtons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private StackPresenter _playerStackPresenter;
    [SerializeField] private Detail _detailPrefab;
    
    private WaveSpawner _waveSpawner;

    private void Start() => 
        _waveSpawner = FindObjectOfType<WaveSpawner>();

    private void Update() => 
        UpdateWaveText(AddToWave(0));

    public void AddWave() => 
        AddToWave(1);
    
    public void AddDetail(int count)
    {
        if (count < 0)
        {
            for (int i = 0; i < -count; i++)
            {
                if (_playerStackPresenter.CanRemoveFromStack(_detailPrefab.Type))
                {
                    var removed = _playerStackPresenter.RemoveFromStack(_detailPrefab.Type);
                    Destroy(removed.gameObject);
                }
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                var detail = Instantiate(_detailPrefab, _playerStackPresenter.transform.position, Quaternion.identity);
                _playerStackPresenter.AddToStack(detail);
            }
        }
    }

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