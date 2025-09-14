using UnityEngine;
using System.Collections.Generic;

public enum TimeLayerType { Global, Player, Enemy, Projectile, UI }

[DisallowMultipleComponent]
[AddComponentMenu("Time System/Time Director")]
public sealed class TimeDirector : MonoBehaviour
{
    public static TimeDirector I { get; private set; }

    [Header("Global Time Settings")]
    [Tooltip("게임 전체 기본 시간 배율(느린 상태).")]
    [Range(0.05f, 1.5f)]
    public float globalScale = 0.3f;

    private readonly Dictionary<TimeLayerType, float> _layer = new()
    {
        { TimeLayerType.Global, 1f },
        { TimeLayerType.Player, 1f },
        { TimeLayerType.Enemy, 1f },
        { TimeLayerType.Projectile, 1f },
        { TimeLayerType.UI, 1f }
    };

    private float _burstTimer;
    private float _burstTarget = 1f;
    private float _lastAppliedScale = -1f;

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (I == this) I = null;
    }

    private void Update()
    {
        bool inBurst = _burstTimer > 0f;
        float g = Mathf.Clamp(inBurst ? _burstTarget : globalScale, 0.05f, 1.5f);

        // 값 변동시에만 반영해서 불필요한 재할당 방지
        if (!Mathf.Approximately(g, _lastAppliedScale))
        {
            Time.timeScale = g;
            _lastAppliedScale = g;
        }

        if (inBurst)
        {
            _burstTimer -= Time.unscaledDeltaTime;
            if (_burstTimer <= 0f) _burstTimer = 0f;
        }
    }

    /// <summary>패링/저스트 회피 등에서 호출: target배율로 dur초 동안 가속</summary>
    public void Burst(float target = 1.0f, float dur = 0.5f)
    {
        _burstTarget = Mathf.Clamp(target, 0.05f, 1.5f);
        _burstTimer = Mathf.Max(_burstTimer, Mathf.Max(0f, dur));
    }

    /// <summary>레이어별 커스텀 Δ (이동/로직에 사용)</summary>
    public float Delta(TimeLayerType layer)
    {
        float layerScale = _layer.TryGetValue(layer, out var s) ? s : 1f;
        float baseScale = (_burstTimer > 0f ? _burstTarget : globalScale);
        return Time.unscaledDeltaTime * baseScale * layerScale;
    }

    /// <summary>특정 레이어 시간 배율 설정(0~2)</summary>
    public void SetLayer(TimeLayerType type, float scale)
    {
        _layer[type] = Mathf.Clamp(scale, 0.0f, 2f);
    }

    public float GetLayer(TimeLayerType type)
    {
        return _layer.TryGetValue(type, out var s) ? s : 1f;
    }
}
