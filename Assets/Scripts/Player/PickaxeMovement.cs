using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickaxeMovement : MonoBehaviour
{
    private SpriteRenderer pickRenderer;
    private Animator animator;
    private RotatePickaxe rotatePickaxe;

    private const float PICKAXE_SWING_TIME = 0.6f;

    private void Awake()
    {
        pickRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rotatePickaxe = transform.parent.GetComponent<RotatePickaxe>();
    }

    public void PickaxeSwing()
    {
        animator.SetTrigger("Swing");
        rotatePickaxe.TimedFreeze(PICKAXE_SWING_TIME);
    }
}
