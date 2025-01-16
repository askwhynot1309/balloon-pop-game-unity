using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int scoreValue = 10;
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
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("redPop");
            }
        }

        Destroy(gameObject, 0.3f);
    }
}