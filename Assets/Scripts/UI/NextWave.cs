using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private Button _nextWaveButton;

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
            spawner.AllEnemySpawned += OnAllEnemySpawned;

        _nextWaveButton.onClick.AddListener(OnNextWaveButtonClick);
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
            spawner.AllEnemySpawned -= OnAllEnemySpawned;

        _nextWaveButton.onClick.RemoveListener(OnNextWaveButtonClick);
    }

    public void OnAllEnemySpawned()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    public void OnNextWaveButtonClick()
    {
        _nextWaveButton.gameObject.SetActive(false);
    }
}
