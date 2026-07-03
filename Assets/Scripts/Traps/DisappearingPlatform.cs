using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 플레이어가 올라서면 잠시 후 사라졌다가 복원
public class DisappearingPlatform : TrapBase
{
    [SerializeField] private float respawnDelay = 2f;

    private Collider2D col;
    private Image image;
    private bool isTriggered;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        image = GetComponent<Image>();
    }

    protected override void OnPlayerCollisionEnter(PlayerController player)
    {
        if (isTriggered) return;
        isTriggered = true;
        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            image.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(0.1f);
            image.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        col.enabled = false;
        image.enabled  = false;

        yield return new WaitForSeconds(respawnDelay);

        col.enabled = true;
        image.enabled = true;
        image.color = Color.black;
        isTriggered = false;
    }
}
