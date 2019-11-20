using UnityEngine;

public class PointBehaviour : MonoBehaviour
{
    int cnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cnt++;
        transform.rotation = Quaternion.Euler(cnt * 10 + cnt, cnt * 10 + cnt, cnt * 10 + cnt);
    }
}
