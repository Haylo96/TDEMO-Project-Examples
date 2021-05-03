using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor_Master : MonoBehaviour
{

    public bool ReactorCollide;
    bool Power;
    float fog_density = 0.03F;
    float Top_Speed = 0F;
    float Mid_Speed = 0F;
    float Low_Speed = 0F;
    float Core_Speed = 0F;
    public float[] Ring_Light_Intensity = new float[4];
    public float[] Ring_Laser_Length_Big = new float[4];
    public float[] Ring_Laser_Length_Small = new float[4];
    public Vector4 Light_Colour;
    Animator reac_anim;
    public float Modifier = 1F;
    public float Min_Intensity = 0F;
    public float Max_Intensity = 10F;
    float Intensity = 0F;
    float Intensity_Modifier = 2.5F,  Intensity_Modifier_Max = 250F;
    float Length_Big_Modifier = -5.15F, Length_Big_Modifier_Min = -20.6F;
    float Length_Small_Modifier = 0.1F, Length_Small_Modifier_Max = 1F;
    public float Core_Intensity = 0F, Core_Intensity_Modifier = 1F, Core_Intensity_Max = 100F;
    //float Core_Volume = 0F, Core_Max_Vol = 0F, Core_Pitch = 0.9F, Core_Max_Pitch = 0.9F, Reactor_Volume = 0F, Reactor_Max_Vol = 0F, Reactor_Pitch = 0.9F, Reactor_Max_Pitch = 0.9F;
    bool Positive = true;
    bool Alarm = true;
    bool Ring1_ready = false, Ring2_ready = false, Ring3_ready = false;
    bool Laser1_Ready = false, Laser2_Ready = false, Laser3_Ready = false;
    GameObject[] Low, Mid, Top;




    void IntensitySet()
    {

        
            foreach (GameObject Laser in Low)
            {

            Laser.GetComponent<Light>().intensity = Intensity;

            }
        
            foreach (GameObject Laser in Mid)
            {

                Laser.GetComponent<Light>().intensity = Intensity;

        }
        
            foreach (GameObject Laser in Top)
            {

                Laser.GetComponent<Light>().intensity = Intensity;

        }
        GameObject.Find("Reactor_Light").GetComponent<Light>().intensity = Intensity;

    }

    void IntensInc()
    {

        Intensity += Modifier;
        IntensitySet();

        if (Intensity >= Max_Intensity)
        {

            Positive = false;

        }

    }

    void IntensDec()
    {

        Intensity -= Modifier;
        IntensitySet();

        if (Intensity <= Min_Intensity)
        {

            Positive = true;

        }

    }

    private void Emergency_Lighting()
    {

        if (Positive == true)
        {

            IntensInc();

        }
        else if (Positive == false)
        {

            IntensDec();

        }

    }

    private void Emergency_Lighting_WindDown()
    {

        Min_Intensity = 0F;
        Intensity -= Modifier;
        IntensitySet();

        if (Intensity <= 0)
        {

            Intensity = 0;
            Alarm = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            ReactorCollide = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            ReactorCollide = false;

        }
    }

    private void Green_Light()
    {

        Light_Colour.Set(0F, 1F, 1F, 1F);

        foreach (GameObject LightCol in Low)
        {
            LightCol.GetComponent<Light>().color = Light_Colour;
        }
        foreach (GameObject LightCol in Mid)
        {
            LightCol.GetComponent<Light>().color = Light_Colour;
        }
        foreach (GameObject LightCol in Top)
        {
            LightCol.GetComponent<Light>().color = Light_Colour;
        }
        GameObject.Find("Reactor_Light").GetComponent<Light>().color = Light_Colour;

    }

    private bool Ring_Light_Intensity_Set(int _Ring)
    {

        _Ring --;

        
            if (_Ring == 2)
                foreach (GameObject Laser in Low)
                {

                    Laser.GetComponent<Light>().intensity = Ring_Light_Intensity[_Ring];

                }
            else if (_Ring == 1)
                foreach (GameObject Laser in Mid)
                {

                    Laser.GetComponent<Light>().intensity = Ring_Light_Intensity[_Ring];

                }
            else if (_Ring == 0)
                foreach (GameObject Laser in Top)
                {

                    Laser.GetComponent<Light>().intensity = Ring_Light_Intensity[_Ring];

                }

        if (Ring_Light_Intensity[_Ring] < Intensity_Modifier_Max)
        {

            Ring_Light_Intensity[_Ring] += Intensity_Modifier;

            return false;

        }
        else
        {

            return true;
        
        }

        
    }

    //private void Pitch_Set(GameObject _Object, ref float _Max_Val, ref float _Current_Val)
    //{

        
    //    if (_Current_Val >= _Max_Val)
    //    {

    //        _Current_Val = _Max_Val;

    //    }
    //    else
    //    {

    //        _Current_Val += 0.002F;
    //        _Object.GetComponent<AudioSource>().pitch = _Current_Val;

    //    }

    //}

    //private void Volume_Set(GameObject _Object, ref float _Max_Val, ref float _Current_Val)
    //{

        
    //    if (_Current_Val >= _Max_Val)
    //    {

    //        _Current_Val = _Max_Val;

    //    }
    //    else
    //    {
    //        _Current_Val += 0.002F;
    //        _Object.GetComponent<AudioSource>().volume = _Current_Val;

    //    }

    //}

    private bool Laser_Length_Set_Check(int _Ring)
    {

        _Ring -= 1;

        bool j = false, k = false;
        if (_Ring == 2)
            foreach (GameObject Laser in Low)
            {

                Laser.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, Ring_Laser_Length_Big[_Ring]));
                Laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Ring_Laser_Length_Small[_Ring]));

            }
        else if (_Ring == 1)
            foreach (GameObject Laser in Mid)
            {

                Laser.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, Ring_Laser_Length_Big[_Ring]));
                Laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Ring_Laser_Length_Small[_Ring]));

            }
        else if (_Ring == 0)
            foreach (GameObject Laser in Top)
            {

                Laser.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, Ring_Laser_Length_Big[_Ring]));
                Laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Ring_Laser_Length_Small[_Ring]));

            }

        if (Ring_Laser_Length_Big[_Ring] > Length_Big_Modifier_Min)
            {

                Ring_Laser_Length_Big[_Ring] += Length_Big_Modifier;

            }
            else
            {

                j = true;

            }

            if (Ring_Laser_Length_Small[_Ring] < Length_Small_Modifier_Max)
            {

                Ring_Laser_Length_Small[_Ring] += Length_Small_Modifier;

            }
            else
            {

                k = true;

            }

            if ((j == true) && (k == true))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        reac_anim = GetComponent<Animator>();

        Alarm = true;

        Light_Colour.Set(1F, 0F, 0F, 1F);

        Ring_Laser_Length_Big[0] = 0F;
        Ring_Laser_Length_Big[1] = 0F;
        Ring_Laser_Length_Big[2] = 0F;

        Ring_Laser_Length_Small[0] = 0F;
        Ring_Laser_Length_Small[1] = 0F;
        Ring_Laser_Length_Small[2] = 0F;

        Low = GameObject.FindGameObjectsWithTag("Pylon_Laser_Low");
        Mid = GameObject.FindGameObjectsWithTag("Pylon_Laser_Mid");
        Top = GameObject.FindGameObjectsWithTag("Pylon_Laser_Top");

        foreach (GameObject Laser in Low)
        {
            Laser.GetComponent<Light>().color = Light_Colour;
            Laser.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, Ring_Laser_Length_Big[2]));
            Laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Ring_Laser_Length_Small[2]));
        }
        foreach (GameObject Laser in Mid)
        {
            Laser.GetComponent<Light>().color = Light_Colour;
            Laser.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, Ring_Laser_Length_Big[1]));
            Laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Ring_Laser_Length_Small[1]));
        }
        foreach (GameObject Laser in Top)
        {
            Laser.GetComponent<Light>().color = Light_Colour;
            Laser.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, Ring_Laser_Length_Big[0]));
            Laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Ring_Laser_Length_Small[0]));
        }

        GameObject.Find("Reactor_Light").GetComponent<Light>().color = Light_Colour;
        

    }


    private void Update()
    {

        //Pitch_Set(GameObject.Find("Reactor_Light"), ref Core_Max_Pitch, ref Core_Pitch);
        //Volume_Set(GameObject.Find("Reactor_Light"), ref Core_Max_Vol, ref Core_Volume);
        //Pitch_Set(GameObject.Find("lz_Reactor_Controller"), ref Reactor_Max_Pitch, ref Reactor_Pitch);
        //Volume_Set(GameObject.Find("lz_Reactor_Controller"), ref Reactor_Max_Vol, ref Reactor_Volume);

        reac_anim.SetFloat("Top_Ring_Speed", Top_Speed);
        reac_anim.SetFloat("Mid_Ring_Speed", Mid_Speed);
        reac_anim.SetFloat("Lower_Ring_Speed", Low_Speed);
        reac_anim.SetFloat("Core_Speed", Core_Speed);


        if (Alarm == true)
        {

            if (Power == false)
            {

                Emergency_Lighting();

            }
            else if (Power == true)
            {

                Emergency_Lighting_WindDown();

            }

        }
        else if (Alarm == false)
        {

            Green_Light();

            if (fog_density >= 0.01F)
            {

                fog_density -= 0.000005F;
                RenderSettings.fogDensity = fog_density;

            }

            if (Top_Speed < 1F || (Ring1_ready = false))
            {

                Top_Speed += 0.005F;
                if (Top_Speed >= 1F)
                {

                    Top_Speed = 1F;

                }
                Ring1_ready = Ring_Light_Intensity_Set(1);

                //GameObject.Find("lz_Reactor_Controller").GetComponent<AudioSource>().volume = 0.5F;

                //Reactor_Max_Pitch = 0.95F;
                //Reactor_Max_Vol = 0.25F;
                

            }
            else if (Top_Speed >= 1F && (Ring1_ready = true))
            {

                if ((Mid_Speed < 1F) || (Ring2_ready = false) || (Laser1_Ready = false))
                {

                    Laser1_Ready = Laser_Length_Set_Check(1);

                    //Core_Max_Pitch = 0.95F;
                    //Core_Max_Vol = 0.25F;

                    Mid_Speed += 0.005F;
                    if (Mid_Speed >= 1F)
                    {

                        Mid_Speed = 1F;

                    }
                        Ring2_ready = Ring_Light_Intensity_Set(2);

                    
                    //Reactor_Max_Pitch = 1F;
                    //Reactor_Max_Vol = 0.5F;

                }
                else if (Mid_Speed >= 1F && (Ring2_ready = true) && (Laser1_Ready = true))
                {

                    if ((Low_Speed < 1F) || (Ring3_ready = false) || (Laser2_Ready = false))
                    {

                        Laser2_Ready = Laser_Length_Set_Check(2);

                        //Core_Max_Pitch = 1F;
                        //Core_Max_Vol = 0.5F;

                        Low_Speed += 0.005F;
                        if (Low_Speed >= 1F)
                        {

                            Low_Speed = 1F;

                        }
                        Ring3_ready = Ring_Light_Intensity_Set(3);

                        
                        //Reactor_Max_Pitch = 1.05F;
                        //Reactor_Max_Vol = 0.75F;

                    }
                    else if (Low_Speed >= 1F && (Ring3_ready = true) && (Laser2_Ready = true))
                    {
                        if (Laser3_Ready == false)
                        {

                            Laser3_Ready = Laser_Length_Set_Check(3);

                            //Core_Max_Pitch = 1.05F;
                            //Core_Max_Vol = 0.75F;

                        }
                        else if ((Core_Speed < 1F) || (Core_Intensity < Core_Intensity_Max))
                        {

                            Core_Speed += 0.005F;
                            Core_Intensity += Core_Intensity_Modifier;
                            GameObject.Find("Reactor_Light").GetComponent<Light>().intensity = Core_Intensity;

                            //Reactor_Max_Pitch = 1.1F;
                            //Reactor_Max_Vol = 1F;

                            if (Core_Speed >= 1F )
                            {

                                Core_Speed = 1F;

                            }

                        }
                        else if ((Core_Speed >= 1F) && (Core_Intensity >= Core_Intensity_Max))
                        {

                            GameObject.Find("Level_Controller").GetComponent<Level_Controller>().Power = true;
                            GameObject.Find("Level_Controller").GetComponent<Level_Controller>().Control_Loss = false;
                            GameObject.Find("Level_Controller").GetComponent<Level_Controller>().Current_Camera = 0;

                        }

                    }

                }

            }

            

        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Power == false)
        {
            if (ReactorCollide == true)
            {

                if (Input.GetMouseButtonDown(0))
                {

                    Power = true;

                    GameObject.Find("Level_Controller").GetComponent<Level_Controller>().Control_Loss = true;
                    GameObject.Find("Level_Controller").GetComponent<Level_Controller>().Current_Camera = 2;

                }

            }
        }
    }
}
