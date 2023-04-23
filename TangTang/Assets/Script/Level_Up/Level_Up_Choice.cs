using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_Up_Choice : MonoBehaviour
{
    private void Awake()
    {
        Level_Up_List = new GameManager.LEVEL_UP_LIST[3];
        Level_Up_Cnt = new Dictionary<GameManager.LEVEL_UP_LIST, int>();
        Random_List = new List<GameManager.LEVEL_UP_LIST>();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_List()
    {
        int n = Random_List.Count;
        if (n > 3)
        {
            int cnt = (n - 3) * 3;

            for (int i = 0; i < cnt; ++i)
            {
                int a, b;
                a = Random.Range(0, n);
                b = Random.Range(0, n);

                GameManager.LEVEL_UP_LIST Temp;

                Temp = Random_List[a];
                Random_List[a] = Random_List[b];
                Random_List[b] = Temp;
            }
            for (int i = 0; i < 3; ++i)
            {
                Level_Up_List[i] = Random_List[i];
                Choice_Button_Text[i].GetComponentInChildren<TMP_Text>().text = Get_Item_String(Random_List[i]);
                Choice_Button_Text[i].SetActive(true);
            }
        }
        else if (n == 0)
        {
            Choice_Button_Text[0].SetActive(true);
            Choice_Button_Text[1].SetActive(false);
            Choice_Button_Text[2].SetActive(false);
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                if (i < n)
                {
                    Level_Up_List[i] = Random_List[i];
                    Choice_Button_Text[i].GetComponentInChildren<TMP_Text>().text = Get_Item_String(Random_List[i]);
                    Choice_Button_Text[i].SetActive(true);
                }
                else
                {
                    Choice_Button_Text[i].SetActive(false);
                }
            }
        }
    }

    public void Level_Up_List_Button1() 
    {
        Debug.Log("111");
        Level_Up_List_Active(Random_List[0]);
        Timer_Manager.Instance.Active = true;
        gameObject.SetActive(false);
    }
    public void Level_Up_List_Button2() 
    {
        Debug.Log("222");
        Level_Up_List_Active(Random_List[1]);
        Timer_Manager.Instance.Active = true;
        gameObject.SetActive(false);
    }
    public void Level_Up_List_Button3() 
    {
        Debug.Log("333");
        Level_Up_List_Active(Random_List[2]);
        Timer_Manager.Instance.Active = true;
        gameObject.SetActive(false);
    }

    private void Level_Up_List_Active(GameManager.LEVEL_UP_LIST _Type)
    {
        switch (_Type)
        {
            case GameManager.LEVEL_UP_LIST.ATT:
                Level_Up_Att();
                break;
            case GameManager.LEVEL_UP_LIST.MAX_HP:
                Level_Up_Max_Hp();
                break;
            case GameManager.LEVEL_UP_LIST.NORMAL_BULLET:
                Level_Up_Normal_Bullet();
                break;
            case GameManager.LEVEL_UP_LIST.CHASE_BULLET:
                Level_Up_Chase_Bullet();
                break;
            case GameManager.LEVEL_UP_LIST.ITEM_MAGNET:
                Level_Up_Magnet();
                break;
            case GameManager.LEVEL_UP_LIST.HP:
                Level_Up_Hp();
                break;
        }
    }

    private void Level_Up_Att()
    {
        Character_Manager.Instance.Att_Up();
    }
    private void Level_Up_Max_Hp()
    {
        Character_Manager.Instance.Max_Hp_Up();
    }
    private void Level_Up_Normal_Bullet()
    {
        Character_Manager.Instance.Bullet_Dmg_Up(GameManager.BULLET_TYPE.BULLET_NORMAL);
    }
    private void Level_Up_Chase_Bullet()
    {
        Character_Manager.Instance.Bullet_Dmg_Up(GameManager.BULLET_TYPE.BULLET_CHASE);
    }
    private void Level_Up_Magnet()
    {
        Item_Manager.Instance.Magnet_Dis_Increase();
    }
    private void Level_Up_Hp()
    {
        Character_Manager.Instance.Hp_Up();
    }

    private string Get_Item_String(GameManager.LEVEL_UP_LIST _Type)
    {
        switch (_Type)
        {
            case GameManager.LEVEL_UP_LIST.ATT:
                return "Att Dmg + 50%";
            case GameManager.LEVEL_UP_LIST.MAX_HP:
                return "Max Hp + 30%";
            case GameManager.LEVEL_UP_LIST.NORMAL_BULLET:
                return "Normal Bullet Dmg + 100%";
            case GameManager.LEVEL_UP_LIST.CHASE_BULLET:
                return "Chase Bullet Dmg + 100%";
            case GameManager.LEVEL_UP_LIST.ITEM_MAGNET:
                return "Magnet Range + 50%";
            case GameManager.LEVEL_UP_LIST.HP:
                return "Hp + 30%";
        }

        return null;
    }

    public void Add_List(GameManager.LEVEL_UP_LIST _Type)
    {
        if(Level_Up_Cnt.TryAdd(_Type, 0))
        {
            Random_List.Add(_Type);
        }
    }

    public GameObject[] Choice_Button_Text;

    private GameManager.LEVEL_UP_LIST[] Level_Up_List;
    private Dictionary<GameManager.LEVEL_UP_LIST, int> Level_Up_Cnt;
    private List<GameManager.LEVEL_UP_LIST> Random_List;
}
