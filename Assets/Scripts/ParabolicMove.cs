using UnityEngine;
using DG.Tweening;

public class ParabolicMove : MonoBehaviour
{
    public float height = 2f;
    public float duration = 1f;

    public void MoveOnParabola(Transform startPoint, Transform endPoint)
    {

        Vector3[] path = CalculateParabolaPoints(startPoint.position, endPoint.position, height, 0.1f);

        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.OutQuad).OnComplete(() =>
            {
                //Destroy(gameObject);
            });
    }

    private Vector3[] CalculateParabolaPoints(Vector3 start, Vector3 end, float height, float interval)
    {
        int numberOfPoints = Mathf.CeilToInt(1f / interval);
        Vector3[] points = new Vector3[numberOfPoints + 1];

        for (int i = 0; i <= numberOfPoints; i++)
        {
            float t = i * interval;
            float x = Mathf.Lerp(start.x, end.x, t);
            float y = Mathf.Lerp(start.y, end.y, t) + Mathf.Sin(t * Mathf.PI) * height;
            float z = Mathf.Lerp(start.z, end.z, t);

            points[i] = new Vector3(x, y, z);
        }

        return points;
    }
}