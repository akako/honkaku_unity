using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private static LifeGaugeContainer _instance;

    [SerializeField] private Camera mainCamera; // ライフゲージ表示対象のMobを映しているカメラ
    [SerializeField] private LifeGauge lifeGaugePrefab; // ライフゲージのPrefab

    private RectTransform rectTransform;
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap = new Dictionary<MobStatus, LifeGauge>(); // アクティブなライフゲージを保持するコンテナ

    private void Awake()
    {
        // シーン上に1つしか存在させないスクリプトのため、このような疑似シングルトンが成り立つ
        if (null != _instance) throw new Exception("LifeBarContainer instance already exists.");
        _instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ライフゲージを追加します。
    /// </summary>
    /// <param name="status"></param>
    public void Add(MobStatus status)
    {
        var lifeGauge = Instantiate(lifeGaugePrefab, transform);
        lifeGauge.Initialize(rectTransform, mainCamera, status);
        _statusLifeBarMap.Add(status, lifeGauge);
    }

    /// <summary>
    /// ライフゲージを破棄します。
    /// </summary>
    /// <param name="status"></param>
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        _statusLifeBarMap.Remove(status);
    }
}