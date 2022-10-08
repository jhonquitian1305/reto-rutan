using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour
{

    public float spellMoveSpeed = 20f;

    private Vector3 spellDirection;
    private GameObject character;
    // Start is called before the first frame update

    private void OnEnable()
    {
        character = GameObject.FindWithTag("Player");
        if (character.transform.rotation.y >= 0)    
        {
            spellDirection = Vector3.right;
        }
        else
        {
            spellDirection = Vector3.left;
        }
        Invoke("Disable", 2);
    }


    void Start()
    {
        

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
        }
        gameObject.SetActive(false);
    }

    private void Disable()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
