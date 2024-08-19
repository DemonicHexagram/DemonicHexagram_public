using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggerManager : MonoBehaviour
{

    public Enemy Enemy;    

    public void OnBasicAttackEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyAttack);
        Enemy.CheckDamage(Enemy.attack, GameManager.Instance.Player);
    }
    public void OnBasicShieldEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.ShieldCard);
        Enemy.AddShield(Enemy.shieldPower);
    }
    public void OnEffectEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyDebuff);
        Enemy.ApplyEffect(Enemy.effect,  GameManager.Instance.Player);
    }
    public void OnSelfEffectEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyBuff);
        Enemy.ApplyEffect(Enemy.effect,null);
    }
    public void OnElementalAttackEvent()
    {
        SoundManager.Instance.PlaySfx(SFX.EnemyAttack);
        Enemy.ApplyElementalEffect(Enemy.elemental, GameManager.Instance.Player);
        Enemy.CheckDamage(Enemy.attack, GameManager.Instance.Player);

    }

}
