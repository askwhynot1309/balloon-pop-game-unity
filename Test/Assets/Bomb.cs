using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (animator != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("BOOM");
            }
        }

        GameManager.instance.GameOver();

        // Destroy the bomb
        Destroy(gameObject, 0.3f);
    }
}
