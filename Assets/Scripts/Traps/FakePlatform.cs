using UnityEngine;

// 겉보기엔 발판처럼 보이지만 통과됨 (Level Devil 핵심 트롤 요소)
public class FakePlatform : MonoBehaviour
{
    private void Start()
    {
        // 콜라이더 비활성화로 그냥 떨어짐
        GetComponent<Collider2D>().enabled = false;
    }
}
