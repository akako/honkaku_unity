using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
    }

    private void Update()
    {
        _agent.destination = _playerController.transform.position; // クエリちゃんを目指して進む
    }
}