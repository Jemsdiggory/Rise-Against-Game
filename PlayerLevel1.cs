using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLevel1 : MonoBehaviour
{
    [Header("HP Settings")]
    public float playerHp = 50f;
    public float maxHp = 50f;
    public float damageCooldown = 1.5f;
    private float lastDamageTime = -999f;
    private bool isDead = false;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 velocity;

    [Header("Animation")]
    public Animator animator;

    [Header("Scene Settings")]
    public string gameOverSceneName = "Game Over Scene";

    [Header("UI Settings")]
    public PlayerHpLevel1 hpUI;
    private Image hpBarImage;

    [Header("Audio Settings")]
    public SoundEffectLibrary soundLibrary;
    private AudioSource audioSource;
    private float footstepTimer = 0f;
    public float footstepInterval = 0.5f;

    void Start()
    {
        playerHp = maxHp;
        isDead = false;

        PlayerHpLevel1 hpLevel1 = GetComponent<PlayerHpLevel1>();
        if (hpLevel1 != null)
        {
            hpBarImage = hpLevel1.playerHpBar;
        }

        audioSource = GetComponent<AudioSource>();
        UpdateHPBar();
        Debug.Log("[Start] Player initialized");
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(playerHp + 1f);
        }

        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
        velocity.Normalize();

        UpdateAnimation();
        HandleFootsteps();
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb.MovePosition(rb.position + velocity * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        bool walking = velocity != Vector2.zero;
        animator.SetBool("Walking", walking);
        if (walking)
        {
            animator.SetFloat("Horizontal", velocity.x);
            animator.SetFloat("Vertical", velocity.y);
        }
    }

    void HandleFootsteps()
    {
        if (velocity != Vector2.zero)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                footstepTimer = 0f;
                AudioClip clip = soundLibrary.GetRandomClip("FootSteps");
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }
        }
        else
        {
            footstepTimer = footstepInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy") && Time.time >= lastDamageTime + damageCooldown)
        {
            Debug.Log("[OnTriggerEnter2D] Player terkena musuh, memanggil TakeDamage(5)");
            TakeDamage(5f);
            lastDamageTime = Time.time;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        playerHp -= amount;
        playerHp = Mathf.Clamp(playerHp, 0f, maxHp);
        Debug.Log($"[TakeDamage] Player took {amount} damage. Current HP: {playerHp}");

        StartCoroutine(FlashRed());
        UpdateHPBar();

        if (playerHp <= 0f)
        {
            playerHp = 0f;
            Die();
        }
    }

    void Die()  
    {
        if (isDead) return;
        Debug.Log("[Die] Player mati, mengaktifkan Game Over");

        isDead = true;
        animator.SetBool("Walking", false);
        animator.SetTrigger("Die");
        Invoke(nameof(GameOver), 1f);
    }

    void GameOver()  //kalo mati, ke scene game over
    {
        if (Application.CanStreamedLevelBeLoaded(gameOverSceneName))
        {
            Debug.Log("[GameOver] Memuat scene Game Over...");
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            Debug.LogError("[GameOver] Scene Game Over tidak ditemukan di Build Settings!");
        }
    }

    IEnumerator FlashRed() //kedip merah pas kena hit
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        playerHp += amount;
        playerHp = Mathf.Clamp(playerHp, 0f, maxHp);

        Debug.Log($"[Heal] Player healed {amount}. Current HP: {playerHp}");
        UpdateHPBar();
    }

    void UpdateHPBar()  //update hp bar
    {
        if (hpBarImage != null)
        {
            hpBarImage.fillAmount = playerHp / maxHp;
        }
    }
}
