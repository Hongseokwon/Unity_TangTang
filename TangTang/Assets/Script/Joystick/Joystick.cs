using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    private static Joystick instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Joystick Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Joystick_Active = true;

        Joystick_Dis_Scale = 0.55f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Joystick_Active && Timer_Manager.Instance.Active)
            Joystick_Update();
    }



    private void Joystick_On(Vector3 _Pos)
    {
        Joystick_Pos = _Pos;
        Joystick_Vector_Pos = _Pos;

        Joystick_Pos_Update();

        Joystick_Obj.SetActive(true);
        Joystick_Vector_Obj.SetActive(true);
    }

    private void Joystick_Off()
    {
        Joystick_Obj.SetActive(false);
        Joystick_Vector_Obj.SetActive(false);
    }

    private void Joystick_Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Joystick_Pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Joystick_Pos.z = 0f;

            Joystick_On(Joystick_Pos);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Joystick_Vector_Pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Joystick_Vector_Pos.z = 0f;

            Joystick_Vector_Pos -= Joystick_Pos;

            if (Joystick_Vector_Pos.magnitude > Joystick_Dis_Scale)
            {
                Joystick_Vector_Pos.Normalize();
                Joystick_Vector_Pos *= Joystick_Dis_Scale;
            }

            Joystick_Vector_Pos += Joystick_Pos;

            Joystick_Pos_Update();

            if (Joystick_Vector_Pos - Joystick_Pos != Vector3.zero)
                Character_Manager.Instance.Player_Move(Joystick_Vector_Pos - Joystick_Pos);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Joystick_Off();
        }
    }
    public void Joystick_Move(Vector3 _Move_Vector)
    {
        Joystick_Pos += _Move_Vector;
        Joystick_Vector_Pos += _Move_Vector;

        Joystick_Pos_Update();
    }

    private void Joystick_Pos_Update()
    {
        Joystick_Obj.transform.position = Joystick_Pos;
        Joystick_Vector_Obj.transform.position = Joystick_Vector_Pos;
    }


    public GameObject Joystick_Obj;
    public GameObject Joystick_Vector_Obj;

    private bool Joystick_Active;

    private Vector3 Joystick_Pos;
    private Vector3 Joystick_Vector_Pos;

    private float Joystick_Dis_Scale;
}
