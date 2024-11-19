using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecDoor : MonoBehaviour
{
    public GameObject keyPad;


    string answer = "0820";
    int btnIndex;
    string input = "";

    void Start()
    {
        
    }


    public void Pause(bool isPause)
    {
        if (isPause)
            Time.timeScale = 0;
        if (!isPause)
            Time.timeScale = 1f;
    }

    public void InputingAnser(int inputNum)
    {
        if(input.Length <= 4)
            input = input + inputNum.ToString();

    }

    public void CheckingAnser()
    {
        if (input == answer)
        {

        }
        else 
        { 
            
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
}
