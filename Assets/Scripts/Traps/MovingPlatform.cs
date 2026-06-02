using UnityEngine;

// 두 지점 사이를 왕복하는 발판
public class MovingPlatform : TrapBase
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Transform target;

    private void Start()
    {
        target = pointB;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
            target = target == pointA ? pointB : pointA;
    }

    // 플레이어가 올라서면 자식으로 붙여서 함께 이동
    protected override void OnPlayerCollisionEnter(PlayerController player)
        => player.transform.SetParent(transform);

    protected override void OnPlayerCollisionExit(PlayerController player)
        => player.transform.SetParent(null);
}
