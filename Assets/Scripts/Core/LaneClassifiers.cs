using System;
using UnityEngine;

public enum Lanes
{
    Left = -1,
    Middle = 0,
    Right = 1,
}

public class LaneClassifiers : MonoBehaviour
{
    public static int laneSpacing = 3;

    public static Lanes GetRandomLane()
    {
        Lanes[] lanes = (Lanes[])Enum.GetValues(typeof(Lanes));
        return lanes[UnityEngine.Random.Range(0, lanes.Length)];
    }

    public static (int minLane, int maxLane) GetMinMaxLaneValues()
    {
        int[] laneValues = (int[])Enum.GetValues(typeof(Lanes));
        return (laneValues[0], laneValues[laneValues.Length - 1]);
    }
}
