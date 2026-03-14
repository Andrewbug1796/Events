using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float healAmount = 25f;
    [SerializeField] bool destroyOnUse = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered HealthCube trigger: " + other.name);

        FPSController player = other.GetComponentInParent<FPSController>();

        if (player != null)
        {
            PlayerHUD hud = FindFirstObjectByType<PlayerHUD>();

            if (hud != null)
            {
                hud.HealPlayer(healAmount);
                Debug.Log("Player healed by " + healAmount);
            }

            if (destroyOnUse)
            {
                Destroy(gameObject);
            }
        }
    }
}