using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の状態管理スクリプト
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;

    protected override void Start()
    {
        base.Start();
        
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // NavMeshAgentのvelocityで移動速度のベクトルが取得できる
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }

    /// <summary>
    /// 倒された時の消滅コルーチンです。
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}