using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    public Transform target; // Drag Player (Parent) ke sini
    public float smoothSpeed = 5f; // Kecepatan gerak kamera
    private Vector3 offset; // Jarak tetap antara kamera dan player

    void Start()
    {
        // Menghitung jarak awal antara kamera dan player saat game dimulai
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    // Menggunakan LateUpdate agar kamera bergerak SETELAH player selesai bergerak
    void LateUpdate()
    {
        if (target == null) return;

        // Tentukan posisi tujuan berdasarkan posisi player + jarak awal
        Vector3 targetPosition = target.position + offset;

        // Gerakkan kamera secara halus ke posisi tujuan
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}