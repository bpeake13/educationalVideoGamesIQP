using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{

    void Update()
    {
        float healthPercent = (float)enemy.Health / (float)enemy.MaxHealth;

        healthImage.fillAmount = healthPercent;
    }

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private Image healthImage;
}
