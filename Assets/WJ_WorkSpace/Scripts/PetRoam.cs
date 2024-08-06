using UnityEngine;
using UnityEngine.AI;

public class PetRoam : MonoBehaviour
{
    public float roamRadius = 10f; // 펫이 돌아다닐 반경
    public float roamDelay = 5f;   // 각 이동 간의 지연 시간

    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 startingPosition;
    private bool isFollowingPlayer = false; // 플레이어를 바라보는지 여부

    public Transform playerTransform; // 플레이어의 Transform

    private Vector3 fixedPosition; // 펫이 고정될 위치

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
        InvokeRepeating("Roam", 0f, roamDelay);
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            agent.isStopped = true; // 플레이어를 따라갈 때 에이전트 멈춤
            transform.LookAt(playerTransform);
            transform.position = fixedPosition; // 위치 고정
            animator.SetFloat("Speed", 0); // 멈췄을 때 애니메이션 속도 0으로 설정
        }
        else
        {
            agent.isStopped = false; // 에이전트가 돌아다니도록 설정
            animator.SetFloat("Speed", agent.velocity.magnitude); // 에이전트 속도에 따라 애니메이션 업데이트
        }
    }

    void Roam()
    {
        if (!isFollowingPlayer)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += startingPosition;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1))
            {
                Vector3 finalPosition = hit.position;
                agent.SetDestination(finalPosition);
            }
        }
    }

    public void SetFollowingPlayer(bool isFollowing)
    {
        isFollowingPlayer = isFollowing;
        if (isFollowingPlayer)
        {
            fixedPosition = transform.position; // 현재 위치를 고정
            agent.isStopped = true; // 플레이어를 따라갈 때 에이전트 완전히 멈춤
        }
        else
        {
            agent.isStopped = false; // 따라가지 않을 때는 돌아다님
            Roam(); // 새로운 목적지를 즉시 설정
        }
    }
}
