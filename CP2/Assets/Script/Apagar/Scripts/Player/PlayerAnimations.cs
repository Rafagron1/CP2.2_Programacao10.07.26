using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;    
    private PlayerControls playerControls;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = GetComponent<PlayerControls>();
    }

    void Update()
    {
        animator.SetInteger("pMove", (int)playerControls.MoveValue());
        animator.SetInteger("pJump", (int)playerControls.RbVelocity());
    }


}
