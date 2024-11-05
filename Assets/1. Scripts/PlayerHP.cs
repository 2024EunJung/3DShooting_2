using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    float hp = 100; // 플레이어체력
    public Slider hpBar; // 플레이어의 체력바

    Animator anim; // 플레이어의 Animator 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damaged(float damage)
    {
        // 공격 받은 데미지 만큼 체력 감소
        hp -= damage;

        // 체력바에 체력 표시
        hpBar.value = hp;

        if (hp > 0)
        {
            // 피격 애니메이션 실행
            anim.SetTrigger("damaged");
        }
        else
        {
            // 죽음 애니메이션 실행
            anim.SetTrigger("dead");

            // 플레이어의 기능 중단
            GetComponent<Player>().enabled = false;
            GetComponent<PlayerFire>().enabled = false;
            GetComponentInChildren<CameraRotate>().enabled = false;

            // 게임 내의 모든 적의 기능 중단
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.enabled = false;
            }
        }
    }
}
