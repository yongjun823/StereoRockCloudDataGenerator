using Pcx;
using System.Linq;
using UnityEngine;
using Microsoft.ML.Probabilistic.Math;

public class PointCamera : MonoBehaviour
{
    #region Editable attributes
    public string RockName = "";
    #endregion
    int cnt = 0;

    // Update is called once per frame
    void Update()
    {
        var angle = -transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        //Debug.Log($"{cnt}, {angle / Mathf.Deg2Rad}");
        if (cnt > 120)
        {
            return;
        }

        var pcr = GameObject.Find("Rock Cloud").GetComponent<PointCloudRenderer>();

        var computeBuffer = pcr.sourceData.computeBuffer;
        var pointList = new Data.Point[computeBuffer.count];
        computeBuffer.GetData(pointList);

        var points = pointList.ToList()
             //.OrderBy(x => Random.Range(0, pointList.Length))
             .Take(2048)
             .ToList();

        var tranformsMatrix = GetTransformationMatrix(transform.rotation, transform.position);

        var TP = points[0].position;
        var dd = new double[3] { TP.x, TP.y, TP.z };

        var tvec = DenseVector.FromArray(dd);

        var kk = tranformsMatrix * tvec;

        Debug.Log(kk);


        //using (var file = new System.IO.StreamWriter($@"C:\sr\{RockName}\{cnt:000}.csv"))
        //{
        //    foreach (var point in points)
        //    {
        //        file.WriteLine($"{point.x}, {point.y}, {point.z}");
        //    }
        //}

        cnt++;
    }

    Matrix GetTransformationMatrix(Quaternion rotate, Vector3 translate)
    {
        var tRotation = new Vector3(
            x: rotate.x * Mathf.Deg2Rad,
            y: rotate.y * Mathf.Deg2Rad,
            z: rotate.z * Mathf.Deg2Rad);

        var fXRotationMatrix = new Matrix(new double[3, 3]
            { { 1.0, 0.0, 0.0 },
            { 0.0, Mathf.Cos(tRotation.x), -Mathf.Sin(tRotation.x) },
            { 0.0, Mathf.Sin(tRotation.x), Mathf.Cos(tRotation.x) } });

        var fYRotationMatrix = new Matrix(new double[3, 3]
            { {Mathf.Cos(tRotation.y), 0.0, Mathf.Sin(tRotation.y) },
            { 0.0, 1.0, 0.0},
            { -Mathf.Sin(tRotation.y), 0.0, Mathf.Cos(tRotation.y)}});

        var fZRotationMatrix = new Matrix(new double[3, 3]
            {{Mathf.Cos(tRotation.z), -Mathf.Sin(tRotation.z), 0.0},
            { Mathf.Sin(tRotation.z), Mathf.Cos(tRotation.z), 0.0 },
            { 0.0, 0.0, 1.0 }});

        var fRotationMatrix = (fXRotationMatrix * fYRotationMatrix) * fZRotationMatrix;

        var fTransformationMatrix = new Matrix(new double[4, 4]
        {
            {fRotationMatrix[0, 0], fRotationMatrix[0, 1], fRotationMatrix[0, 2], translate.x },
            {fRotationMatrix[1, 0], fRotationMatrix[1, 1], fRotationMatrix[1, 2], translate.y },
            {fRotationMatrix[2, 0], fRotationMatrix[2, 1], fRotationMatrix[2, 2], translate.z },
            {0.0, 0.0, 0.0, 1.0 }
        });

        return fRotationMatrix;
    }
}
