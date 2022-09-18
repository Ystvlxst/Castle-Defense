using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTutorial : MonoBehaviour
{
    private const string TutorialComplete = "TutorialComplete";

    [SerializeField] private DronePlatform _dronePlatform;
    [SerializeField] private DroneMovement _droneMovement;
    [SerializeField] private float _platformDistance;
    [SerializeField] private float _droneDistance;
    [SerializeField] private Canvas _droneCanvas;
    [SerializeField] private Canvas _platformCanvas;

    private void Start()
    {
        if (PlayerPrefs.HasKey(TutorialComplete))
        {
            _platformCanvas.enabled = false;
            _droneCanvas.enabled = false;
            return;
        }

        StartCoroutine(StartTutorial());
    }

    private IEnumerator StartTutorial()
    {
        yield return null;
        _platformCanvas.enabled = true;
        _droneCanvas.enabled = false;
        yield return new WaitUntil(() => Vector3.Distance(_dronePlatform.transform.position, _droneMovement.transform.position) > _droneDistance);
        _droneCanvas.enabled = true;
        yield return new WaitUntil(() => Vector3.Distance(_dronePlatform.transform.position, _droneMovement.transform.position) > _platformDistance);
        _platformCanvas.enabled = false;
        yield return new WaitUntil(() => _droneMovement.IsFirstMovementStoped == true);
        _droneCanvas.enabled = false;
        PlayerPrefs.SetInt(TutorialComplete, 1);
    }
}
