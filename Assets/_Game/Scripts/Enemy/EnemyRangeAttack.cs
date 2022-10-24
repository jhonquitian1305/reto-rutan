using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public float spellDamage = 10f;
    public float spellMoveSpeed = 5f;
    public float spellRange = 10f;
    public float spellCooldown = 1f;
    public GameObject spellBallPrefab;
    public Transform originPoint;
    public LayerMask enemyLayer;

    private float cooldownLeft;

    private Vector3 enemyTransformForward;

    private LayerMask spellBallLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetCooldownLeft();
        enemyTransformForward = gameObject.transform.forward;
    }

    public bool PlayerInSight()
    {
        LayerMask spellBallLayer = spellBallPrefab.layer;
        Vector3 rayOrigin = transform.position;
        rayOrigin.y = originPoint.position.y;
        Debug.DrawRay(rayOrigin, enemyTransformForward * spellRange, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, enemyTransformForward, out hit, spellRange, ~enemyLayer))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    private void SetCooldownLeft()
    {
        cooldownLeft -= Time.deltaTime;
    }

    public void RangeAttack()
    {
        if (cooldownLeft <= 0)
        {
            GameObject spellBall = Instantiate(spellBallPrefab, originPoint.position, transform.rotation);
            spellBall.GetComponent<SpellBall>().OriginGameObject = gameObject;
            spellBall.GetComponent<SpellBall>().SpellDamage = spellDamage;
            spellBall.GetComponent<SpellBall>().SpellMoveSpeed = spellMoveSpeed;
            spellBall.GetComponent<SpellBall>().SpellRange = spellRange;
            cooldownLeft = spellCooldown;
        }
    }
}
