using UnityEngine;

public class CameraNewBehaviour : MonoBehaviour
{
    int angle_cnt = 1;
    int position_cnt = 0;

    void InitState(Transform transform)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(0, 0, -8);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 120 * Time.deltaTime);
        
        var angle = transform.rotation.eulerAngles.y;

        if(angle_cnt >= 2 && position_cnt >= 3)
        {
            Debug.Log("End!");
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }

        //Debug.Log($"{cnt}, {angle / Mathf.Deg2Rad}");
        if (angle > 330)
        {
            if (angle_cnt < 2)
            {    
                angle_cnt++;
                Debug.Log("angle");
            }
            else
            {
                if (position_cnt < 3)
                {
                    position_cnt++;
                    angle_cnt = 0;
                }
            }

            InitState(transform);
            transform.Translate(new Vector3(0, 0.5f * position_cnt, 0.2f * angle_cnt));
            transform.rotation = Quaternion.Euler(5 * position_cnt, 0, 0);
        }

        Debug.Log($"{angle} / {angle_cnt} / {position_cnt}");
    }
}
