using Pcx;
using System.Linq;
using UnityEngine;

public class PointCamera : MonoBehaviour
{
    int cnt = 0;

    // Update is called once per frame
    void Update()
    {
        if (cnt > 200)
        {
            return;
        }

        var pcr = GameObject.Find("Rock Cloud").GetComponent<PointCloudRenderer>();
        var computeBuffer = pcr.sourceData.computeBuffer;
        var pointList = new Data.Point[computeBuffer.count];
        computeBuffer.GetData(pointList);

        var points = pointList.ToList()
             .OrderBy(x => Random.Range(0, pointList.Length))
             .Take(2048)
             .ToList();

        using (var file = new System.IO.StreamWriter($@"C:\sr\Rock2\{cnt}.txt"))
        {
            foreach (var point in points)
            {
                var position = point.position;
                //var color = point.color;

                file.WriteLine($"{position.x}, {position.y}, {position.z}");
            }
        }

        cnt++;
    }
}
