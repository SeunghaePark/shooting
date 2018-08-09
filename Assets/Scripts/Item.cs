using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    LifeUP, PowerUP, EnemyPowerUP
};

public class Item : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float angularSpeed
                    , Speed;
    [SerializeField]
    eItemType itemType;

    private GameController control;

    // Use this for initialization
    void Awake()
    {
        // 다시 시작할때 새 대상이 나오는것처럼 만들기 위해 start 가 아닌 awake 에 새팅을 한다. 
        // start 에 만들경우 새로 시작할때마다 같은 대상의 변화를 가지고 나타나기 때문에

        rb = GetComponent<Rigidbody>();
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();


    }
    private void OnEnable()
    {
        rb.angularVelocity = Random.onUnitSphere * angularSpeed;
        rb.velocity = Vector3.back * Speed;

        //back 으로 해야 (0,0,-z)  값에만 변화가 있으므로 회전해도 다른 방향으로 가지 않는다.
    }

    private void OnTriggerEnter(Collider other)
    {
        //플레이어와 부딛혔을때 꺼지는
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            switch (itemType)
            {
                case eItemType.LifeUP:
                    control.AddLife();
                    break;
                case eItemType.PowerUP:
                    control.PowerUP();
                    break;
                case eItemType.EnemyPowerUP:
                    control.EnemyPowerUP();
                    break;
                default:
                    Debug.Log("Item Type error");
                    break;
            }
            
        }
    
    }
    // Update is called once per frame
    public void Update()
    {

    }
}
