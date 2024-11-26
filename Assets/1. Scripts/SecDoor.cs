using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecDoor : MonoBehaviour
{
    public GameObject keyPad;
    public Text worngText;
    public GameObject secDoor;

    string answer = "0820";
    int btnIndex;
    string input = "";
    int index = 0;
    public bool corectBool = false;
    public bool turningBool = false;


    private void FixedUpdate()
    {
        //if(turningBool == true)
        //{
        //    RotationSecDoor();
        //}
    }

    public void Pause(bool isPause)
    {
        if (isPause)
            Time.timeScale = 0;
        else if (!isPause)
            Time.timeScale = 1f;
    }

    public void InputingAnser(int inputNum)
    {

        
        if(input == null)
        {
            input += inputNum.ToString();
        }
        else
        {
            if(input.Length <= 4)
            {
                input += inputNum.ToString();
            }
            else
            {
                input = "";
                input += inputNum.ToString();
            }
                
        }
        

    }

    public void CheckingAnser()
    {
        if(index <= 5)
        {
            print(input);
            if (input == answer)
            {
                keyPad.SetActive(false);
                Pause(false);
                corectBool = true;
                //secDoor.SetActive(false);
                turningBool = true;
            }
            else 
            { 
                
                if(worngText.text != "오답입니다.")
                {
                    index++;
                    worngText.text = "오답입니다." + index;
                }
                else
                {
                    worngText.text = "오답입니다.";
                } 
            }
        }
        else if(index > 5)
        {
            Exit();
            index = 0;
        }   

        
    }

    public void Exit()
    {
        Pause(false );
        keyPad.SetActive(false);
    }

    
    public void RetBtninD(int index)
    {
        btnIndex = index;
        InputingAnser(btnIndex);
        
    }

    public void ShowUI()
    {
        keyPad.SetActive(true);
        Pause(true);
    }

    public void RotationSecDoor()
    {
        //if(secDoor.transform.rotation.y > -70000000f)
        //{
        //    secDoor.transform.Rotate(0, -1 * 5, 0);
        //}
        secDoor.SetActive(false);
    }
}
