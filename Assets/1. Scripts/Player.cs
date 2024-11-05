using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // �̵��ӵ�
    public float jumpPower; // �����ϴ� ��
    public float rotateSpeed; // ȸ�� �ӵ�

    int jumpCount; // ������ Ƚ��

    Rigidbody rb; // �÷��̾��� Rigidbody ������Ʈ
    Animator anim; // �÷��̾��� Animator ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾��� Rigidbody, Animator ������Ʈ �����ͼ� ����
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ű �Ǵ� WASDŰ �Է��� ���ڷ� �޾Ƽ� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // x�࿡�� h�� ����, z�࿡�� v�� ���� ���� ���� ����
        Vector3 dir = new Vector3(h, 0, v);

        // ��� ������ �ӵ��� �����ϵ��� ����ȭ
        dir.Normalize();

        // �÷��̾ �������� dir�� ���� ����
        dir = transform.TransformDirection(dir);

        //// x�࿡�� h�� ����, z�࿡�� v�� ���� ��� ���ϱ�
        //transform.position += dir * moveSpeed * Time.deltaTime;

        // ���� �ۿ��� �̿��� �̵�
        rb.MovePosition(rb.position + (dir * moveSpeed * Time.deltaTime));

        // �̵��ϴ� �ӵ��� velocity ������ �Ҵ�
        anim.SetFloat("velocity", dir.magnitude);

        // <Space> Ű�� ���� ����, ������ Ƚ���� 2ȸ �̸��̶��
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // ���� �������� �� �߻�
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // ���� �ִϸ��̼� ����
            anim.SetTrigger("jump");
            anim.SetBool("isJump", true);

            // ������ ������ ���� Ƚ�� ����
            jumpCount++;
        }

        // ���콺�� �¿� ������ �Է��� ���ڷ� �޾Ƽ� ����
        float mouseMoveX = Input.GetAxis("Mouse X");

        // ���콺�� ������ ��ŭ Y�� ȸ��
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);

    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �±װ� "Ground"���
        if(collision.gameObject.tag == "Ground")
        {
            // ���� Ƚ�� �ʱ�ȭ
            jumpCount = 0;

            // ���� �ִϸ��̼� ����
            anim.SetBool("isJump", false);
        }
    }
}
