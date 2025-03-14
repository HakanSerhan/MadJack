using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float minSpeed = 1f;      // Minimum ileri hýz
    public float maxSpeed = 6f;     // Maksimum ileri hýz
    public float acceleration = 4f;  // Hýzlanma ve yavaþlama oraný
    public float moveSpeed = 3f;     // Baþlangýç sað-sol hareket hýzý
    public float horizontalSpeed = 3f;     // Baþlangýç sað-sol hareket hýzý
    public float defaultMoveSpeed = 3f; // Sað-sol hareket hýzý için varsayýlan deðer
    public float slowMoveSpeed = 3f; // Sað-sol hareket hýzý, input verilmediðinde düþeceði hýz
    public float moveSpeedChangeRate = 2f; // Hareket hýzýnýn yavaþ yavaþ düþme oraný
    public float timeToSlowDown = 3f; // Input verilmezse yavaþlama süresi (saniye)
    public Rigidbody2D rb;            // Arabanýn Rigidbody'si

    private float currentSpeed;       // Mevcut ileri hýz
    private Vector2 movement;
    private float lastInputTime;      // Son input zamaný

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        currentSpeed = defaultMoveSpeed;      // Baþlangýçta minimum hýzda baþla
        lastInputTime = Time.time;    // Baþlangýçta input zamaný þu an
    }

    void Update()
    {
        // Ýleri ve geri hareket kontrolü (W ile hýzlan, S ile yavaþla)
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Max(currentSpeed - acceleration * Time.deltaTime, minSpeed);
        }

        // Sað ve sola hareket kontrolü (A ve D tuþlarý ile)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Eðer input varsa son input zamanýný güncelle
        if (moveHorizontal != 0 || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            lastInputTime = Time.time;
            moveSpeed = defaultMoveSpeed; // Input alýnca varsayýlan hýza geri dön
        }
        else if (Time.time - lastInputTime >= timeToSlowDown)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, defaultMoveSpeed, moveSpeedChangeRate * Time.deltaTime);
        }

        // Hareket vektörünü oluþtur
        movement = new Vector2(moveHorizontal * horizontalSpeed, currentSpeed);

    }

    void FixedUpdate()
    {
        // Rigidbody ile aracý hareket ettir
        rb.velocity = movement;
        transform.rotation=Quaternion.Euler(0,0,0);
    }
}
