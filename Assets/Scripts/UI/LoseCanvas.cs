using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private Tower _player;
    [SerializeField] private Canvas _canvas;

    private void OnEnable()
    {
        _player.Die += OnPlayerDie;

        _canvas.enabled = false;
    }

    private void OnDisable()
    {
        _player.Die -= OnPlayerDie;
    }

    private void OnPlayerDie()
    {
        _canvas.enabled = true;
    }
}
