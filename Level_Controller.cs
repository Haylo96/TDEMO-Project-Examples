using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Controller : MonoBehaviour
{

    public bool Power = false;
    public bool Control_Loss = false;
    public int Current_Camera = 0, Current_Log, Current_Keycard;
    public float Modifier = 0.01F;
    public float Min_Intensity = 0.2F;
    public float Max_Intensity = 2F;
    float Log_Timer, Dialogue_Timer;
    public bool Log_Obtained = false, Keycard_Obtained = false, Dialogue_Triggered = false;
    public bool[] Journal = new bool[10], Keycard = new bool[5], Dialogue = new bool[32];
    public Material[] Journal_Material = new Material[10], Screen_Material = new Material[4];
    public Sprite[] Dialogue_Sprite = new Sprite[32];
    public GameObject PADDUI, ThoughtsUI, CommandC_Screen,  Reactor_Screen;
    public GameObject[] ComputerC_Screen = new GameObject[7];
    public bool Comp_Core_Done, Reactor_Done, Office_Done, S_Office_Done;
    public Material Glass;
    Color Current_Colour, Target_Colour = Color.cyan;
    float Current_Fade = 0;
    public float Fade_Duration = 3;
    public Queue<int> Dialogue_Queue = new Queue<int>();

    private void Journal_Display()
    {

        if (Journal[Current_Log] == false)
        {

            Journal[Current_Log] = true;
            PADDUI.GetComponent<UnityEngine.UI.Image>().material = Journal_Material[Current_Log];
            PADDUI.SetActive(true);

            Log_Timer = 600;

        }

        Log_Timer--;
        
        if (Log_Timer <= 0 || Input.GetKeyDown("f"))
        {

            PADDUI.SetActive(false);
            Log_Obtained = false;

            switch (Current_Log)
            {

                case 0:
                    Dialogue_Queue.Enqueue(2);
                    break;
                case 1:
                    Dialogue_Queue.Enqueue(6);
                    break;
                case 2:
                    Dialogue_Queue.Enqueue(9);
                    break;
                case 3:
                    Dialogue_Queue.Enqueue(13);
                    break;
                case 4:
                    Dialogue_Queue.Enqueue(15);
                    break;
                case 5:
                    Dialogue_Queue.Enqueue(16);
                    break;
                case 6:
                    Dialogue_Queue.Enqueue(17);
                    break;
                case 7:
                    Dialogue_Queue.Enqueue(19);
                    break;
                case 8:
                    Dialogue_Queue.Enqueue(22);
                    break;
                case 9:
                    Dialogue_Queue.Enqueue(23);
                    break;
                default:
                    break;
            }

        }

    }

    private void Dialogue_Display()
    {

        if (Dialogue_Queue.Count > 0)
        {

            if (Dialogue[Dialogue_Queue.Peek()] == false)
            {

                Dialogue[Dialogue_Queue.Peek()] = true;
                ThoughtsUI.GetComponent<UnityEngine.UI.Image>().sprite = Dialogue_Sprite[Dialogue_Queue.Peek()];
                ThoughtsUI.SetActive(true);

                Dialogue_Timer = 300;

            }

            Dialogue_Timer--;

            if (Dialogue_Timer <= 0)
            {

                ThoughtsUI.SetActive(false);
                Dialogue_Triggered = false;
                Dialogue_Queue.Dequeue();

            }

        }

    }

    void Forcefield_Deactivation()
    {

        if (Current_Keycard == 0)
        {
            Destroy(GameObject.Find("Forcefield_Red"));
            Keycard[Current_Keycard] = true;
        }
        else if (Current_Keycard == 1)
        {
            Destroy(GameObject.Find("Forcefield_Green"));
            Keycard[Current_Keycard] = true;
        }
        else if (Current_Keycard == 2)
        {
            Destroy(GameObject.Find("Forcefield_Blue"));
            Keycard[Current_Keycard] = true;
        }
        else if (Current_Keycard == 3)
        {
            Destroy(GameObject.Find("Forcefield_Yellow"));
            Keycard[Current_Keycard] = true;
        }
        else if (Current_Keycard == 4)
        {
            Destroy(GameObject.Find("Forcefield_White"));
            Keycard[Current_Keycard] = true;
        }

        Keycard_Obtained = false;

    }

    private void Start()
    {

        Current_Camera = 0;

        PADDUI.SetActive(true);
        ThoughtsUI.SetActive(true);
        PADDUI.SetActive(false);
        ThoughtsUI.SetActive(false);
        GameObject.Find("_ADD_9").SetActive(false);
        GameObject.Find("Forcefield_White").SetActive(false);
        Glass.SetColor("Glass_Emission", Color.red);
        Current_Colour = Glass.GetColor("Glass_Emission");

    }

    private void Update()
    {

        if (Log_Obtained == true)
        {

            Journal_Display();

        }
        
        if (Keycard_Obtained == true)
        {

            Forcefield_Deactivation();

        }
               
            Dialogue_Display();

        if (Power == true)
        {

            if (Current_Fade < Fade_Duration)
            {

                Current_Fade += Time.deltaTime * Fade_Duration;

                Glass.SetColor("Glass_Emission", Color.Lerp(Current_Colour, Target_Colour, Current_Fade));

            }

            Reactor_Screen.GetComponent<MeshRenderer>().material = Screen_Material[2];
            CommandC_Screen.GetComponent<MeshRenderer>().material = Screen_Material[2];

            if (Keycard[2] == false)
            {
                foreach (GameObject _Screen in ComputerC_Screen)
                {
                    _Screen.GetComponent<MeshRenderer>().material = Screen_Material[0];
                }
            }
            else if (Keycard[2] == true)
            {
                foreach (GameObject _Screen in ComputerC_Screen)
                {
                    _Screen.GetComponent<MeshRenderer>().material = Screen_Material[1];
                }
            }

            if (Keycard[3] == true)
            {
                CommandC_Screen.GetComponent<MeshRenderer>().material = Screen_Material[3];
                GameObject.Find("_ADD_9").SetActive(true);

            }

        }

    }

}
