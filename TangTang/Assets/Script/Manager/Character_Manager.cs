using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Manager : MonoBehaviour
{

    private static Character_Manager instance = null;
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

    public static Character_Manager Instance
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
        Bullet_Manager.Instance.Bullet_Init();
        Stage_Manager.Instance.Stage_Load();

        Main_Camera.GetComponent<Camera_Controll>().Character = Player_Character;

        Character_Att_Per = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Character_Load()
    {
        Player_Character = Prefab_Manager.Instance.Character_Create(GameManager.CHARACTER_TYPE.CHARACTER_CAT);
        
        Character_Info_Temp = new GameManager.CHARACTER_INFO(new GameManager.OBJ_INFO(100, 10, 1), new Vector3(0f, 0f, 0f));
        Player_Character.GetComponent<Character>().Set_Character_Info(Character_Info_Temp);
    }

    public void Bullet_Load()
    {
        Bullet_Manager.Instance.Bullet_Prefab_Create(GameManager.BULLET_TYPE.BULLET_NORMAL, 100);
        Bullet_Manager.Instance.Bullet_Prefab_Create(GameManager.BULLET_TYPE.BULLET_CHASE, 100);

        Player_Character.GetComponent<Character>().Set_Bullet_Delay_Time(GameManager.BULLET_TYPE.BULLET_NORMAL, 1f);
        Player_Character.GetComponent<Character>().Set_Bullet_Delay_Time(GameManager.BULLET_TYPE.BULLET_CHASE, 2f);

        Level_Up_Manager.Instance.Add_List(GameManager.LEVEL_UP_LIST.NORMAL_BULLET);
        Level_Up_Manager.Instance.Add_List(GameManager.LEVEL_UP_LIST.CHASE_BULLET);
        Level_Up_Manager.Instance.Add_List(GameManager.LEVEL_UP_LIST.ATT);
        Level_Up_Manager.Instance.Add_List(GameManager.LEVEL_UP_LIST.HP);
        Level_Up_Manager.Instance.Add_List(GameManager.LEVEL_UP_LIST.MAX_HP);
        Level_Up_Manager.Instance.Add_List(GameManager.LEVEL_UP_LIST.ITEM_MAGNET);
    }
    public GameObject Get_Character()
    {
        return Player_Character;
    }
    public void Player_Move(Vector3 _Move_Vector)
    {
        Player_Character.GetComponent<Character>().Player_Move(_Move_Vector);
    }

    public void Att_Up()
    {
        Character_Att_Per += 0.5f;
    }

    public void Max_Hp_Up()
    {
        Player_Character.GetComponent<Character>().Max_Hp_Up();
    }

    public void Bullet_Dmg_Up(GameManager.BULLET_TYPE _Type)
    {
        Player_Character.GetComponent<Character>().Bullet_Dmg_Up(_Type);
    }

    public void Hp_Up()
    {
        Player_Character.GetComponent<Character>().Hp_Up();
    }


    protected GameManager.CHARACTER_INFO Character_Info_Temp;

    private GameObject Player_Character;

    public GameObject Main_Camera;

    private float Character_Att_Per;
    private float Item_Magnet_Dis_Per;
}
