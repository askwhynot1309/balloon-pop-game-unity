using System;
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
            animator.Play("redPop");
        }
        

        Destroy(gameObject, 0.3f);
    }
}