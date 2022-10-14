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
        spellDirection = originGameObject.transform.forward;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Enemy"))
        {
            IDamage enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(50);
            }
            Disable();
        }
        if (collider.gameObject.layer == 3)
        {
            Debug.Log("aa");
            Disable();
        }
    }

    private void Disable()
    {

       Destroy(gameObject);
        
    }
}
