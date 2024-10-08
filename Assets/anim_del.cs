using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Ensure the object is destroyed after the animation
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength);
    }
}
