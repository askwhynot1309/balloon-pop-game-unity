using UnityEngine;

public class Balloon2 : MonoBehaviour
{
    public int scoreValue = 20;
    private Animator animator;
    public AstraInputController inputController;
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
            PopBalloon();
        }
    }

    private void OnMouseDown()
    {
        if (!isPopped)
        {
            PopBalloon();
        }
    }

    private void PopBalloon()
    {
        if (isPopped) return;
        isPopped = true;
        GameManager.instance.AddScore(scoreValue);

        if (animator != null)
        {
            animator.Play("bluePop");
        }
        SoundManager.Instance.PlayBalloonPop();
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

