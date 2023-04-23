using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class My_Scene_Manager : MonoBehaviour
{
    private static My_Scene_Manager instance = null;
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

    public static My_Scene_Manager Instance
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

    public void Game_Start_Button()
    {
        StartCoroutine(Load_Stage1_Scene());
    }

    private IEnumerator Load_Stage1_Scene()
    {
        AsyncOperation Oper = SceneManager.LoadSceneAsync("Stage");
        while (!Oper.isDone)
        {
            yield return null;
        }

    }

    public void Gameover()
    {
        SceneManager.LoadScene("Lobby");
    }
}
