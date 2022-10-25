using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAnimController
{
    void Idle();
    void WalkBack();
    void WalkAnim();
    void GetHitAnim();
    void EndGetHitAnim();
    void DieAnim();
    void AttackAnim();
    void EndAttackAnim();
    void SummonAnim();
}
