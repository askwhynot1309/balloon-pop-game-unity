using UnityEngine;

public class Balloon2 : MonoBehaviour
{
    public int scoreValue = 20;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        GameManager.instance.AddScore(scoreValue);

        if (animator != null)
        {
            animator.SetTrigger("bluePop");
        }

        Destroy(gameObject, 0.3f);
    }
}
