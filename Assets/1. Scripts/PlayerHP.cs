using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    float hp = 100; // �÷��̾�ü��
    public Slider hpBar; // �÷��̾��� ü�¹�

    Animator anim; // �÷��̾��� Animator ������Ʈ

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
        // ���� ���� ������ ��ŭ ü�� ����
        hp -= damage;

        // ü�¹ٿ� ü�� ǥ��
        hpBar.value = hp;

        if (hp > 0)
        {
            // �ǰ� �ִϸ��̼� ����
            anim.SetTrigger("damaged");
        }
        else
        {
            // ���� �ִϸ��̼� ����
            anim.SetTrigger("dead");

            // �÷��̾��� ��� �ߴ�
            GetComponent<Player>().enabled = false;
            GetComponent<PlayerFire>().enabled = false;
            GetComponentInChildren<CameraRotate>().enabled = false;

            // ���� ���� ��� ���� ��� �ߴ�
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.enabled = false;
            }
        }
    }
}
