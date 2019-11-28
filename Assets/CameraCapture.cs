using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    #region Editable attributes

    public string cameraName = "";
    #endregion

    private Camera camera = null;
    string path = "C:/sr/Rock1";

    int cnt = 0;
    // Start is called before the first frame update

    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }

        Debug.Log("Save path: " + path);
        camera = GameObject.Find($"{cameraName} Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt > 10000)
        {
            Debug.Log("Capture End!");
            return;
        }

        var resWidth = Screen.width;
        var resHeight = Screen.height;

        string name = $"{path}/{cnt:000}_{cameraName}.jpg";
        cnt++;

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Resources.UnloadUnusedAssets();

        byte[] bytes = screenShot.EncodeToJPG();
        File.WriteAllBytes(name, bytes);
    }
}
