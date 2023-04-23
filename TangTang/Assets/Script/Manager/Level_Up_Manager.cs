using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Up_Manager : MonoBehaviour
{
    private static Level_Up_Manager instance = null;
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

    public static Level_Up_Manager Instance
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Level_Up()
    {
        Timer_Manager.Instance.Active = false;

        Level_Up_Obj.GetComponent<Level_Up_Choice>().Set_List();
        Level_Up_Obj.SetActive(true);
    }

    public void Add_List(GameManager.LEVEL_UP_LIST _Type)
    {
        Level_Up_Obj.GetComponent<Level_Up_Choice>().Add_List(_Type);
    }


    public GameObject Level_Up_Obj;

}
