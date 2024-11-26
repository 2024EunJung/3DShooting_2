using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed; // �̵��ӵ�
    public float jumpPower; // �����ϴ� ��
    public float rotateSpeed; // ȸ�� �ӵ�
    public float distance;
    
    public GameObject InterE;
    public GameObject InteractionDoor;
    public GameObject SecDoor;
    


    int jumpCount; // ������ Ƚ��

    Rigidbody rb; // �÷��̾��� Rigidbody ������Ʈ
    Animator anim; // �÷��̾��� Animator ������Ʈ
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾��� Rigidbody, Animator ������Ʈ �����ͼ� ����
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        tr = transform;

        

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


        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        distance = Mathf.Abs(hit.transform.position.x - tr.position.x) + Mathf.Abs(hit.transform.position.z - tr.position.z);
        //Vector3.Distance(hit.transform.position, tr.position);



        if (hit.collider.CompareTag("Door"))
        {


            if (distance <= 2f)
            {
                ShowUIFOREKey(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractionDoor.GetComponent<InteractionDoor>().OpenDoor();
                    ShowUIFOREKey(false);
                    return;
                }

            }
            else
            {

                ShowUIFOREKey(false);
            }





        }

        if (hit.collider.CompareTag("SecDoor"))
        {
            
            if(!SecDoor.GetComponent<SecDoor>().corectBool)
            {
                if (distance <= 2f)
                {
                    ShowUIFOREKey(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ShowUIFOREKey(false);
                        SecDoor.GetComponent<SecDoor>().ShowUI();
                        
                        return;
                    }
                    
                }
                else
                {

                    ShowUIFOREKey(false);
                }
            }
            
        }
      

    }

        void OnCollisionEnter(Collision collision)
        {
            // �浹�� ��ü�� �±װ� "Ground"���
            if (collision.gameObject.tag == "Ground")
            {
                // ���� Ƚ�� �ʱ�ȭ
                jumpCount = 0;

                // ���� �ִϸ��̼� ����
                anim.SetBool("isJump", false);
            }
        }

        void ShowUIFOREKey(bool statu)
        {
            InterE.SetActive(statu);
        }
  }

