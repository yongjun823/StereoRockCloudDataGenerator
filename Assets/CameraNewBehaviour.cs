using UnityEngine;

public class CameraNewBehaviour : MonoBehaviour
{
    int angle_cnt = 1;
    int position_cnt = 0;

    void InitState(Transform transform)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(0, 0, -2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 50 * Time.deltaTime);
        
        var angle = transform.rotation.eulerAngles.y;

        //Debug.Log($"{cnt}, {angle / Mathf.Deg2Rad}");
        if (angle > 350)
        {
            if (angle_cnt < 5)
            {    
                angle_cnt++;
            }
            else
            {
                if (position_cnt < 5)
                {
                    position_cnt++;
                    angle_cnt = 0;
                }
            }

            InitState(transform);
            transform.Translate(new Vector3(0, 0.1f * position_cnt, 0.1f * angle_cnt));
        }

        Debug.Log($"{angle} / {angle_cnt} / {position_cnt}");
    }
}
