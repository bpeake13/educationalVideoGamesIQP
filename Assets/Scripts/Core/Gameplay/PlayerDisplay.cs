using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public void UpdateHealthBar(int health)
    {
        healthBar.value = (int)health;
    }

    [SerializeField]
    private Slider healthBar;
}
