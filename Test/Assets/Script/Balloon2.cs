using UnityEngine;

public class Balloon2 : MonoBehaviour
{
    public int scoreValue = 20;
    private Animator animator;
    public AstraInputController inputController;
    private bool isHandOver = false;

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
        if (isHandOver)
        {
            PopBalloon();
        }
    }

    private void OnMouseDown()
    {
        PopBalloon();
    }

    private void PopBalloon()
    {
        GameManager.instance.AddScore(scoreValue);

        if (animator != null)
        {
            animator.Play("bluePop");
        }

        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            isHandOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {

            isHandOver = false;
        }
    }
}

