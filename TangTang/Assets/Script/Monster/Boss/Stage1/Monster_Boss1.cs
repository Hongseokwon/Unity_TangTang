using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Boss1 : Monster
{
    enum FIRE_PATTERN
    {
        PATTERN1,PATTERN2, PATTERN_END
    }
    // Start is called before the first frame update
    void Start()
    {
        Speed = 0f;
        Fire_Delay_Time = 5f;
        Fire_Delay_Count = 0f;
        Bullet_Dis = 1f;
        Fire_Pattern = FIRE_PATTERN.PATTERN1;
        Target_Obj = Character_Manager.Instance.Get_Character();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        Fire_Delay_Check();
    }


    private void Fire_Delay_Check()
    {
        Debug.Log("Delay");
        Fire_Delay_Count += Time.deltaTime;
        if (Fire_Delay_Count > Fire_Delay_Time)
        {
            Bullet_Fire_Pattern();
            Fire_Delay_Count = 0f;
        }
    }

    private void Bullet_Fire_Pattern()
    {
        Debug.Log("Pattern");
        Fire_Pattern = (FIRE_PATTERN)Random.Range(0, (int)FIRE_PATTERN.PATTERN_END);
        switch (Fire_Pattern)
        {
            case FIRE_PATTERN.PATTERN1:
                StartCoroutine(Bullet_Fire2());
                //Bullet_Fire1();
                break;
            case FIRE_PATTERN.PATTERN2:
                StartCoroutine(Bullet_Fire2());
                break;
        }
    }

    private void Bullet_Fire1()
    {
        for (int i = 0; i < 18; ++i)
        {
            Bullet_Dir = new Vector3(Mathf.Cos(20 * i * Mathf.Deg2Rad), Mathf.Sin(20 * i * Mathf.Deg2Rad), 0f);
            Bullet_Dir.Normalize();
            Bullet_Pos = transform.position + (Bullet_Dir * Bullet_Dis);
            Bullet_Manager.Instance.Monster_Bullet_Normal_Fire(new GameManager.BULLET_INFO(Bullet_Pos, Bullet_Dir, Monster_Info.Obj_Info.Att));
        }
    }

    private IEnumerator Bullet_Fire2()
    {
        Bullet_Dir = Target_Obj.transform.position - transform.position;
        float Deg = Mathf.Rad2Deg * Mathf.Atan2(Bullet_Dir.y, Bullet_Dir.x);

        for (int i = 0; i < 11; ++i)
        {
            yield return new WaitForSeconds(0.075f);
            Bullet_Dir = new Vector3(Mathf.Cos((Deg - 60 + (12 * i)) * Mathf.Deg2Rad), Mathf.Sin((Deg - 60 + (12 * i)) * Mathf.Deg2Rad), 0f);

            Bullet_Dir.Normalize();
            Bullet_Pos = transform.position + (Bullet_Dir * Bullet_Dis);
            Bullet_Manager.Instance.Monster_Bullet_Normal_Fire(new GameManager.BULLET_INFO(Bullet_Pos, Bullet_Dir, Monster_Info.Obj_Info.Att));
        }
        for (int i = 0; i < 10; ++i)
        {
            yield return new WaitForSeconds(0.075f);
            Bullet_Dir = new Vector3(Mathf.Cos((Deg + 54 - (12 * i)) * Mathf.Deg2Rad), Mathf.Sin((Deg + 54- (12 * i)) * Mathf.Deg2Rad), 0f);
            Bullet_Dir.Normalize();
            Bullet_Pos = transform.position + (Bullet_Dir * Bullet_Dis);
            Bullet_Manager.Instance.Monster_Bullet_Normal_Fire(new GameManager.BULLET_INFO(Bullet_Pos, Bullet_Dir, Monster_Info.Obj_Info.Att));
        }
    }

    private float Fire_Delay_Time;
    private float Fire_Delay_Count;
    private float Bullet_Dis;
    private Vector3 Bullet_Dir;
    private Vector3 Bullet_Pos;

    FIRE_PATTERN Fire_Pattern;
}
