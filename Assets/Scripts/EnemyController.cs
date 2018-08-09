using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody rb;
    public float Speed;

    private GameController control;
    private SoundController soundControl;
    [SerializeField]
    private int ScoreValue;

    [SerializeField]
    private Transform Boltpos;
    private BoltPool Boltp;

   
    private int boltSize;
    private float powerUPTimer;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        //Boltp = GameObject.FindGameObjectWithTag("EnemyBoltPool").GetComponent<BoltPool>();
        soundControl = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void SetBoltPoop(BoltPool boltp)
    {
        Boltp = boltp;
    }

    private void OnEnable()
    {
        StartCoroutine(AutoShoot());
        rb.velocity = Vector3.back * Speed;
        StartCoroutine(AutoMovement());
    }

    private IEnumerator AutoMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1f,4f));
            float moveForce = Random.Range(2f, 3f);
            if(rb.position.x>=0)
            {
                rb.velocity += Vector3.left * moveForce;
            }
            else
            {
                rb.velocity += Vector3.right * moveForce;
            }
            float moveTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(moveTime);
            if (rb.position.x >= 0)
            {
                rb.velocity += Vector3.left * moveForce;
            }
            else
            {
                rb.velocity += Vector3.right * moveForce;
            }
            yield return new WaitForSeconds(Random.Range(0f,1f));
            if (rb.position.x >= 0)
            {
                rb.velocity += Vector3.left * moveForce;
            }
            else
            {
                rb.velocity += Vector3.right * moveForce;
            }
            yield return new WaitForSeconds(moveTime);
            if (rb.position.x >= 0)
            {
                rb.velocity += Vector3.left * moveForce;
            }
            else
            {
                rb.velocity += Vector3.right * moveForce;
            }
        }
    }

    private IEnumerator AutoShoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            Bolt bullet = Boltp.GetFromPool();
            bullet.transform.position = Boltpos.position;
            bullet.transform.rotation = Boltpos.rotation;
            bullet.gameObject.SetActive(true);
            soundControl.PlayEffectSound(eSoundEffectClip.shotEnemy);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            soundControl.PlayEffectSound(eSoundEffectClip.expEnemy);
            GameObject effect = control.GetEffect(eEffectType.expEnemy);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
        if (other.gameObject.CompareTag("PlayerBolt"))
        {
            other.gameObject.SetActive(false);
            control.AddScore(ScoreValue);
            gameObject.SetActive(false);

            soundControl.PlayEffectSound(eSoundEffectClip.expEnemy);
            GameObject effect = control.GetEffect(eEffectType.expEnemy);
            effect.transform.position = transform.position;
            effect.SetActive(true);

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

    public void EnemyPowerUP()
    {
        boltSize = 3;
        powerUPTimer += 3;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
