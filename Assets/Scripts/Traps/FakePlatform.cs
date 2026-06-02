using UnityEngine;

// 겉보기엔 발판처럼 보이지만 통과됨 (Level Devil 핵심 트롤 요소)
public class FakePlatform : TrapBase
{
    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
