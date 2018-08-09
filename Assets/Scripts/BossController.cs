using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    private Rigidbody rb;
    public float Speed;

    private GameController control;
    private SoundController soundControl;
    private MainUIController UIcontrol;

    [SerializeField]
    private int ScoreValue;

    private bool AttackStart;

    [SerializeField]
    private Transform Boltpos;
    private BoltPool Boltp;

    [SerializeField]
    private int MaxHP;
    private int currentHP;
  

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //Boltp = GameObject.FindGameObjectWithTag("EnemyBoltPool").GetComponent<BoltPool>();
        soundControl = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UIcontrol = GameObject.FindGameObjectWithTag("UI").GetComponent<MainUIController>();
    }

    public void SetBoltPoop(BoltPool boltp)
    {
        Boltp = boltp;
    }

    private void OnEnable()
    {
        AttackStart = false;
        currentHP = MaxHP;
        UIcontrol.SetHP((float)currentHP / MaxHP);
        StartCoroutine(BossPhase());
    }



    private IEnumerator BossPhase()
    {
        //등장
        rb.velocity = Vector3.back * Speed;

        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        WaitForSeconds One = new WaitForSeconds(1);
        WaitForSeconds TwoPointFive = new WaitForSeconds(2.5f);
        WaitForSeconds PointFive = new WaitForSeconds(0.5f);
        WaitForSeconds Five = new WaitForSeconds(5);

        Coroutine AutoShot;

        while (rb.position.z > 12.2f)
        {
            yield return pointOne;
        }
        rb.velocity = Vector3.zero;
        yield return One;
        AttackStart =true;
        

        //좌우가운데로 움직이게 루프 + 공격
        while (true)
        {
            rb.velocity = Vector3.left * Speed;
            AutoShot = StartCoroutine(AutoShoot());
            yield return TwoPointFive;
            StopCoroutine(AutoShot);
            rb.velocity = Vector3.zero;
            yield return PointFive;

            rb.velocity = Vector3.right * Speed;
            AutoShot = StartCoroutine(AutoShoot());
            yield return Five;
            StopCoroutine(AutoShot);
            rb.velocity = Vector3.zero;
            yield return PointFive;

            rb.velocity = Vector3.left * Speed;
            AutoShot = StartCoroutine(AutoShoot());
            yield return TwoPointFive;
            StopCoroutine(AutoShot);
            rb.velocity = Vector3.zero;
            yield return One;
        }

    }

    private IEnumerator AutoShoot()
    {
        WaitForSeconds PointFive = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return PointFive;
            Bolt bullet = Boltp.GetFromPool();
            bullet.transform.position = Boltpos.position;
            bullet.transform.rotation = Boltpos.rotation;
            bullet.gameObject.SetActive(true);
            soundControl.PlayEffectSound(eSoundEffectClip.shotEnemy);

        }
    }

    public void Bomb()
    {
        currentHP /= 2;
        UIcontrol.SetHP((float)currentHP / MaxHP);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            other.gameObject.SetActive(false);

          
            if(AttackStart)
            {
                if (currentHP > 0)
                {
                    currentHP--;
                }
                else
                {
                    control.AddScore(ScoreValue);
                    control.BossDead();
                    gameObject.SetActive(false);

                    soundControl.PlayEffectSound(eSoundEffectClip.expEnemy);
                    GameObject effect = control.GetEffect(eEffectType.expEnemy);
                    effect.transform.position = transform.position;
                    effect.SetActive(true);
                }

                UIcontrol.SetHP((float)currentHP / MaxHP);
            }

        }

    }



    // Update is called once per frame
    void Update () {
		
	}
}
