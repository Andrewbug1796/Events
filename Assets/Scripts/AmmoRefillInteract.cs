using UnityEngine;

public class AmmoRefillInteract : MonoBehaviour
{
    [SerializeField] int ammoToGive = 5;

    FPSController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            player = other.GetComponent<FPSController>();
            player.OnInteractPressed.AddListener(RefillAmmo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            FPSController exitingPlayer = other.GetComponent<FPSController>();
            exitingPlayer.OnInteractPressed.RemoveListener(RefillAmmo);

            if (player == exitingPlayer)
            {
                player = null;
            }
        }
    }

    void RefillAmmo()
    {
        if (player != null)
        {
            player.IncreaseAmmo(ammoToGive);
        }
    }
}