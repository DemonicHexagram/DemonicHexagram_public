using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class BaseCharacter : MonoBehaviour
{
    [Header ("현재 체력, 최대 체력")]
    public int hp;
    public int fullhp;
    [Header("쉴드 수치")]
    public int shield = 0;
    public Dictionary<StatusEffect, int> activeEffects = new Dictionary<StatusEffect, int>();
    public List<Elemental> currentElementalEffect = new List<Elemental>();

    public Animator EffectAnimator;

    public Transform ShakeTransform;   

    public Transform particlePosition;

    [SerializeField] public ParticleSystem Hit;
    [SerializeField] public ParticleSystem FireHit;
    [SerializeField] public ParticleSystem WaterHit;
    [SerializeField] public ParticleSystem ThunderHit;
    [SerializeField] public ParticleSystem Elemental_FireThunderHit;
    [SerializeField] public ParticleSystem Elemental_FireWaterHit;
    [SerializeField] public ParticleSystem Elemental_WaterThundernderHit;

    protected virtual void Start()
    {
        Hit.gameObject.SetActive(false);
        FireHit.gameObject.SetActive(false);
        WaterHit.gameObject.SetActive(false);
        ThunderHit.gameObject.SetActive(false);
        Elemental_FireThunderHit.gameObject.SetActive(false);
        Elemental_FireWaterHit.gameObject.SetActive(false);
        Elemental_WaterThundernderHit.gameObject.SetActive(false);
    }
    public virtual void StartTurn()
    {
        ApplyPoisonDamage(this);
        DecreaseEffectStacks();
        shield = 0;
        if (GameManager.Instance.Player.hp <= 0)
        {
            BattleManager.Instance.PlayerDie();
        }
    }
    //체력 감소 메소드
    public void HpDecrease(int damage, BaseCharacter target)
    {
        if (damage >= 0) 
        {
            target.hp = target.hp - damage;
            if (target.hp < 0) 
            { 
                target.hp = 0;
            }
        }
        else
        {
            target.hp = 0;
            //BattleUIManager.Instance.OnLosePanel();
        }
    }

    public void Heal(int amount)
    {
        hp += amount;

        if (hp > fullhp)
        {
            hp = fullhp;
        }
    }
    public void AddShield(int amount)
    {
        shield += amount;
        if (amount > 0)
        {
            SoundManager.Instance.PlaySfx(SFX.ShieldCard);
        }
    }
    public virtual void ApplyEffect(int[] stack, BaseCharacter target)
    {
        if (target == null)
        {
            target = this;
        }
        if (stack[1] != 0)
        {
            StatusEffect effect = (StatusEffect)1;
            if (this.activeEffects.ContainsKey(effect))
            {
                this.activeEffects[effect] += stack[1];
            }
            else
            {
                this.activeEffects[effect] = stack[1];
            }
        }
        else
        {
            for (int i = 0; i < stack.Length; i++)
            {
                if (stack[i] > 0)
                {
                    StatusEffect effect = (StatusEffect)i;
                    if (target.activeEffects.ContainsKey(effect))
                    {
                        target.activeEffects[effect] += stack[i];
                    }
                    else
                    {
                        target.activeEffects[effect] = stack[i];
                    }
                }
            }
        }

        
    }

    protected virtual void ApplyPoisonDamage(BaseCharacter target)
    {
        if (target.activeEffects.ContainsKey(StatusEffect.Poison))
        {
            int poisonStack = target.activeEffects[StatusEffect.Poison];
            DamageTextManager.Instance.ShowDamageText(target.transform.position, poisonStack, Color.green);
            HpDecrease(poisonStack, target);
        }

    }
    protected void DecreaseEffectStacks()
    {
        var effectsToRemove = new List<StatusEffect>();
        var keys = new List<StatusEffect>(this.activeEffects.Keys);

        foreach (var effect in keys)
        {
            int stack = this.activeEffects[effect];

            if (stack > 1)
            {
                activeEffects[effect] = stack - 1;
            }
            else
            {
                effectsToRemove.Add(effect);
            }
        }

        foreach (var effect in effectsToRemove)
        {
            RemoveStatusEffect(effect);
        }
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        if (activeEffects.ContainsKey(effect))
        {
            activeEffects.Remove(effect);
        }
    }

    public virtual int CheckDamage(int damage, BaseCharacter target)
    {
        int finalDamage = damage;
        if (activeEffects.ContainsKey(StatusEffect.Strength) && this.activeEffects[StatusEffect.Strength] > 0)
        {
            finalDamage *= 2;
        }

        if (activeEffects.ContainsKey(StatusEffect.Weak) && this.activeEffects[StatusEffect.Weak] > 0)
        {
            finalDamage /= 2;
        }

        OnAttacked(finalDamage, target);

        return finalDamage;
    }

    public virtual void OnAttacked(int damage, BaseCharacter target)
    {
        if (target.shield > 0)
        {
            int remainingDamage = damage - target.shield;
            target.shield -= damage;

            if (target.shield < 0)
            {
                target.shield = 0;
            }

            if (remainingDamage > 0)
            {
                target.hp -= remainingDamage;
            }

        }
        else
        {
            HpDecrease(damage, target);
        }
        DamageTextManager.Instance.ShowDamageText(target.transform.position, damage, Color.white, Color.clear);
        HitEffect(target);
        ShakeObject(target);
    }

    public virtual void ApplyElementalEffect(Elemental element, BaseCharacter target)
    {
        if (target == null)
        {
            target = this;
        }
        target.currentElementalEffect.Add(element);

        switch (element)
        {
            case Elemental.None:
                target.HitEffect(target);
                break;
            case Elemental.Fire:
                target.FireEffect(target);
                break;
            case Elemental.Water:
                target.WaterEffect(target);
                break;
            case Elemental.Thunder:
                target.ThunderEffect(target);
                break;
        }
        //CheckDamage(damage, target);
        //AddShield(sheld);
        //ApplyEffect(effect, target);
        //if (effect[1] == 0)
        //{
        //    ApplyEffect(effect, target);
        //}
        //else
        //{
        //    ApplyEffect(effect, null);
        //}

        if (target.currentElementalEffect.Count >= 2)
        {
            Elemental firstValue = target.currentElementalEffect[0];
            Elemental secondValue = target.currentElementalEffect[1];

            if (firstValue == element)
            {
                target.currentElementalEffect.RemoveAt(0);
                
            }
            else
            {
                CheckElementalSynergy(firstValue, secondValue, target);
                target.currentElementalEffect.Clear();
            }
        }

    }

    protected void CheckElementalSynergy(Elemental firstelement, Elemental secondelement, BaseCharacter target)
    {

        if ((firstelement == Elemental.Fire && secondelement == Elemental.Water) ||
        (firstelement == Elemental.Water && secondelement == Elemental.Fire))
        {
            Elemental_FireWater(target);
        }
        else if ((firstelement == Elemental.Fire && secondelement == Elemental.Thunder) ||
                 (firstelement == Elemental.Thunder && secondelement == Elemental.Fire))
        {
            Elemental_FireThunder(target);
        }
        else if ((firstelement == Elemental.Water && secondelement == Elemental.Thunder) ||
                 (firstelement == Elemental.Thunder && secondelement == Elemental.Water))
        {            
            Elemental_WaterThunder(target);
        }
    }

    public void RemoveElementalEffect(BaseCharacter taget)
    {

        if (taget != null)
        {
            taget.currentElementalEffect.Clear();
        }
    }

    public void LogCurrentEffects()
    {
        if (activeEffects.Count == 0)
        {
            Debug.Log("현재 적용된 상태 효과가 없습니다.");
        }
        else
        {
            foreach (var kvp in activeEffects)
            {
                Debug.Log($"{kvp.Key} 효과가 현재 {kvp.Value} 스택 적용 중입니다.");
            }
        }

        Debug.Log(this.currentElementalEffect);
        Debug.Log(GameManager.Instance.Player.currentElementalEffect);
    }

    private void ShakeObject(BaseCharacter target)
    {
        Vector3 originalPosition = target.transform.position;
        target.ShakeTransform.DOShakePosition(0.2f, 0.4f, 80, 20f, false, true).OnKill(() => target.transform.position = originalPosition);        
    }

    private void Elemental_FireWater(BaseCharacter target)
    {
        DamageTextManager.Instance.ShowDamageText(this.transform.position, 3, Color.white, Color.yellow);
        target.Elemental_FireWaterEffect(this);
        Heal(3);
    }

    private void Elemental_WaterThunder(BaseCharacter target)
    {
        ApplySpecificEffect(StatusEffect.Poison, 5, target);
        target.Elemental_WaterThunderEffect(target);
        ApplyPoisonDamage(target);

    }
    private void Elemental_FireThunder(BaseCharacter target)
    {
        int damage = 5;
        DamageTextManager.Instance.ShowDamageText(target.transform.position, damage, Color.red, Color.yellow);
        target.Elemental_FireThunderEffect(target);
        CheckDamage(damage, target);
    }

    private void ApplySpecificEffect(StatusEffect effect, int stack, BaseCharacter target)
    {
        if (target == null)
        {
            target = this;
        }

        if (target.activeEffects.ContainsKey(effect))
        {
            target.activeEffects[effect] += stack;
        }
        else
        {
            target.activeEffects[effect] = stack;
        }
    }
    public void HitEffect(BaseCharacter target)
    {
        Hit.gameObject.SetActive(true);
        Hit.transform.position = target.particlePosition.position;
        Hit.Play();
        ParticleEnd(Hit);
    }
    public void FireEffect(BaseCharacter target)
    {
        FireHit.gameObject.SetActive(true);
        FireHit.transform.position = target.particlePosition.position;
        FireHit.Play();
        ParticleEnd(Hit);
    }
    public void WaterEffect(BaseCharacter target)
    {
        WaterHit.gameObject.SetActive(true);
        WaterHit.transform.position = target.particlePosition.position + Vector3.down * 0.7f;
        WaterHit.Play();
        ParticleEnd(WaterHit);
    }
    public void ThunderEffect(BaseCharacter target)
    {
        ThunderHit.gameObject.SetActive(true);
        ThunderHit.transform.position = target.particlePosition.position;
        ThunderHit.Play();
        ParticleEnd(ThunderHit);
    }

    public void Elemental_FireWaterEffect(BaseCharacter target)
    {
        target.Elemental_FireWaterHit.gameObject.SetActive(true);
        target.Elemental_FireWaterHit.transform.position = target.particlePosition.position;
        target.Elemental_FireWaterHit.Play();
        ParticleEnd(Elemental_FireWaterHit);
    }
    public void Elemental_WaterThunderEffect(BaseCharacter target)
    {
        target.Elemental_WaterThundernderHit.gameObject.SetActive(true);
        target.Elemental_WaterThundernderHit.transform.position = target.particlePosition.position;
        target.Elemental_WaterThundernderHit.Play();
        ParticleEnd(Elemental_WaterThundernderHit);
    }
    public void Elemental_FireThunderEffect(BaseCharacter target)
    {
        target.Elemental_FireThunderHit.gameObject.SetActive(true);
        target.Elemental_FireThunderHit.transform.position = target.particlePosition.position;
        target.Elemental_FireThunderHit.Play();
        ParticleEnd(Elemental_FireThunderHit);
    }
    
    IEnumerator ParticleEnd(ParticleSystem particle)
    {
        yield return new WaitForSeconds(2f);
        particle.gameObject.SetActive(false);
    }
}
