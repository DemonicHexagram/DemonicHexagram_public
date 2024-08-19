using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;


public partial class Enemy : BaseCharacter
{
    public int maxhp;
    public int minhp;

    public string enemyName;
    public int shieldPower;
    public int attack;
    [Header("사용 효과")]
    public int[] effect;
    public int stack;
    [Header("사용 원소")]
    public Elemental elemental;
    [Header("Name")]
    public TextMeshProUGUI Name;
    public bool IsAnimating { get { return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"); } }
    public bool IsEffectAnimating { get { return GameManager.Instance.Player.EffectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"); } }
    public int currentActionIndex = 0;
    
    public List<int> actions;

    public Animator animator;

    public NextActionUI nextActionUI;

    new void Start()
    {
        base.Start();
        nextActionUI = GetComponent<NextActionUI>();
        if (nextActionUI != null)
        {
            nextActionUI.ResetNextActionUI();
        }
        Name.text = enemyName.ToString();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        int actionIndex = actions[currentActionIndex];
        PerformAction((Action)actionIndex);
        currentActionIndex = (currentActionIndex + 1) % actions.Count;
        nextActionUI.UpdateNextAction();
    }
    public Action GetNextAction()
    {
        if (actions == null || actions.Count == 0)
        {
            throw new InvalidOperationException("행동 리스트가 비어 있습니다.");
        }

        if (currentActionIndex < 0 || currentActionIndex >= actions.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(currentActionIndex), "현재 액션 인덱스가 리스트 범위를 벗어났습니다.");
        }

        return (Action)actions[currentActionIndex];
    }

    public virtual void PerformAction(Action action)
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
        }
    }
    protected override void ApplyPoisonDamage(BaseCharacter target)
    {
        if (this.activeEffects.ContainsKey(StatusEffect.Poison))
        {
            int poisonStack = activeEffects[StatusEffect.Poison];
            DamageTextManager.Instance.ShowDamageText(target.transform.position, poisonStack, Color.green);
            hp -= poisonStack;

            if (hp <= 0)
            {
                hp = 0;
                if (hp <= 0) BattleManager.Instance.EnemyDieRemove(this);
            }
        }
    }


    public override void ApplyEffect(int[] stack, BaseCharacter target)
    {
        base.ApplyEffect(stack, target);
        this.UpdateNextActionUI();
    }


    public virtual void UpdateNextActionUI()
    {

        nextActionUI.UpdateNextAction();
    }

}