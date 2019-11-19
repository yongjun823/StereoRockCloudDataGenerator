using System.Collections.Generic;
using UnityEngine;
using Pcx;
using System.Linq;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    #region Editable attributes
    public Camera camera = null;
    public string cameraName = "";
    #endregion

    List<Data.Point> points;
    string path;
    int cnt = 0;
    // Start is called before the first frame update

    void Start()
    {
        path = Application.dataPath + "/ScreenShot";
        Debug.Log(path);

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
        var resWidth = Screen.width;
        var resHeight = Screen.height;

        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }
        
        if(cnt > 10)
        {
            return;
        }

        string name = $"{path}/{cnt}_{cameraName}.jpg";
        cnt++;
        
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        byte[] bytes = screenShot.EncodeToJPG();
        File.WriteAllBytes(name, bytes);
    }
}
