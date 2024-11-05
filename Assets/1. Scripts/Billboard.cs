using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라가 바라보는 방향대로 내가 바라보는 방향 설정
        transform.forward = Camera.main.transform.forward;
    }
}
