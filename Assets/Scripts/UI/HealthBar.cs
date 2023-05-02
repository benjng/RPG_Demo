using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int maxHealth)
    {
        // Init slider to max
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        // Set new value to health bar
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue); // depend on the slider value
    }

    public void Damage(int dmg)
    {
        slider.value -= dmg;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
