using UnityEngine;
using System.Collections.Generic;

public class TriangleFormation : IFormationPattern
{
    public List<Vector2> GetPoints(int count, Transform center, float spacing)
    {
        List<Vector2> points = new List<Vector2>();
        int index = 0;
        int rows = Mathf.CeilToInt(Mathf.Sqrt(2 * count));

        for (int row = 0; row < rows && index < count; row++)
        {
            int cols = row + 1;
            for (int col = 0; col < cols && index < count; col++, index++)
            {
                float x = (col - row / 2f) * spacing;
                float y = -row * spacing;

                Vector2 localPos = new Vector2(x, y);
                Vector2 worldPos = center.TransformPoint(localPos);
                points.Add(worldPos);
            }
        }

        return points;
    }
}

