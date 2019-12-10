using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cycle : MonoBehaviour
{
    //Variables that hold the numbers that the slot machine cycles through
    int[] numberCycle = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int arrayPos = 0;
    public Text slotNum;  //First Number
    public Text slotNum2; //Second Number
    public Text slotNum3; //Third Number
    public Text slotNum4; //Fourth Number

    public float clickCounter = 0; //Counts the number of times the "E" button is clicked
    
    public float frameCount = 0; //Counts the Frames
    public string passcode ="2222"; //Master Passcode that the player is trying to get to
    private float speed = 1; //Variable to controll frame speed

    void Start()
    {
       

    }
    //Called Whenever the "E" Button is clicked
    public void TaskOnClick()
    {
        clickCounter ++;

        if(clickCounter == 5)
        {
           
            checkPasscode(); //Function that checks the passcode entered
        }

        if (clickCounter == 6)
        {
            clickCounter = 0;  //Click counter reset function
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TaskOnClick();
        }
        //This if statement is used to cycle the number through the array
        if(frameCount == 12/speed)
        {
            if (clickCounter == 1)
            {
                slotNum.text = numberCycle[arrayPos].ToString();
                slotNum2.text = numberCycle[arrayPos].ToString();
                slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }

                speed = 2;
      
            }
            if (clickCounter == 2)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                slotNum2.text = numberCycle[arrayPos].ToString();
                slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
                speed = 3;
            }
            if (clickCounter == 3)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                //slotNum2.text = numberCycle[arrayPos].ToString();
                slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
                speed = 4;
            }
            if (clickCounter == 4)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                //slotNum2.text = numberCycle[arrayPos].ToString();
                //slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
            }
            if (clickCounter == 5)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                //slotNum2.text = numberCycle[arrayPos].ToString();
                //slotNum3.text = numberCycle[arrayPos].ToString();
                //slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
            }
        }
        //cycles the numbers randomly while the display is idle
        if (clickCounter == 0)
        {
            
            slotNum.text = numberCycle[Random.Range(0,9)].ToString();
            slotNum2.text = numberCycle[Random.Range(0, 9)].ToString();
            slotNum3.text = numberCycle[Random.Range(0, 9)].ToString();
            slotNum4.text = numberCycle[Random.Range(0, 9)].ToString();
        }

        if (frameCount == 13)
        {
            frameCount = 0;
        }



        if (speed == 5)
        {
            speed = 1;
        }
        frameCount++;

    }

    void stopFirstNum()
    {
        //slotNum.text = numberCycle[arrayPos].ToString();
    }

    public void checkPasscode()
    {
        //checks to see if the passcode is correct and if it is displays Correct in the debug log
        string s = slotNum.text + slotNum2.text + slotNum3.text + slotNum4.text;
        if(s == passcode)
        {
            Debug.Log("Correct");
        }
        
    }

}
