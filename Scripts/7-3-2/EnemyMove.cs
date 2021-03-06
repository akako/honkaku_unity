﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
    }

    // CollisionDetectorのonTriggerStayにセットし、衝突判定を受け取るメソッド
    public void OnDetectObject(Collider collider)
    {
        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position; // 自身とプレイヤーの座標差分を計算
            var distance = positionDiff.magnitude; // プレイヤーとの距離を計算
            var direction = positionDiff.normalized; // プレイヤーへの方向
            
            // _raycastHitsに、ヒットしたColliderや座標情報などが格納される
            // RaycastAllとRaycastNonAllocは同等の機能だが、RaycastNonAllocだとメモリにゴミが残らないのでこちらを推奨
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);
            Debug.Log("hitCount: " + hitCount);
            if (hitCount == 0)
            {
                // 本作のプレイヤーはCharacterControllerを使っていて、Colliderは使っていないのでRaycastはヒットしない
                // つまり、ヒット数が0であればプレイヤーとの間に障害物が無いということになる
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                // 見失ったら停止する
                _agent.isStopped = true;
            }
        }
    }
}