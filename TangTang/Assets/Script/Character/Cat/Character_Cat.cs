using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Cat : Character
{
    // Start is called before the first frame update

    void Start()
    {
        Speed = 3f;
        Dir_Dis = 3f;
        Bullet_Dis = 0.5f;

        Hp_Down_Check = true;
        Hp_Down_Count = 0f;
        Hp_Down_Delay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer_Manager.Instance.Active)
            Bullet_Check();

        if (!Hp_Down_Check)
            Hp_Down_Check_Count();

    }
}
