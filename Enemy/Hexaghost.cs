using System.Collections.Generic;
using UnityEngine;


public class Hexaghost : Enemy
{
    NextActionUIBoss nextActionUIBoss;

    private new void Start()
    {
        base.Start();
        Name.text = enemyName.ToString();
        nextActionUIBoss = GetComponent<NextActionUIBoss>();

    }

    public int spAttack;
    public override void PerformAction(Action action)
    {
        switch (action)
        {
            case Action.Attack:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_AttackTrigger);
                break;
            case Action.Shield:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_ShieldTrigger);
                break;
            case Action.Effect:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_EffectToPlayerTrigger);
                break;
            case Action.SelfEffect:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_EffectToSelfTrigger);
                break;
            case Action.Elemental:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_ElementalAtkTrigger);
                break;
            case Action.BossAttack:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_BossAtkTrigger);
                break;
            case Action.BossSp:
                if (animator != null) animator.SetTrigger(KeyWordManager.int_SpecalAttackTrig);
                break;
        }
    }

    public override void UpdateNextActionUI()
    {

        nextActionUIBoss.UpdateNextAction();
    }
}