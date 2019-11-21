using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    #region Editable attributes
    public Camera camera = null;
    public string cameraName = "";
    #endregion

    string path = "C:/sr/Rock1";
    int cnt = 0;
    // Start is called before the first frame update

    void Start()
    {
        //path = Application.dataPath + "/ScreenShot";
        Debug.Log(path);
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

        Debug.Log($"{cameraName} / {cnt}");
        if(cnt > 200)
        {
            Debug.Log("Capture End!");
            return;
        }

        string name = $"{path}/{cnt:000}_{cameraName}.jpg";
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
