using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!Stage_Manager.Instance.Boss_Stage_Active)
        {
            Camera_Pos = Character.transform.position;
            Camera_Pos.z = -10f;
            gameObject.transform.position = Camera_Pos;
        }
    }

    public GameObject Character;

    private Vector3 Camera_Pos;
}
