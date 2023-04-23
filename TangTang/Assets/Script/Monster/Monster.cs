using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Set_Monster_Info(GameManager.MONSTER_INFO _Info, Vector3 _Pos)
    {
        transform.position = _Pos;
        Monster_Info = _Info;
        Target_Obj = Character_Manager.Instance.Get_Character();
        Hp_Ui_Update();
    }

    protected virtual void Monster_Move() { }

    public void Bullet_Hit(int _Att)
    {
        int Damage = _Att - Monster_Info.Obj_Info.Def;
        if (Damage < 1)
            Damage = 1;
        Monster_Info.Obj_Info.Hp -= Damage;
        Hp_Ui_Update();

        if (Monster_Info.Obj_Info.Hp <= 0)
            Monster_Dead();
    }

    public virtual void Monster_Dead() { }

    public void Hp_Ui_Update()
    {
        float Rate = (float)Monster_Info.Obj_Info.Hp / (float)Monster_Info.Obj_Info.Max_Hp;

        Vector3 Vec_Temp = Hp_Bar_Background.transform.localScale;

        Vec_Temp.x = Vec_Temp.x * Rate;

        Hp_Bar.transform.localScale = Vec_Temp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Character_Manager.Instance.Get_Character().GetComponent<Character>().Hp_Down(Monster_Info.Obj_Info.Att);
        }
    }


    protected Vector3 Target_Pos;
    protected Vector3 Move_Vector;

    protected float Speed;
    
    protected GameManager.MONSTER_INFO Monster_Info;

    public GameObject Target_Obj;

    public GameObject Hp_Bar;
    public GameObject Hp_Bar_Background;
}
