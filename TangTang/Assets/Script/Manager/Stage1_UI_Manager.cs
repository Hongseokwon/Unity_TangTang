using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_UI_Manager : MonoBehaviour
{
    private static Stage1_UI_Manager instance = null;
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

    public static Stage1_UI_Manager Instance
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

    public void Gameover_Button()
    {
        My_Scene_Manager.Instance.Gameover();
    }

    public GameObject Game_Over_Obj;
}
