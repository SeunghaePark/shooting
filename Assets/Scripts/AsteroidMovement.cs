using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float angularSpeed
                    , Speed;
    [SerializeField]
    private int ScoreValue;
    private GameController control;
    private SoundController soundControl;

	// Use this for initialization
	void Awake () {
        // 다시 시작할때 새 대상이 나오는것처럼 만들기 위해 start 가 아닌 awake 에 새팅을 한다. 
        // start 에 만들경우 새로 시작할때마다 같은 대상의 변화를 가지고 나타나기 때문에

        rb = GetComponent<Rigidbody>();

     
            control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            soundControl = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
     

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
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            soundControl.PlayEffectSound(eSoundEffectClip.expAsteroid);
            GameObject effect = control.GetEffect(eEffectType.expAsteroid);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
        if(other.gameObject.CompareTag("PlayerBolt"))
        {
          

            other.gameObject.SetActive(false);
            soundControl.PlayEffectSound(eSoundEffectClip.expAsteroid);
            GameObject effect = control.GetEffect(eEffectType.expAsteroid);
            effect.transform.position = transform.position;
            effect.SetActive(true);

            control.AddScore(ScoreValue);
            gameObject.SetActive(false);
        }
    }

    public void Bomb()
    {
      
            control.AddScore(ScoreValue);
            gameObject.SetActive(false);

            soundControl.PlayEffectSound(eSoundEffectClip.expEnemy);
            GameObject effect = control.GetEffect(eEffectType.expEnemy);
            effect.transform.position = transform.position;
            effect.SetActive(true);
       

    }

    // Update is called once per frame
    void Update () {
		
	}
}
