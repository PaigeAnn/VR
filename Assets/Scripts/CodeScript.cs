using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;
using UnityEngine.InputSystem;

public class CodeScript : MonoBehaviour
{
    public string password;
    public string enteredPassword;
    //public TMP_Text keypadDisplay;
    public int passDigits;
    
    public GameObject player;
    public GameObject keypad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        passDigits = password.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (enteredPassword.Length == passDigits)
        {
            if (enteredPassword == password)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                enteredPassword = "";
            }
        }
    }


    public void ButtonNumber(string btnNum)
    {
        EnterCode(btnNum);
    }

    private void EnterCode(string btnNum)
    {
        enteredPassword += btnNum;
    }
    public void clear()
    {
        enteredPassword = "";
    }
}


