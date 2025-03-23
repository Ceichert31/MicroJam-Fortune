using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickaxeMovement : MonoBehaviour
{
    private SpriteRenderer pickRenderer;
    private Animator animator;
    private RotatePickaxe rotatePickaxe;

    private float agility => GameManager.Instance.Agility;

    private const float PICKAXE_SWING_TIME = 0.8f;

    private void Awake()
    {
        pickRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rotatePickaxe = transform.parent.GetComponent<RotatePickaxe>();
    }
    private void Update()
    {
        animator.speed = Mathf.Clamp(agility, 1, 2);
    }
    public void PickaxeSwing()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameStates.Death) return;

        animator.SetTrigger("Swing");
        rotatePickaxe.TimedFreeze(PICKAXE_SWING_TIME);
    }
}
