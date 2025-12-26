using UnityEngine;
using TMPro;
using System.Collections;

public class NPCM3L2 : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject motivasiUI;
    [TextArea]
    [SerializeField] private string kataMotivasi = "Tetap sabar, tetap kuat. Masalah datang dan pergi, tapi hati yang teguh selalu menang.";

    [SerializeField] private AudioClip healSFX; // tambahan sfx
    private AudioSource audioSource;

    private bool isPlayerInRange = false;
    private bool hasInteracted = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (promptUI != null) promptUI.SetActive(false);

        if (motivasiUI != null)
        {
            canvasGroup = motivasiUI.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        audioSource = GetComponent<AudioSource>(); // audio source
    }

    void Update()
    {
        if (isPlayerInRange && !hasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            hasInteracted = true;
            if (promptUI != null) promptUI.SetActive(false);

            if (motivasiUI != null)
            {
                motivasiUI.GetComponent<TextMeshProUGUI>().text = kataMotivasi;
                StartCoroutine(FadeInOutUI());
            }

            PlayerLevel2 player = GameObject.FindWithTag("Player").GetComponent<PlayerLevel2>();
            if (player != null)
            {
                player.Heal(5); // heal 5 HP

                // play sfx
                if (audioSource != null && healSFX != null)
                {
                    audioSource.PlayOneShot(healSFX);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            isPlayerInRange = true;
            if (promptUI != null) promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (promptUI != null) promptUI.SetActive(false);
        }
    }

    private IEnumerator FadeInOutUI()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            canvasGroup.alpha = t;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(3.5f);

        for (float t = 1f; t > 0; t -= Time.deltaTime)
        {
            canvasGroup.alpha = t;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
