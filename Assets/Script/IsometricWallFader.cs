using UnityEngine;
using System.Collections.Generic;

public class IsometricWallFader : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player; // Drag object Player ke sini
    public LayerMask wallLayer; // Pilih layer "Wall" di Inspector

    [Header("Fade Settings")]
    [Range(0f, 1f)]
    public float transparentAlpha = 0.3f; // Tingkat transparansi (0 = hilang, 1 = solid)
    public float fadeSpeed = 5f;

    private List<WallMaterialController> hiddenWalls = new List<WallMaterialController>();

    void Update()
    {
        if (player == null) return;

        // 1. Hitung arah dari kamera ke pemain
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // 2. Tembakkan Raycast
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, wallLayer);

        // Kumpulkan semua tembok yang terkena hit
        HashSet<WallMaterialController> currentHits = new HashSet<WallMaterialController>();

        foreach (var hit in hits)
        {
            var controller = hit.collider.GetComponent<WallMaterialController>();
            if (controller != null)
            {
                currentHits.Add(controller);
                controller.FadeOut(transparentAlpha, fadeSpeed);
            }
        }

        // 3. Kembalikan tembok yang sudah tidak menghalangi menjadi solid
        for (int i = hiddenWalls.Count - 1; i >= 0; i--)
        {
            if (!currentHits.Contains(hiddenWalls[i]))
            {
                hiddenWalls[i].FadeIn(fadeSpeed);
                hiddenWalls.RemoveAt(i);
            }
        }

        // Tambahkan hits baru ke daftar pantauan
        foreach (var wall in currentHits)
        {
            if (!hiddenWalls.Contains(wall)) hiddenWalls.Add(wall);
        }
    }
}