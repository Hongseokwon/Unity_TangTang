using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
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
    public enum CHARACTER_TYPE
    {
        CHARACTER_CAT
    }

    public enum MONSTER_TYPE
    {
        MONSTER_CAT , MONSTER_SOLDIER , MONSTER_BOSS1
    }

    public enum BULLET_TYPE
    {
        BULLET_NORMAL , BULLET_CHASE , MONSTER_BULLET_NORMAL
    }

    public enum ITEM_TYPE
    {
        ITEM_EXP, ITEM_EXP2
    }

    public enum LEVEL_UP_LIST
    {
        ATT,MAX_HP,NORMAL_BULLET,CHASE_BULLET,ITEM_MAGNET,HP
    }
    public struct OBJ_INFO
    {
        public OBJ_INFO(int _Max_Hp, int _Att, int _Def)
        {
            Hp = _Max_Hp;
            Max_Hp = _Max_Hp;
            Att = _Att;
            Def = _Def;
        }
        public int Hp;
        public int Max_Hp;
        public int Att;
        public int Def;
    }

    public struct CHARACTER_INFO
    {
        public CHARACTER_INFO(OBJ_INFO _Obj_Info,Vector3 _Pos)
        {
            Pos = _Pos;
            Obj_Info = _Obj_Info;
            Lv = 1;
            Exp = 0;
            Max_Exp = 100;
        }

        public OBJ_INFO Obj_Info;
        public Vector3 Pos;

        public float Exp;
        public float Max_Exp;
        public int Lv;
    }

    public struct MONSTER_INFO
    {
        public MONSTER_INFO(OBJ_INFO _Obj_Info)
        {
            Obj_Info = _Obj_Info;
        }

        public OBJ_INFO Obj_Info;
    }

    public struct BULLET_INFO
    {
        public BULLET_INFO(Vector3 _Pos, Vector3 _Dir, int _Att)
        {
            Pos = _Pos;
            Dir = _Dir;
            Att = _Att;
        }
        public Vector3 Pos;
        public Vector3 Dir;
        public int Att;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Get_Random_Monster_Pos(float _Dis)
    {
        float Deg = Random.Range(0, 360);
        return Character_Manager.Instance.Get_Character().transform.position + new Vector3(_Dis * Mathf.Cos(Deg), _Dis * Mathf.Sin(Deg), 0f);
    }
}
