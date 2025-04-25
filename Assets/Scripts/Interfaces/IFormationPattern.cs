using UnityEngine;
using System.Collections.Generic;

public interface IFormationPattern
{
    public List<Vector2> GetPoints(int count, Transform center, float spacing);
}
