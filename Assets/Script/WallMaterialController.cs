using UnityEngine;

public class WallMaterialController : MonoBehaviour
{
    private Renderer wallRenderer;
    private Material wallMaterial;
    
    private float targetOpacity = 1.0f;
    private float currentOpacity = 1.0f;
    
    // Menggunakan PropertyID lebih efisien daripada string setiap frame
    private static readonly int OpacityProperty = Shader.PropertyToID("_Opacity");

    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        // Gunakan material instance agar tidak mengubah semua tembok sekaligus
        wallMaterial = wallRenderer.material;
    }

    void Update()
    {
        // Transisi halus antara Solid (1) dan Dithered (0.3)
        currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, Time.deltaTime * 8f);
        
        // Mengirim nilai ke Shader Graph
        wallMaterial.SetFloat(OpacityProperty, currentOpacity);
    }

    public void FadeOut(float opacity, float speed) => targetOpacity = opacity;
    public void FadeIn(float speed) => targetOpacity = 1.0f;
}