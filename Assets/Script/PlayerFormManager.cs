using UnityEngine;

public class PlayerFormManager : MonoBehaviour
{
    public enum PlayerState { Static, OfficeCube, HomeCapsule, Humanoid }
    
    [Header("Current State")]
    public PlayerState currentState = PlayerState.Static;

    [Header("Models")]
    public GameObject staticModel;   
    public GameObject cubeModel;     
    public GameObject capsuleModel; 
    public GameObject humanoidModel; 

    [Header("Settings")]
    public float energy = 100f;
    public float maxEnergy = 100f;
    public float burnoutRate = 5f; 
    public float rechargeRate = 10f; // Kecepatan isi energi di safe zone

    [Header("Visual Filter Settings")]
    public Color dimWorldColor = Color.gray;   // Warna dunia saat Statis (Numb)
    public Color brightWorldColor = Color.white; // Warna dunia saat Masked (Perfect)
    public bool useFog = true; // Opsi pakai kabut untuk efek dramatis

    private IsometricController moveScript;

    void Start()
    {
        moveScript = GetComponent<IsometricController>();
        UpdateForm();
    }

    void Update()
    {
        // LOGIKA ENERGI
        // Energi HANYA berkurang di fase "OfficeCube" atau "HomeCapsule"
        // Fase "Humanoid" tidak mengurangi energi (Acceptance)
        if (currentState == PlayerState.OfficeCube || currentState == PlayerState.HomeCapsule)
        {
            energy -= burnoutRate * Time.deltaTime;
            
            // Jika energi habis, paksa burnout kembali ke statis
            if (energy <= 0)
            {
                energy = 0;
                SetState(PlayerState.Static); 
                Debug.Log("Burnout! Forced to Static state.");
            }
        }
    }

    // Fungsi untuk mengubah bentuk
    public void SetState(PlayerState newState)
    {
        if (currentState == newState) return; 
        currentState = newState;
        UpdateForm();
    }

    // Fungsi untuk mengisi ulang energi (Dipanggil oleh SafeZone)
    public void RechargeEnergy()
    {
        energy += rechargeRate * Time.deltaTime;
        if (energy > maxEnergy) energy = maxEnergy;
    }

    void UpdateForm()
    {
        // 1. Reset Model
        staticModel.SetActive(false);
        cubeModel.SetActive(false);
        capsuleModel.SetActive(false);
        humanoidModel.SetActive(false);

        // 2. Aktifkan Model & Atur Fisik serta Visual Dunia
        switch (currentState)
        {
            case PlayerState.Static:
                staticModel.SetActive(true);
                moveScript.walkSpeed = 7f; // Ringan
                SetWorldVisuals(dimWorldColor, 0.05f); // Dunia Gelap/Berkabut
                break;

            case PlayerState.OfficeCube:
                cubeModel.SetActive(true);
                moveScript.walkSpeed = 4f; // Berat
                SetWorldVisuals(brightWorldColor, 0f); // Dunia Terang/Jelas
                break;

            case PlayerState.HomeCapsule:
                capsuleModel.SetActive(true);
                moveScript.walkSpeed = 4f; // Berat
                SetWorldVisuals(brightWorldColor, 0f); // Dunia Terang/Jelas
                break;

            case PlayerState.Humanoid:
                humanoidModel.SetActive(true);
                moveScript.walkSpeed = 6f; // Seimbang
                SetWorldVisuals(brightWorldColor, 0f); // Dunia Indah Permanen
                break;
        }
    }

    // Fungsi mengubah atmosfer dunia (Filtered Reality)
    void SetWorldVisuals(Color ambientColor, float fogDensity)
    {
        RenderSettings.ambientLight = ambientColor;
        if (useFog)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = ambientColor;
            RenderSettings.fogDensity = fogDensity;
        }
    }
}