using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : IFormationPattern
{
    private float rotationSpeed = 30f; // �������� �������� �������� � �������
    private float currentAngleOffset = 0f; // ������� ����������� ��������
    public List<Vector2> GetPoints(int count, Transform center, float spacing)
    {
        List<Vector2> points = new List<Vector2>();
        float radius = spacing * count / Mathf.PI;

        // ��������� ����� ���� (Time.deltaTime ����� ������������ ������ ��������, �.�. ����� ���������� �� � Update)
        currentAngleOffset += rotationSpeed * Time.deltaTime;
        if (currentAngleOffset > 360f)
            currentAngleOffset -= 360f;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            angle += currentAngleOffset * Mathf.Deg2Rad; // ��������� ��������, ����������� � �������

            Vector2 localPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 worldPos = center.TransformPoint(localPos);
            points.Add(worldPos);
        }

        return points;
    }
}
