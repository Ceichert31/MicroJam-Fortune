using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickaxeMovement : MonoBehaviour
{
    private SpriteRenderer pickRenderer;
    private Animator animator;

    private void Awake()
    {
        pickRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void PickaxeSwing()
    {
        animator.SetTrigger("Swing");
    }
}
