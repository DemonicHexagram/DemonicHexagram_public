using UnityEngine;

public class KeyWordManager
{
    public static readonly string str_nullTxt = "";

    public static readonly string str_CardList = "CardList";
    public static readonly string str_CardCode = "CardCode";
    public static readonly string str_CardName = "Name";
    public static readonly string str_CardCost = "Cost";
    public static readonly string str_CardDescrible = "Describle";
    public static readonly string str_CardElemental = "Elemental";
    public static readonly string str_CardDamage = "Damage";
    public static readonly string str_CardShield = "Shield";
    public static readonly string str_CardType = "Type";
    public static readonly string str_CardEffectWeak = "Weak";
    public static readonly string str_CardEffectStrength = "Strength";
    public static readonly string str_CardEffectPoison = "Poison";
    public static readonly string str_CardMana = "Mana";
    public static readonly string str_CardDraw = "Draw";
    public static readonly string str_CardCount = "Count";


    public static readonly string str_MonsterName = "Name";
    public static readonly string str_MonsterMaxHp = "MaxHp";
    public static readonly string str_MonsterMinHp = "MinHp";
    public static readonly string str_MonsterShieldPower = "ShieldPower";
    public static readonly string str_MonsterAttack = "Attack";
    public static readonly string str_MonsterElemental = "Elemental";
    public static readonly string str_MonsterAction = "Actions";
    public static readonly string str_MonsterWeak = "Weak";
    public static readonly string str_MonsterStrength = "Strength";
    public static readonly string str_MonsterPoison = "Poison";

    public static readonly string str_PoolTagDeck = "Deck";
    public static readonly string str_PoolTagEnemy = "Enemy";
    public static readonly string str_PoolTagCard = "Card";

    public static readonly string str_PoolTagTwoWayStageNode = "TwoWayStageNode";
    public static readonly string str_PoolTagMinorEnemyBlock = "MinorEnemyBlock";
    public static readonly string str_PoolTagEliteEnemyBlock = "EliteEnemyBlock";
    public static readonly string str_PoolTagMysteryBlock = "MysteryBlock";
    public static readonly string str_PoolTagBossBlock = "BossBlock";
    public static readonly string str_PoolTagStoreBlock = "StoreBlock";
    public static readonly string str_PoolTagIncidentScene = "IncidentScene";
    public static readonly string str_PoolTagRestSiteBlock = "RestSiteBlock";

    public static readonly int int_LimitCardCount = 8;
    public static readonly int int_DeleteCardPrice = 100;

    public static readonly float flt_DefaultLocationX = 0.8f;
    public static readonly float flt_LocationMultiplierX = 2.1f;
    public static readonly float flt_LocationMultiplierY = -1.27f;
    public static readonly float flt_LocationMultiplierZ = 7.92f;

    public static readonly char char_UnderBar = '_';

    public static readonly float flt_DrawCardAnime = 0.5f;
    public static readonly float flt_EPS = 0.01f;
    public static readonly Vector2 vec_bezircurvStartPosOffset = new Vector2(0, Screen.height * 0.15f);

    public static readonly string str_HitParticleTag = "PlayerHit";

    public static readonly int int_AttackTrigger = Animator.StringToHash("BasicAttackTrigger");
    public static readonly int int_ShieldTrigger = Animator.StringToHash("BasicShieldTrigger");
    public static readonly int int_EffectToPlayerTrigger = Animator.StringToHash("EffectToPlayerTrig");
    public static readonly int int_EffectToSelfTrigger = Animator.StringToHash("EffectToSelfTrig");
    public static readonly int int_ElementalAtkTrigger = Animator.StringToHash("ElementalAttackTrig");
    public static readonly int int_BossAtkTrigger = Animator.StringToHash("BossAttackTrigger");
    public static readonly int int_SpecalAttackTrig = Animator.StringToHash("SpecalAttackTrig");


    public static readonly float flt_EnemyActionInterval = 1.3f;
    public static readonly string str_PlayerTurnTxt = "플레이어 턴";
    public static readonly string str_EnemyTurnTxt = "적 턴";
    public static readonly float flt_AnimaTime = 1.5f;

    public static readonly string str_MovingStageSceneTxt = "MovingStageScene_Giwoong222 (UPdate)feat 1";
    public static readonly string str_TitleSceneTxt = "TitleScene";
    public static readonly string str_BattleSceneTxt = "3DMapMerge_Giwoong";
    public static readonly string str_BossBattleSceneTxt = "BossScene";
    
    public static readonly Vector3 Vec3_PlayerDefaultTransform = new Vector3(-0.03f, 1.953202f, 0.1f);

    public static readonly string str_StageSheetTxt = "StageSheet";
    public static readonly string str_IncidentListSheetTxt = "IncidentList";
    public static readonly string str_MonsterSheetTxt = "MonsterSheet";

    public static readonly float flt_HoverScaleFactor = 1.2f;
    public static readonly float flt_MaxClickDuration = 0.5f;
    public static readonly float flt_NodeAnimeDuration = 0.3f;

    public static readonly Vector3 Vec3_ZoomedScale = new Vector3(0.8f, 0.8f, 1);
    public static readonly Vector3 Vec3_ZoomedPosition = new Vector3(960, -540, 0);

    public static readonly Quaternion Quat_Zeros = new Quaternion(0, 0, 0, 0);
    public static readonly Quaternion Quat_flip = new Quaternion(0, 180, 0, 0);

    public static readonly string str_WalkTxt = "Walk";

    public static readonly int int_CanStandLayer = 8;
    public static readonly int int_PlayerLayer = 7;


    public static readonly string str_TagPlayer = "Player";

    public static readonly float flt_IncidentTitleTextSpd = 0.4f;
    public static readonly float flt_IncidentDescriptionTextSpd = 0.8f;
    public static readonly float flt_IncidentSelectTextSpd = 0.8f;

    public static readonly float flt_TurnPanelAnimeDuration = 0.5f;
    public static readonly float flt_TurnPanelDisplayDuration = 0.8f;

    public static readonly string str_GoldAddButtonTxt = "GoldAddbutton";
    public static readonly int int_MinorEnemyMinGold = 20;
    public static readonly int int_MinorEnemyMaxGold = 30;
    public static readonly int int_EliteEnemyMinGold = 40;
    public static readonly int int_EliteEnemyMaxGold = 60;
    public static readonly int int_BossEnemyMinGold = 80;
    public static readonly int int_BossEnemyMaxGold = 100;

    public static readonly string str_CardAir = "CardAir";
    public static readonly string str_CardEarth = "CardEarth";
    public static readonly string str_CardFire = "CardFire";
    public static readonly string str_CardWater = "CardWater";

    public static readonly string str_CardFront = "CardFront";
    public static readonly string str_CardPower = "Power";
    public static readonly string str_CardHealth = "Health";
    public static readonly string str_CardFrame = "Frame";
    public static readonly string str_CardMask = "Mask";
    public static readonly string str_CardArtWork = "ArtWork";

    public static readonly string str_Common = "Common";
    public static readonly string str_UnCommon = "UnCommon";
    public static readonly string str_Rare = "Rare";
    public static readonly string str_SuperRare = "SuperRare";
    public static readonly string str_Legend = "Legend";

}
