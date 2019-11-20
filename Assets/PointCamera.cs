using Pcx;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointCamera : MonoBehaviour
{
    List<Data.Point> points;
    int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        var pcr = GameObject.Find("Rock Cloud").GetComponent<PointCloudRenderer>();
        var computeBuffer = pcr.sourceData.computeBuffer;
        var pointList = new Data.Point[computeBuffer.count];
        computeBuffer.GetData(pointList);

        var random = new Random();

        points = pointList.ToList()
            .OrderBy(x => Random.Range(0, pointList.Length))
            .Take(2048)
            .ToList();
    }

    // Update is called once per frame
    void Update()
    {

        //foreach(var point in points)
        //{
        //    var position = point.position;
        //    var color = point.color;
        //}

        cnt++;
    }
}
