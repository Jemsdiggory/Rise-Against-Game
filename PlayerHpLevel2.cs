using UnityEngine;
using UnityEngine.UI;

public class PlayerHpLevel2 : MonoBehaviour
{
    public Image playerHpBar;

    [HideInInspector] public int currentHP = 50;
    [HideInInspector] public int maxHP = 50;

    // method utk nambah hp
    public void AddHP(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP; // menghindari hp lebih dari max
        UpdateHPUI(); // update ui hp after penambahan
    }

    // update ui hp
    public void UpdateHPUI()
    {
        if (playerHpBar != null)
        {
            playerHpBar.fillAmount = (float)currentHP / maxHP;
        }
    }
}
