using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFormManager manager = other.GetComponent<PlayerFormManager>();
            if (manager != null)
            {
                // 1. Kembalikan bentuk ke Statis (istirahat dari topeng)
                if (manager.currentState != PlayerFormManager.PlayerState.Static)
                {
                    manager.SetState(PlayerFormManager.PlayerState.Static);
                }

                // 2. Isi ulang energi
                manager.RechargeEnergy();
            }
        }
    }
}