using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rest : MonoBehaviour
{
    public int hpheal;

    public int Up;

    [SerializeField] private Animator animator;
    [Header("Anim")]
    [SerializeField] private GameObject heal;
    [SerializeField] private GameObject upgrade;

    public Button healButton;
    public Button upgradeButton;

    public TextMeshProUGUI healText;
    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        
    }

    public void OnEnable()
    {
        hpheal = (int)(GameManager.Instance.Player.fullhp * 0.2);
        healText.text = $"체력을 {hpheal}만큼 회복합니다.";
    }

    public void Heal()
    {
        healButton.interactable = false;
        upgradeButton.interactable = false;
        GameManager.Instance.Player.Heal(hpheal);
        StartCoroutine(HealAnim());

        GameManager.Instance.mapManager.SaveMap();

    }
    public void Upgrade() 
    {
        StageUIManager.Instance.OnUpgradeUI();
    }

    public void SetUpgrade()
    {
        healButton.interactable = false;
        upgradeButton.interactable = false;
        StartCoroutine(UpgradeAnim());

        GameManager.Instance.mapManager.SaveMap();
    }
    private float GetAnimationClipLength(Animator animator, string clipName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0f; // Default length if clip not found
    }

    IEnumerator HealAnim()
    {
        heal.SetActive(true);
        animator.SetTrigger("Heal");

        float healAnimationLength = GetAnimationClipLength(animator, "Heal");

        yield return new WaitForSeconds(KeyWordManager.flt_AnimaTime);
        gameObject.SetActive(false);
        heal.SetActive(false);
        GameManager.Instance.Player.ChangeCanMove(true);
        healButton.interactable = true;
        upgradeButton.interactable = true;


    }
    IEnumerator UpgradeAnim()
    {
        upgrade.SetActive(true);
        animator.SetTrigger("Upgrade");

        float upgradeAnimationLength = GetAnimationClipLength(animator, "Upgrade");

        yield return new WaitForSeconds(KeyWordManager.flt_AnimaTime);
        gameObject.SetActive(false);
        upgrade.SetActive(false);
        GameManager.Instance.Player.ChangeCanMove(true);
        healButton.interactable = true;
        upgradeButton.interactable = true;

    }
}
