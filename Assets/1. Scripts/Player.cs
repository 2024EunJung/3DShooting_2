using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // 이동속도
    public float jumpPower; // 점프하는 힘
    public float rotateSpeed; // 회전 속도

    int jumpCount; // 점프한 횟수

    Rigidbody rb; // 플레이어의 Rigidbody 컴포넌트
    Animator anim; // 플레이어의 Animator 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어의 Rigidbody, Animator 컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키 또는 WASD키 입력을 숫자로 받아서 저장
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // x축에는 h의 값을, z축에는 v의 값을 넣은 변수 생성
        Vector3 dir = new Vector3(h, 0, v);

        // 모든 방향의 속도가 동일하도록 정규화
        dir.Normalize();

        // 플레이어를 기준으로 dir의 방향 조절
        dir = transform.TransformDirection(dir);

        //// x축에는 h의 값을, z축에는 v의 값을 계속 더하기
        //transform.position += dir * moveSpeed * Time.deltaTime;

        // 물리 작용을 이용해 이동
        rb.MovePosition(rb.position + (dir * moveSpeed * Time.deltaTime));

        // 이동하는 속도를 velocity 변수에 할당
        anim.SetFloat("velocity", dir.magnitude);

        // <Space> 키를 누른 순간, 점프한 횟수가 2회 미만이라면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // 위로 순간적인 힘 발생
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // 점프 애니메이션 실행
            anim.SetTrigger("jump");
            anim.SetBool("isJump", true);

            // 점프할 때마다 점프 횟수 증가
            jumpCount++;
        }

        // 마우스의 좌우 움직임 입력을 숫자로 받아서 저장
        float mouseMoveX = Input.GetAxis("Mouse X");

        // 마우스가 움직인 만큼 Y축 회전
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);

    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체의 태그가 "Ground"라면
        if(collision.gameObject.tag == "Ground")
        {
            // 점프 횟수 초기화
            jumpCount = 0;

            // 점프 애니메이션 종료
            anim.SetBool("isJump", false);
        }
    }
}
