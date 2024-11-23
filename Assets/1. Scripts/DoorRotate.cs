using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public Transform door;

    public bool openDoor = false;
    public float doorRotateValue = 0;
    public int speed;
    public int rating = 7;
    

    private void Update()
    {
        doorRotateValue = door.rotation.y;
              
        if (openDoor)
        {
            door.Rotate(0, +1 * Time.deltaTime * speed, 0);
            if(doorRotateValue >= Mathf.Abs(0.5f))
            {
                openDoor = false;
            }
        }
        
        else if(!openDoor)
        {
            if (door.rotation.y != 0) 
            {
                door.Rotate(0, -1 * Time.deltaTime * speed, 0);
                if (doorRotateValue <= 0.01f)
                {
                    speed = 0;

                }
                
            }
            
            
        }

    }

    void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            openDoor = true;
            speed = rating;
        }
        else
        {
            openDoor = false;
        }
    }



}
