using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총 효과 프리팹을 담아둘 변수
    public GameObject ShootEffectPref;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // 마우스 커서 안보이게
        Cursor.visible = false;

        // 마우스 커서가 게임 화면을 벗어나지 못하도록 잠ㅈ금
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스를 좌클릭을 누르는 순간
        if(Input.GetMouseButtonDown(0))
        {
            // 총 쏘는 애니메이션 실행
            anim.SetTrigger("shoot");

            // 화면 가운데에서 시작하는 Ray 생성
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            // Ray에 맞은 물체를 담아둘 변수
            RaycastHit hit;

            // Ray를 발사하고, Ray에 맞은 물체는 hit에 저장, 맞은 물체가 있을 때만
            if(Physics.Raycast(ray, out hit))
            {
                // 맞은 위치에, 맞은 표면의 수직이 되는 각도로 총 효과 프리팹의 복사본 생성
                GameObject shootEffect = Instantiate(ShootEffectPref, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));

                // 총알 자국을 맞은 오브젝트들의 자식으로 설정
                shootEffect.transform.SetParent(hit.transform);

                // Ray에 맞은 물체가 적이라면
                if (hit.transform.tag == "Enemy")
                {
                    // 적에게 10만큼 공격 받으라고 전달
                    hit.transform.SendMessage("Damaged", 10);
                }
            }
        }
    }
}
