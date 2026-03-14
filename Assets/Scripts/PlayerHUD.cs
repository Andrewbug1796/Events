using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text maxAmmoText;
    [SerializeField] Image damageFlashImage;
    [SerializeField] TMP_Text healthText;

    FPSController player;
    Gun gun;

    float maxHealth = 100f;
    float currentHealth = 100f;

    void Start()
    {
        player = FindFirstObjectByType<FPSController>();
        gun = FindFirstObjectByType<Gun>();

        if (gun != null)
        {
            gun.OnAmmoChanged.AddListener(UpdateAmmoUI);
            maxAmmoText.text = gun.GetMaxAmmo().ToString();
            currentAmmoText.text = gun.GetMaxAmmo().ToString();
        }

        if (player != null)
        {
            player.OnPlayerDamaged.AddListener(OnPlayerDamaged);
        }

        if (damageFlashImage != null)
        {
            Color c = damageFlashImage.color;
            c.a = 0f;
            damageFlashImage.color = c;
        }

        UpdateHealthUI();
    }

    void UpdateAmmoUI(int ammoAmount)
    {
        currentAmmoText.text = ammoAmount.ToString();
    }

    void OnPlayerDamaged()
    {
        currentHealth -= 10f;

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }

        UpdateHealthUI();

        StopAllCoroutines();
        StartCoroutine(FlashDamage());
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        }
    }

    IEnumerator FlashDamage()
    {
        if (damageFlashImage == null)
        {
            yield break;
        }

        Color c = damageFlashImage.color;
        c.a = 0.4f;
        damageFlashImage.color = c;

        yield return new WaitForSeconds(0.15f);

        c.a = 0f;
        damageFlashImage.color = c;
    }
}