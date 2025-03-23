using UnityEngine;

public class Mine : MonoBehaviour
{
    private Animator mineAnimator;

    private void Start()
    {
        mineAnimator = GetComponentInParent<Animator>();
    }

   
}
