using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour
{

    private float spellMoveSpeed = 7f;
    private float spellRange = 7f;
    private float spellDamage = 10f;
    public float criticDamageMultiplier = 1.5f;
    public ElementType spellElementType;
    private GameObject originGameObject;

    private Vector3 spellDirection;

    public float SpellMoveSpeed { get => spellMoveSpeed; set => spellMoveSpeed = value; }
    public float SpellRange { get => spellRange; set => spellRange = value; }
    public float SpellDamage { get => spellDamage; set => spellDamage = value; }
    public GameObject OriginGameObject { get => originGameObject; set => originGameObject = value; }
    public Vector3 SpellDirection { get => spellDirection; set => spellDirection = value; }

    // Start is called before the first frame update

    void Start()
    {
        //transform.parent = GameObject.Find("SpellBallParent").transform;
        float timeToDestroy = (spellRange / spellMoveSpeed);

        Invoke(nameof(Disable), timeToDestroy);

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
            EnemyHealthSystem enemyHealth = collision.gameObject.GetComponent<EnemyHealthSystem>();
            if (enemyHealth != null)
            {
                if(enemyHealth.weaknessElementType == spellElementType)
                {
                    Debug.Log("critico");
                    enemyHealth.UpdateHealth(-spellDamage*criticDamageMultiplier);
                }
                else
                {
                    enemyHealth.UpdateHealth(-spellDamage);
                }
            }
        }
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealthSystem playerHealth = collision.gameObject.GetComponent<PlayerHealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.UpdateHealth(-spellDamage);
            }
        }
        Disable();
    }

    private void Disable()
    { 
       Destroy(gameObject);

    }
}
