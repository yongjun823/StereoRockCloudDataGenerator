using UnityEngine;
using System.Collections;

public static class Data
{
    [System.Serializable]
    public struct Point
    {
        public Vector3 position;
        public uint color;
    }

    public static int cnt = 0;
}
