using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public AstraInputController inputController;
    private Animator animator;
    private bool isFootOver = false;
    private bool hasClicked = false;
    private bool isPopped = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (inputController == null)
        {
            inputController = FindFirstObjectByType<AstraInputController>();
        }

        if (inputController != null)
        {
            inputController.OnClickEvent.AddListener(HandleClick);
        }
    }

    private void OnDestroy()
    {
        if (inputController != null)
        {
            inputController.OnClickEvent.RemoveListener(HandleClick);
        }
    }

    private void HandleClick()
    {
        if (isFootOver && !hasClicked && !isPopped)
        {
            hasClicked = true;
            TriggerBomb();
        }
    }

    private void OnMouseDown()
    {
        TriggerBomb();
    }

    private void TriggerBomb()
    {
        if (isPopped) return;
        isPopped = true;
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (animator != null)
        {
            animator.SetTrigger("BOOM");
        }
        SoundManager.Instance.PlayBombExplosion();
        GameManager.instance.GameOver();

        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Foot"))
        {
            isFootOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Foot"))
        {
            isFootOver = false;
            hasClicked = false;
        }
    }
}
