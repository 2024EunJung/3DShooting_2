using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class InteractionDoor : MonoBehaviour
{
    public float speed = 5;

    public int count = 0;
    int rotateDis = 1;
   public  bool isOpening = false;
    float origintr = 0;

    Transform tr;

    
    private void Start()
    {
        tr = transform;
        
    }


    private void FixedUpdate()
    {
        
    }


    public void OpenDoor()
    {
        

        if (!isOpening)
        {
            rotateDis = 1;
            count = 0;
            while (true)
            {
                origintr = tr.rotation.y;
                if (origintr >= Mathf.Abs(0.5f))
                {
                    break;
                }

                else
                {
                    tr.Rotate(0, rotateDis * Time.deltaTime * speed, 0);
                    count++;
                }

             
            }
            
            isOpening = true;

        }
        else if (isOpening)
        {
            rotateDis = -1;
            count = 0;
            while (true)
            {
                origintr = tr.rotation.y;
                if (origintr <= 0.01f)
                {
                    break;
                }

                else
                {
                    tr.Rotate(0, rotateDis * Time.deltaTime * speed, 0);
                    count++;
                }


            }

            isOpening = false;
        }
        
    }
}
