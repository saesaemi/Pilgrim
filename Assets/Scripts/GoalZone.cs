using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            var animator = GetComponent<Animator>();
            animator.SetBool("Clear", true);
            GameManager.Instance.OnClearStage(0.3f);
        }
    }
}
