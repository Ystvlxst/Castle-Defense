using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTutorial : MonoBehaviour
{
    private const string TutorialComplete = "TutorialComplete";

    [SerializeField] private DronePlatform _dronePlatform;
    [SerializeField] private DroneMovement _droneMovement;
    [SerializeField] private float _distance;

    private void Start()
    {
        if (PlayerPrefs.HasKey(TutorialComplete))
        {
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(StartTutorial());
    }

    private IEnumerator StartTutorial()
    {
        yield return null;
        gameObject.SetActive(true);
        yield return new WaitUntil(() => Vector3.Distance(_dronePlatform.transform.position, _droneMovement.transform.position) > _distance);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt(TutorialComplete, 1);
    }
}
