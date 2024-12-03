using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    //private TextMeshProUGUI _healthText;

    private HealthComponent _healthComponent;

    [SerializeField] private GameObject _playerObject;

    [SerializeField] private Image[] _healthHearts;

    private void Awake()
    {
        _healthComponent = _playerObject.GetComponent<HealthComponent>();

    }

    public void UpdateHeathUI()
    {
        for(int i = 0; i < _healthHearts.Length; i++)
        {
            _healthHearts[i].enabled = i < _healthComponent.CurrentHealth; // Enable only as many sprites as there is current health (remove hearts when the player loses a health point)
        }
    }
}
