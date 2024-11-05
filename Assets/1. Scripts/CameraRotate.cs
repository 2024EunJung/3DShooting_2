using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotateSpeed; // 회전 속도
    float tempX; // eulerAngles.x 의 값을 담아둘 변수

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스의 위아래 움직임 입력을 숫자로 받아서 저장
        float mouseMoveY = Input.GetAxis("Mouse Y");
        // 마우스가 움직인 만큼 X축 회전
        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime,0 , 0);

        // x의 각도가 180을 넘는다면
        if (transform.eulerAngles.x > 180)
        {
            // 360을 빼서 음수로 저장
            tempX = transform.eulerAngles.x - 360;
        }
        // x의 각도가 180을 넘지 않는다면
        else
        {
            // 그대로 저장
            tempX = transform.eulerAngles.x;
        }

        // 음수를 포함한 x의 각도를 -30도 ~ 30도로 제한
        tempX = Mathf.Clamp(tempX, -30, 30);
        // 제한된 값을 eulerAngles.x에 적용 (y축과 z축은 고정되지 않고 현재 각도대로)
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
