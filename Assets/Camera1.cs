using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pcx;
using System.Linq;

public class Camera1 : MonoBehaviour
{
    List<Data.Point> points;
    string path;
    int resWidth, resHeight;
    // Start is called before the first frame update

    void Start()
    {
        var pcr = GameObject.Find("rock22").GetComponent<PointCloudRenderer>();
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

    }
}
