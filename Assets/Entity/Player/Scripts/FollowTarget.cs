using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] GameObject followTarget;
    [SerializeField] bool clampToMapBounds;

    private static Vector3 _minBounds;
    private static Vector3 _maxBounds;
    private static float _halfHeight;
    private static float _halfWidth;


    void Update()
    {
        var target = followTarget.transform.position;

        // カメラのビューポートサイズを計算
        _halfHeight = Camera.main.orthographicSize;
        _halfWidth = Camera.main.aspect * _halfHeight;

        if (clampToMapBounds)
        {
            target.x = Mathf.Clamp(target.x, _minBounds.x + _halfWidth, _maxBounds.x - _halfWidth);
            target.y = Mathf.Clamp(target.y, _minBounds.y + _halfHeight, _maxBounds.y - _halfHeight);
        }

        transform.position = new Vector3(target.x, target.y, transform.position.z);
    }


    public static void SetBounds()
    {
        // タイルマップの境界を計算
        Bounds bounds = MapManager._currentGroundMap.localBounds;
        _minBounds = bounds.min;
        _maxBounds = bounds.max;
    }
}
