using System.Collections;
using UnityEngine;

// 플레이어가 올라서면 잠시 후 사라졌다가 복원
public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private float disappearDelay = 0.5f;
    [SerializeField] private float respawnDelay = 2f;

    private Collider2D col;
    private SpriteRenderer sr;
    private bool isTriggered;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isTriggered) return;
        if (other.gameObject.GetComponent<PlayerController>() == null) return;

        isTriggered = true;
        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        // 깜빡임으로 경고
        for (int i = 0; i < 3; i++)
        {
            sr.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        col.enabled = false;
        sr.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        col.enabled = true;
        sr.enabled = true;
        sr.color = Color.white;
        isTriggered = false;
    }
}
