using UnityEngine;

public class FormTriggerArea : MonoBehaviour
{
    [Header("Configuration")]
    public PlayerFormManager.PlayerState targetState; // Pilih: OfficeCube atau HomeCapsule

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFormManager manager = other.GetComponent<PlayerFormManager>();
            if (manager != null)
            {
                manager.SetState(targetState);
                // Debug log untuk memastikan trigger bekerja
                // Debug.Log("Entered Mask Zone: " + targetState);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFormManager manager = other.GetComponent<PlayerFormManager>();
            // Cek agar saat Humanoid (Ending), dia tidak berubah jadi statis lagi
            if (manager != null && manager.currentState != PlayerFormManager.PlayerState.Humanoid)
            {
                // Kembali jadi Statis (Numb) saat keluar dari zona tanggung jawab
                manager.SetState(PlayerFormManager.PlayerState.Static);
            }
        }
    }
}