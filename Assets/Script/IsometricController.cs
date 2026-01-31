using UnityEngine;

public class IsometricController : MonoBehaviour
{
    public float walkSpeed = 5f;
    
    private Vector3 input;
    private Vector3 motion;
    private Quaternion screenRotation;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Rotasi 45 derajat agar input sesuai dengan kamera isometric
        screenRotation = Quaternion.Euler(0, 45, 0); 
    }

    void Update()
    {
        // Mengambil input Raw agar responsif
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        if (input.magnitude > 0.1f)
        {
            // Kalkulasi arah gerak
            motion = screenRotation * input.normalized;
            
            // Gerakkan menggunakan Transform (Bisa diganti RB.MovePosition jika mau collision lebih akurat)
            transform.position += motion * walkSpeed * Time.fixedDeltaTime;
            
            // Rotasi karakter menghadap arah jalan dengan smoothing
            transform.forward = Vector3.Lerp(transform.forward, motion, 0.15f);
        }
    }
}