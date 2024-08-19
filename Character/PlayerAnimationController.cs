using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator EffectAnimator;
    void Start()
    {
        GameManager.Instance.Player.animator = EffectAnimator;
        GameManager.Instance.Player.ShakeTransform = this.gameObject.transform;
        GameManager.Instance.Player.particlePosition = this.gameObject.transform;
    }
}
