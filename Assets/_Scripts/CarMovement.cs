using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float minSpeed = 1f;      // Minimum ileri h�z
    public float maxSpeed = 6f;     // Maksimum ileri h�z
    public float acceleration = 4f;  // H�zlanma ve yava�lama oran�
    public float moveSpeed = 3f;     // Ba�lang�� sa�-sol hareket h�z�
    public float horizontalSpeed = 3f;     // Ba�lang�� sa�-sol hareket h�z�
    public float defaultMoveSpeed = 3f; // Sa�-sol hareket h�z� i�in varsay�lan de�er
    public float slowMoveSpeed = 3f; // Sa�-sol hareket h�z�, input verilmedi�inde d��ece�i h�z
    public float moveSpeedChangeRate = 2f; // Hareket h�z�n�n yava� yava� d��me oran�
    public float timeToSlowDown = 3f; // Input verilmezse yava�lama s�resi (saniye)
    public Rigidbody2D rb;            // Araban�n Rigidbody'si

    private float currentSpeed;       // Mevcut ileri h�z
    private Vector2 movement;
    private float lastInputTime;      // Son input zaman�

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        currentSpeed = defaultMoveSpeed;      // Ba�lang��ta minimum h�zda ba�la
        lastInputTime = Time.time;    // Ba�lang��ta input zaman� �u an
    }

    void Update()
    {
        // �leri ve geri hareket kontrol� (W ile h�zlan, S ile yava�la)
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Max(currentSpeed - acceleration * Time.deltaTime, minSpeed);
        }

        // Sa� ve sola hareket kontrol� (A ve D tu�lar� ile)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // E�er input varsa son input zaman�n� g�ncelle
        if (moveHorizontal != 0 || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            lastInputTime = Time.time;
            moveSpeed = defaultMoveSpeed; // Input al�nca varsay�lan h�za geri d�n
        }
        else if (Time.time - lastInputTime >= timeToSlowDown)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, defaultMoveSpeed, moveSpeedChangeRate * Time.deltaTime);
        }

        // Hareket vekt�r�n� olu�tur
        movement = new Vector2(moveHorizontal * horizontalSpeed, currentSpeed);

    }

    void FixedUpdate()
    {
        // Rigidbody ile arac� hareket ettir
        rb.velocity = movement;
        transform.rotation=Quaternion.Euler(0,0,0);
    }
}
