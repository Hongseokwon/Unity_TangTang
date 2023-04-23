using UnityEngine;

public class Bullet_Chase : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        Speed = 4f;
        Deg_Speed = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer_Manager.Instance.Active)
            Bullet_Move();
    }

    public override void Bullet_Move()
    {
        
        
        if (Target_Obj == null || !Target_Obj.activeSelf)
            Target_Obj = Monster_Manager.Instance.Get_Close_Monster(transform.position);

        float V_Vec_Deg = Mathf.Rad2Deg * Mathf.Atan2(Move_Vector.y, Move_Vector.x) - 90f;
        Vector3 V_Vec = new Vector3(Mathf.Cos(V_Vec_Deg * Mathf.Deg2Rad), Mathf.Sin(V_Vec_Deg * Mathf.Deg2Rad), 0f);

;
        if (Target_Obj == null)
            transform.position += Move_Vector * Speed * Time.deltaTime;
        else
        {
            Vector3 Target_Vec = Target_Obj.transform.position - transform.position;
            float Move_Deg = Mathf.Rad2Deg * Mathf.Atan2(Move_Vector.y, Move_Vector.x);

            if (0 < Vector3.Dot(V_Vec, Target_Vec)) 
            {
                Move_Deg -= (Deg_Speed * Time.deltaTime);
                Move_Vector.x = Mathf.Cos(Move_Deg * Mathf.Deg2Rad);
                Move_Vector.y = Mathf.Sin(Move_Deg * Mathf.Deg2Rad);
                Move_Vector.Normalize();
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * (Mathf.Atan2(Move_Vector.y, Move_Vector.x)));
                transform.position += Move_Vector * Speed * Time.deltaTime;
            }
            else
            {
                Move_Deg += (Deg_Speed * Time.deltaTime);
                Move_Vector.x = Mathf.Cos(Move_Deg * Mathf.Deg2Rad);
                Move_Vector.y = Mathf.Sin(Move_Deg * Mathf.Deg2Rad);
                Move_Vector.Normalize();
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * (Mathf.Atan2(Move_Vector.y, Move_Vector.x)));
                transform.position += Move_Vector * Speed * Time.deltaTime;
            }
        }
        
    }

    public override void Fire(GameManager.BULLET_INFO _Info)
    {
        transform.position = _Info.Pos;
        Att = _Info.Att;
        Move_Vector = _Info.Dir;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * (Mathf.Atan2(Move_Vector.y, Move_Vector.x)));
        Target_Obj = Monster_Manager.Instance.Get_Close_Monster(transform.position);

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().Bullet_Hit(Att);
            gameObject.SetActive(false);
        }
    }

    private float Deg_Speed;
    public GameObject Target_Obj;
}
