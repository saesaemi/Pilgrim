using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
}
