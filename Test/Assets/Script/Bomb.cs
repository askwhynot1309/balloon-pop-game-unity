using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public AstraInputController inputController;
    private Animator animator;
    private bool isFootOver = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (inputController == null)
        {
            inputController = FindObjectOfType<AstraInputController>();
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
        if (isFootOver)
        {
            TriggerBomb();
        }
    }

    private void OnMouseDown()
    {
        TriggerBomb();
    }

    private void TriggerBomb()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (animator != null)
        {
            animator.SetTrigger("BOOM");
        }

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
        }
    }
}
