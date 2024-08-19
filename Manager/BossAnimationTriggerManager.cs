using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationTriggerManager : MonoBehaviour
{
    public Hexaghost Boss;

    public void OnBasicAttackEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyAttack);
        Boss.CheckDamage(Boss.attack, GameManager.Instance.Player);
    }
    public void OnBasicShieldEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.ShieldCard);
        Boss.AddShield(Boss.shieldPower);
    }
    public void OnEffectEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyDebuff);
        Boss.ApplyEffect(Boss.effect, GameManager.Instance.Player);
    }
    public void OnSelfEffectEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyBuff);
    }
    public void OnElementalAttackEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyAttack);
        Boss.ApplyElementalEffect(Elemental.Fire, GameManager.Instance.Player);
        Boss.CheckDamage(Boss.attack, GameManager.Instance.Player);

    }

    public void OnBossAttackEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyAttack);
        Boss.ApplyElementalEffect(Elemental.Thunder, GameManager.Instance.Player);
        Boss.CheckDamage(Boss.attack, GameManager.Instance.Player);

    }

    public void OnSpecalAttackEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyAttack);
        Boss.CheckDamage(3, GameManager.Instance.Player);
    }
}
