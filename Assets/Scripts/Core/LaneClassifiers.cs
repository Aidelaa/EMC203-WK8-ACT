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
    public static int laneGaps = 3;
    public static System.Random random = new System.Random();

    public static Lanes GetRandomLane()
    {
        Array lanes = Enum.GetValues(typeof(Lanes));
        
        return (Lanes)lanes.GetValue(random.Next(lanes.Length));
    }

    public static (int, int) GetMinMaxLaneValues()
    {
        int[] lanes = (int[])Enum.GetValues(typeof(Lanes));
        Array.Sort(lanes);
        
        return (lanes[0], lanes[^1]);
    }
}
