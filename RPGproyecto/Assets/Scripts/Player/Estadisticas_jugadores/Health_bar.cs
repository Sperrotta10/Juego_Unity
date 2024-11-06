using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_bar : MonoBehaviour
{
    [SerializeField] private Image barImage;

    public void UpdateHealthbar(float maxHealth, float health){

        barImage.fillAmount = health / maxHealth;
    }
}
