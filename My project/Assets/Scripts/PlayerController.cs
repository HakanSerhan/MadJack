using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public BulletPool bulletPool; 
    public Transform firePoint;   
    public float bulletSpeed = 10f; 

    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private bool isGrounded;
    private bool _facingRight = true;
    private static readonly int PlayerSpeed = Animator.StringToHash("playerSpeed");
    public int playerHealth;
    public bool bullet; 

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        bullet = PlayerPrefs.GetInt("bullet") > 0;
        
        if (bulletPool == null)
        {
            bulletPool = FindObjectOfType<BulletPool>();
            if (bulletPool == null)
            {
                Debug.LogError("BulletPool sahnede bulunamadı!");
            }
        }
        
        var clickPoint = PlayerPrefs.GetInt("clickPoint");

        foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name != clickPoint.ToString()) continue;
            var position = obj.transform.position;
            gameObject.transform.position = position;
            break;
        }
    }

    private void Update()
    {
        Move();
        Jump();
        Shoot();
    }

    private void Move()
    {
        var moveInput = Input.GetAxis("Horizontal");
        var velocity = new Vector2(moveInput * moveSpeed, _rb.velocity.y);
        _rb.velocity = velocity;
        _animator.SetFloat(PlayerSpeed, Mathf.Abs(velocity.x));

        if (moveInput > 0 && !_facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale.x *= -1;
        transform1.localScale = localScale;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.X) && bullet)
        {
            if (bulletPool == null)
            {
                Debug.LogError("BulletPool ayarlanmamış!");
                return;
            }

            GameObject newBullet = bulletPool.GetBullet();
            if (newBullet != null)
            {
                newBullet.transform.position = transform.position;
                newBullet.transform.rotation = transform.rotation;
                
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = _facingRight ? Vector2.right * bulletSpeed : Vector2.left * bulletSpeed;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }

        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("bullet",1);
            bullet = true;
        }

        if (collision.CompareTag("fail"))
        {
            Time.timeScale = 0;
            PlayerPrefs.SetInt("can", 100);
            PlayerPrefs.SetInt("bullet", 0);
            GameManager.Instance.retryPanel.SetActive(true);
        }

        if (collision.CompareTag("enemy"))
        {
            playerHealth -= 10;
            GameManager.Instance.health = playerHealth;
            GameManager.Instance.healthText.text = "Health : " + playerHealth;
            Debug.Log("enemy");

            if (playerHealth <= 0)
            {
                Time.timeScale = 0;
                PlayerPrefs.SetInt("can", 100);
                PlayerPrefs.SetInt("bullet", 0);
                GameManager.Instance.retryPanel.SetActive(true);
            }
        }
    }
}
