using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float Speed;
    public float rot;

    public Transform Boltpos;
    [SerializeField]
    private BoltPool Boltp;

    public float FireRate;
    private float currentFirerate;

    [SerializeField]
    private int MaxHP;
    private int currentHP;
    private SoundController soundControl;
    private GameController control;
    private MainUIController uicontrol;

    private int boltSize;
    private float powerUPTimer;

	// Use this for initialization


	void Awake () {
        rb = GetComponent<Rigidbody>();
        currentFirerate = 0;
        boltSize = 1;
        soundControl = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uicontrol = GameObject.FindGameObjectWithTag("UI").GetComponent<MainUIController>();

    }

    private void OnEnable()
    {
        currentHP = MaxHP;
        uicontrol.SetPlayerHP(currentHP);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            currentHP--;
            uicontrol.SetPlayerHP(currentHP);

            if(currentHP <= 0)
            {
                gameObject.SetActive(false);
                soundControl.PlayEffectSound(eSoundEffectClip.expPlayer);
                GameObject effect = control.GetEffect(eEffectType.expPlayer);
                effect.transform.position = transform.position;
                effect.SetActive(true);
                control.GameOver();
            }
            
            
        }
    }

    public void PowerUP()
    {
        boltSize = 3;
        powerUPTimer += 3;
    }

    // Update is called once per frame
    void Update () {
        float horizontal = Input.GetAxis("Horizontal") * Speed;
        float vertical = Input.GetAxis("Vertical") * Speed;

        rb.velocity = new Vector3(horizontal, 0, vertical);
        rb.rotation = Quaternion.Euler(0, 0, horizontal * -rot);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, -5, 5),
                                  0,
                                  Mathf.Clamp(rb.position.z, -4, 10));

        currentFirerate -= Time.deltaTime;
       


        if (Input.GetButton("Shoot")&& currentFirerate<=0)
        {
            Bolt temp = Boltp.GetFromPool();
            temp.transform.position = Boltpos.position;
            temp.transform.localScale = Vector3.one * boltSize;
            temp.gameObject.SetActive(true);
            currentFirerate = FireRate;
            soundControl.PlayEffectSound(eSoundEffectClip.shotPlayer);
        }
       if( powerUPTimer<=0)
        {
            boltSize = 1;
            powerUPTimer = 0;
        }
        else
        {
            powerUPTimer -= Time.deltaTime;
        }
	}
}
