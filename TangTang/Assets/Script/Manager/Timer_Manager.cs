using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer_Manager : MonoBehaviour
{
    private static Timer_Manager instance = null;
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

    public static Timer_Manager Instance
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
        Active = true;
        T_Min_Count = 0;
        Min_Count = 0;
        T_Sec_Count = 0;
        Sec_Count = 0f;
        Timer_Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active && !Stage_Manager.Instance.Boss_Stage_Active)
        {
            Sec_Count += Time.deltaTime;
            Timer_Update();
        }
    }

    private void Timer_Update()
    {
        if(Sec_Count >= 10f)
        {
            Sec_Count -= 10f;
            T_Sec_Increase();
        }
        Sec_Timer.text = ((int)Sec_Count).ToString();
    }

    private void T_Sec_Increase()
    {
        T_Sec_Count++;
        if(T_Sec_Count ==6)
        {
            T_Sec_Count = 0;
            Min_Increase();
        }
        T_Sec_Timer.text = T_Sec_Count.ToString();
    }

    private void Min_Increase()
    {
        Min_Count++;
        if(Min_Count==10)
        {
            Min_Count = 0;
            T_Min_Increase();
        }
        if (Min_Count == 1)
            Stage_Manager.Instance.Phase2();
        else if (Min_Count == 2)
            Stage_Manager.Instance.Phase3();
        else if (Min_Count == 3)
            Stage_Manager.Instance.Phase4();
        else if (Min_Count == 4)
            Stage_Manager.Instance.Phase5();

        Min_Timer.text = Min_Count.ToString();
    }

    private void T_Min_Increase()
    {
        T_Min_Count++;
        T_Min_Timer.text = T_Min_Count.ToString();
    }

    public void Timer_Active()
    {
        Active = true;
    }

    public void Timer_Inactive()
    {
        Active = false;
    }

    public TMP_Text T_Min_Timer;
    public TMP_Text Min_Timer;
    public TMP_Text T_Sec_Timer;
    public TMP_Text Sec_Timer;

    public bool Active;

    private int T_Min_Count;
    private int Min_Count;
    private int T_Sec_Count;
    private float Sec_Count;
}
