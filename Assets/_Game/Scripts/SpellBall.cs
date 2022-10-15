using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour
{

    public float spellMoveSpeed = 5f;

    private GameObject originGameObject;
    private Vector3 spellDirection;
    public Vector3 rotation;

    public GameObject OriginGameObject { get => originGameObject; set => originGameObject = value; }

    // Start is called before the first frame update

    void Start()
    {
        transform.parent = GameObject.Find("SpellBallParent").transform;
        spellDirection = originGameObject.transform.forward;

        Invoke("Disable", 0.8f);

        GetComponent<ParticleSystem>().Play();
        ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
        em.enabled = true;
    }


    // Update is called once per frames
    void Update()
    {
        SpellMovement();
    }

    private void SpellMovement()
    {


        Vector3 spellVelocity = spellDirection * spellMoveSpeed;
        transform.Translate(spellVelocity * Time.deltaTime, Space.World);
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
        }
        Disable();
    }

    private void Disable()
    { 
       Destroy(gameObject);
    }
}
