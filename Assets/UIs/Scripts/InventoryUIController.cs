using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] CanvasGroup ui;
    [SerializeField] float aMax, aMin;
    [SerializeField] float size;

    public RectTransform imageRectTransform; // ImageのRectTransform
    public Transform target; // ゲームオブジェクトのTransform

    void Update()
    {
        // ImageのRectTransformの4隅のスクリーン座標を取得
        Vector3[] corners = new Vector3[4];
        imageRectTransform.GetWorldCorners(corners); //左下から時計回り

        corners = corners.Select(x => Camera.main.ScreenToWorldPoint(x)).Select(x => new Vector3(x.x, x.y, 0)).ToArray();

        var minx = corners[0].x;
        var miny = corners[0].y;
        var maxx = corners[2].x;
        var maxy = corners[2].y;

        var xdiff = float.MaxValue;
        var ydiff = float.MaxValue;

        var distance = float.MaxValue;
        for (int i = 0; i < 4; i++)
        {
            Vector3 corner = corners[i];
            var diff = target.position - corner;
            if (target.position.x < maxx && minx < target.position.x && target.position.y < maxy && miny < target.position.y)
            {
                distance = 0;
                break;
            }
            if(diff.x < xdiff)
            {
                xdiff = diff.x;
            }
            if(diff.y < ydiff)
            {
                ydiff = diff.y;
            }
            distance = Mathf.Max(xdiff, ydiff);
        }

        var t = Mathf.Clamp01(distance / size);
        Debug.Log(t);

        ui.alpha = Mathf.Lerp(aMin, aMax, t);
    }
}
