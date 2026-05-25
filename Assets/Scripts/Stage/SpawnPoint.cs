using UnityEngine;

// 각 스테이지 프리팹 안에 배치 — 플레이어 시작 위치
public class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        Gizmos.DrawIcon(transform.position, "sv_icon_dot0_pix16_gizmo", true);
    }
}
