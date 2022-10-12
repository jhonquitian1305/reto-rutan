using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour
{

    public float spellMoveSpeed = 20f;


    private GameObject originGameObject;
    private Vector3 spellDirection;

    public GameObject OriginGameObject { get => originGameObject; set => originGameObject = value; }

    // Start is called before the first frame update

    void Start()
    {
        transform.parent = GameObject.Find("SpellBallParent").transform;
        if (OriginGameObject.transform.localEulerAngles.y >= 0 && OriginGameObject.transform.localEulerAngles.y < 180)
        {
            spellDirection = Vector3.right;
        }
        else
        {
            spellDirection = Vector3.left;
        }
        Invoke("Disable", 5);
    }


    // Update is called once per frame
    void Update()
    {
        SpellMovement();
    }

    private void SpellMovement()
    {


        Vector3 spellVelocity = spellDirection * spellMoveSpeed;
        transform.Translate(spellVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            IDamage enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(50);
            }
            Disable();
        }
        if (collision.transform.CompareTag("ground"))
        {
            Disable();
        }
    }

    private void Disable()
    {

       Destroy(gameObject);
        
    }
}
