using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public GameObject bulletPrefab;
    public Transform firePoint;
    private InputAction shootAction;
    public int maxAmmo = 3;
    private int currentAmmo;
    public TextMeshProUGUI ammoText;

    public float reloadTime = 3f;
    private bool isReloading = false;

    public Image[] hearts;


    public TextMeshProUGUI scoreText;
    public int score;

    public int maxHP = 3;
    private int currentHP;

    public bool isDashing = false;
    private InputAction dashAction;

    int jumpCount = 0;
    int maxJump = 2;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private bool isOnGround = true;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        if (playerAudio == null)
        {
            playerAudio = gameObject.AddComponent<AudioSource>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;

        Physics.gravity *= gravityModifier;

        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        dashAction = InputSystem.actions.FindAction("Sprint");

        jumpAction = InputSystem.actions.FindAction("Jump");

        shootAction = InputSystem.actions.FindAction("Fire");

        currentHP = maxHP;
        UpdateHPUI();

        gameOver = false;

    }

    // Update is called once per frame
    void Update()
    {
        isDashing = dashAction.IsInProgress();


        if (jumpAction.triggered && jumpCount < maxJump && !gameOver)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            jumpCount++;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();

            if (playerAudio != null && jumpSfx != null)
            {
                playerAudio.PlayOneShot(jumpSfx);
            }
        }

        if (shootAction.triggered && !gameOver && !isReloading)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                UpdateAmmoUI();

                if (currentAmmo <= 0)
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        float timer = reloadTime;

        while (timer > 0)
        {
            ammoText.text = "Reloading: " + timer.ToString("F1");
            timer -= Time.deltaTime;
            yield return null;
        }

        currentAmmo = maxAmmo;
        isReloading = false;

        UpdateAmmoUI();
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            currentHP--;
            UpdateHPUI();

            explosionParticle.Play();
            if (playerAudio != null && crashSfx != null)
            {
                playerAudio.PlayOneShot(crashSfx);
            }
            Destroy(collision.gameObject);
            if (currentHP <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
            }
        }

    }
    void UpdateHPUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHP;
        }
    }
    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
    }

}