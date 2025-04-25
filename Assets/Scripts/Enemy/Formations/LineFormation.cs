using System.Collections.Generic;
using UnityEngine;

public class LineFormation : IFormationPattern
{
    public List<Vector2> GetPoints(int count, Transform center, float spacing)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            float offset = (i - (count - 1) / 2f) * spacing;

            Vector2 localPos = new Vector2(offset, 0);
            Vector2 worldPos = center.TransformPoint(localPos);
            points.Add(worldPos);
        }
        return points;
    }
}
