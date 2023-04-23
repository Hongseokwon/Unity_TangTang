using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private void Awake()
    {
        Bullet_Delay_Time = new Dictionary<GameManager.BULLET_TYPE, float>();
        Bullet_Count = new Dictionary<GameManager.BULLET_TYPE, float>();
        Bullet_Att_Per = new Dictionary<GameManager.BULLET_TYPE, float>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Player_Move(Vector3 _Move_Vector)
    {

        Move_Vector = _Move_Vector.normalized;
        _Move_Vector = Move_Vector * Speed * Time.deltaTime;

        transform.position += _Move_Vector;

        if (!Stage_Manager.Instance.Boss_Stage_Active)
            Joystick.Instance.Joystick_Move(_Move_Vector);

        _Move_Vector = _Move_Vector.normalized * Dir_Dis;

        Character_Dir.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Rad2Deg * (Mathf.Atan2(_Move_Vector.y, _Move_Vector.x))));
        Character_Dir.transform.localPosition = _Move_Vector;

        float X = Stage_Manager.Instance.Get_Tile_X();
        float Y = Stage_Manager.Instance.Get_Tile_Y();


        if (Stage_Manager.Instance.Boss_Stage_Active)
        {
            Vector3 Temp_Pos = transform.position;

            if (Boss_Start_Character_Pos.x - transform.position.x > 2.5f)
                Temp_Pos.x = Boss_Start_Character_Pos.x - 2.5f;
            else if (Boss_Start_Character_Pos.x - transform.position.x < -2.5f)
                Temp_Pos.x = Boss_Start_Character_Pos.x + 2.5f;
            
            if (Boss_Start_Character_Pos.y - transform.position.y > 4.7f)
                Temp_Pos.y = Boss_Start_Character_Pos.y - 4.7f;
            else if (Boss_Start_Character_Pos.y - transform.position.y < -4.7f)
                Temp_Pos.y = Boss_Start_Character_Pos.y + 4.7f;

            transform.position = Temp_Pos;
        }
        else
        {
            if (transform.position.x < -X)
                All_Obj_Move(new Vector3(X, 0f, 0f));
            else if (transform.position.x > X)
                All_Obj_Move(new Vector3(-X, 0f, 0f));
            else if (transform.position.y > 6.5f)
                All_Obj_Move(new Vector3(0f, -Y, 0f));
            else if (transform.position.y < -6.5f)
                All_Obj_Move(new Vector3(0f, Y, 0f));
        }
    }

    private void All_Obj_Move(Vector3 _Vec)
    {
        Character_Manager.Instance.Get_Character().transform.position += _Vec;
        Joystick.Instance.Joystick_Move(_Vec);
        Bullet_Manager.Instance.Bullet_Obj_Move(_Vec);
        Monster_Manager.Instance.Monster_Obj_Move(_Vec);
        Item_Manager.Instance.Item_Obj_Move(_Vec);
    }

    public void Set_Character_Info(GameManager.CHARACTER_INFO _Info)
    {
        Character_Info = _Info;

        Pos_Update();
    }

    private void Pos_Update()
    {
        transform.position = Character_Info.Pos;
    }

    public void Set_Bullet_Delay_Time(GameManager.BULLET_TYPE _Type, float _Delay_Time)
    {
        Bullet_Delay_Time.TryAdd(_Type, _Delay_Time);
        Bullet_Count.TryAdd(_Type, 0f);
        Bullet_Att_Per.TryAdd(_Type, 1f);
    }

    protected void Bullet_Check()
    {
        GameManager.BULLET_TYPE Temp;
        foreach(KeyValuePair<GameManager.BULLET_TYPE, float> _B_Time in Bullet_Delay_Time)
        {
            Temp = _B_Time.Key;
            Bullet_Count[Temp] += Time.deltaTime;
            if (Bullet_Count[Temp] > Bullet_Delay_Time[Temp])
            {
                Bullet_Count[Temp] = 0f;
                Bullet_Fire(Temp);
            }
        }
    }

    protected void Bullet_Fire(GameManager.BULLET_TYPE _Type) 
    {
        switch (_Type)
        {
            case GameManager.BULLET_TYPE.BULLET_NORMAL:
                Bullet_Normal_Fire();
                break;
            case GameManager.BULLET_TYPE.BULLET_CHASE:
                Bullet_Chase_Fire();
                break;
        }
    }

    protected void Bullet_Normal_Fire()
    {
        if (Move_Vector == Vector3.zero)
            Move_Vector = new Vector3(0f, 1f, 0f);
        Vector3 Bullet_Dir = Move_Vector;
        Vector3 Bullet_Pos = transform.position + (Move_Vector * Bullet_Dis);
        int Att = (int)((float)Character_Info.Obj_Info.Att * Bullet_Att_Per[GameManager.BULLET_TYPE.BULLET_NORMAL]);
        Bullet_Manager.Instance.Bullet_Normal_Fire(new GameManager.BULLET_INFO(Bullet_Pos, Bullet_Dir, Att));
    }

    protected void Bullet_Chase_Fire()
    {
        int Deg=Random.Range(0,360);
        Vector3 Bullet_Dir = new Vector3(Mathf.Cos(Deg * Mathf.Deg2Rad), Mathf.Sin(Deg * Mathf.Deg2Rad), 0f);
        Vector3 Bullet_Pos = transform.position + (Bullet_Dir * Bullet_Dis);
        int Att = (int)((float)Character_Info.Obj_Info.Att * Bullet_Att_Per[GameManager.BULLET_TYPE.BULLET_CHASE]);
        Bullet_Manager.Instance.Bullet_Chase_Fire(new GameManager.BULLET_INFO(Bullet_Pos, Bullet_Dir, Att));
    }

    public void Hp_Down(int _Att)
    {
        if (Hp_Down_Check)
        {
            Hp_Down_Check = false;

            Character_Info.Obj_Info.Hp -= _Att;
            Hp_Ui_Update();
            if (Character_Die_Check())
            {
                Timer_Manager.Instance.Active = false;  
            }
        }
    }

    public void Hp_Ui_Update()
    {
        float Rate = (float)Character_Info.Obj_Info.Hp / (float)Character_Info.Obj_Info.Max_Hp;

        Vector3 Vec_Temp = Hp_Bar_Background.transform.localScale;

        Vec_Temp.x = Vec_Temp.x * Rate;

        Hp_Bar.transform.localScale = Vec_Temp;

        Debug.Log(Character_Info.Obj_Info.Hp);
        Debug.Log(Character_Info.Obj_Info.Max_Hp);
    }

    protected bool Character_Die_Check()
    {
        return Character_Info.Obj_Info.Hp < 0;
    }

    public void Exp_Gain(float _Exp)
    {
        Character_Info.Exp += _Exp;

        Exp_Ui_Update();
    }

    protected void Exp_Ui_Update()
    {
        float rate;

        rate = Character_Info.Exp / Character_Info.Max_Exp;
        if (rate > 1f)
            rate = 1f;

        Vector3 Vec_Temp = Exp_Bar_Background.transform.localScale;

        Vec_Temp.x = Vec_Temp.x * rate;

        Exp_Bar.transform.localScale = Vec_Temp;

        if(Character_Info.Exp>=Character_Info.Max_Exp)
        {
            Level_Up();            
        }
    }

    protected void Level_Up()
    {
        Character_Info.Lv++;

        Character_Info.Exp -= Character_Info.Max_Exp;

        Character_Info.Max_Exp = Character_Info.Lv * 100;

        Exp_Ui_Update();

        Level_Up_Manager.Instance.Level_Up();
    }

    public void Hp_Up()
    {
        Character_Info.Obj_Info.Hp += (int)((float)Character_Info.Obj_Info.Max_Hp * 0.3f);

        if (Character_Info.Obj_Info.Hp > Character_Info.Obj_Info.Max_Hp)
            Character_Info.Obj_Info.Hp = Character_Info.Obj_Info.Max_Hp;

        Hp_Ui_Update();
    }

    public void Max_Hp_Up()
    {
        Character_Info.Obj_Info.Hp += (int)((float)Character_Info.Obj_Info.Max_Hp * 0.3f);
        Character_Info.Obj_Info.Max_Hp += (int)((float)Character_Info.Obj_Info.Max_Hp * 0.3f);

        Hp_Ui_Update();
    }

    public void Bullet_Dmg_Up(GameManager.BULLET_TYPE _Type)
    {
        Bullet_Att_Per[_Type] += 0.5f;
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            transform.Translate(new Vector3(-10f, 0f, 0f));

        if (Input.GetKeyDown(KeyCode.W))
            transform.Translate(new Vector3(10f, 0f, 0f));

        if (Input.GetKeyDown(KeyCode.A))
            Character_Manager.Instance.Get_Character().transform.Translate(new Vector3(-10f, 0f, 0f));

        if (Input.GetKeyDown(KeyCode.S))
            Character_Manager.Instance.Get_Character().transform.Translate(new Vector3(10f, 0f, 0f));
    }

    public void Set_Boss_Start()
    {
        Boss_Start_Character_Pos = transform.position;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        Exp_Bar.SetActive(false);
        Exp_Bar_Background.SetActive(false);
    }

    protected void Hp_Down_Check_Count()
    {
        Hp_Down_Count += Time.deltaTime;
        if(Hp_Down_Count > Hp_Down_Delay)
        {
            Hp_Down_Count = 0f;
            Hp_Down_Check = true;
        }
    }

    protected float Speed;

    protected GameManager.CHARACTER_INFO Character_Info;

    public GameObject Character_Dir;
    protected float Dir_Dis;
    protected float Bullet_Dis;

    protected Vector3 Move_Vector;

    protected Dictionary<GameManager.BULLET_TYPE, float> Bullet_Delay_Time;
    protected Dictionary<GameManager.BULLET_TYPE, float> Bullet_Count;
    protected Dictionary<GameManager.BULLET_TYPE, float> Bullet_Att_Per;

    public GameObject Hp_Bar;
    public GameObject Hp_Bar_Background;

    public GameObject Exp_Bar;
    public GameObject Exp_Bar_Background;

    public Vector3 Boss_Start_Character_Pos;

    protected float Hp_Down_Delay;
    protected float Hp_Down_Count;
    protected bool Hp_Down_Check;
}
