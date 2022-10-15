using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Transform personaje;
    public int vida;
    public float playerSpeed;
    public float playerRotate;
    public float jumpSpeed;
    public bool playerMove = false;  
    public bool chekGround = true; 
    public Transform chkGround;
    private Rigidbody rb;
    private Vector3 displacement;
    //public Slider healthBar;
    //public GameObject back;
    //public GameObject img;
    
    void Awake()
    {
        rb=GetComponent<Rigidbody>();
    }
    void Start()
    {
        vida = 5;
    }

    void FixedUpdate()
    {
        float mh = Input.GetAxis("Horizontal");
        PlayerMove(mh);
        PlayerJumper(); 
    }
    void Update()
    {
        //healthBar.value = vida;
        //if (vida <= 0)
        //{
        //    FindObjectOfType<SoundManager>().Play("muerte");
        //    back.SetActive(false);
        //    img.SetActive(true);
        //    Destroy(gameObject);
        //}
        
    }
    void PlayerMove(float mh)
    {
        displacement.Set(mh,0f,0f);
        displacement = displacement.normalized * playerSpeed * Time.deltaTime;
        rb.MovePosition (transform.position+displacement);

        if(mh != 0f)
        {
            PlayerRotate(mh);
        }

        bool playerRun = mh != 0f;
        if (playerRun)
        {
            playerMove = true;
        }
        else
        {
            playerMove = false;
        }

    }
    
  void PlayerRotate(float mh)
    {
        float interpolation = playerRotate * Time.deltaTime;
        Vector3 targetDirection = new Vector3 (mh,0f,0f);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection,Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rb.rotation,targetRotation,interpolation);
        rb.MoveRotation(newRotation);

    }

    void PlayerJumper()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;

        if(Input.GetButton("Jump") && chekGround)
        {
            rb.velocity = new Vector3(0f,jumpSpeed,0f);
            chekGround = false;
        }
        if(Physics.Raycast(chkGround.position, dwn, out hit, 0.2f) && hit.collider.CompareTag("Ground"))
        {
            chekGround = true;
        }
        else
        {
            chekGround = false; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Bullet")
        //{
        //    FindObjectOfType<SoundManager>().Play("herida");
        //    personaje.transform.position = respawnPoint.transform.position;
        //    vida -= 1;
        //    healthBar.value = vida;
        //}
        //if (other.gameObject.tag == "muerteSubita")
        //{
        //    FindObjectOfType<SoundManager>().Play("herida");
        //    personaje.transform.position = respawnPoint.transform.position;
        //    vida -= 1;
        //    healthBar.value = vida;
        //}
    }
}