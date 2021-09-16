using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private Image image;

    private void Start() {
        image = GetComponent<Image>();
    }

    private void Update() {
        image.fillAmount = unit.currentHP / unit.maxHP;
    }
}
