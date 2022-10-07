using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{

    public float spellMoveSpeed = 20f;

    private Vector3 spellDirection;
    // Start is called before the first frame update
    void Start()
    {
        GameObject character = GameObject.FindWithTag("Player");
        if (character.transform.rotation.y >= 0)
        {
            spellDirection = Vector3.right;
        }
        else
        {
            spellDirection = Vector3.left;
        }

        Physics.IgnoreCollision(GetComponent<Collider>(), character.GetComponent<Collider>());
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
}
