using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.U2D;

public class SpriteManager
{
    private List<CardSpriteData> _cardResourcessList;
    private List<Sprite> _cardGradeList;
    private Sprite _deleteCardSprite;
    public List<CardSpriteData> CardResourcessList { get { return _cardResourcessList; } }
    public List<Sprite> CardGradeList { get { return _cardGradeList; } }
    public Sprite DeleteCardSprite { get { return _deleteCardSprite; } }

    public void Initialize()
    {
        _cardResourcessList = new List<CardSpriteData>();
        _cardGradeList = new List<Sprite>();
        CardResourcesSetting();
    }

    public void CardResourcesSetting()
    {
        SpriteAtlas cardEarthAtlas = Resources.Load<SpriteAtlas>("CardSpriteAtlas/CardEarth");
        CardSetting(cardEarthAtlas, KeyWordManager.str_CardEarth);

        SpriteAtlas cardFireAtlas = Resources.Load<SpriteAtlas>("CardSpriteAtlas/CardFire");
        CardSetting(cardFireAtlas, KeyWordManager.str_CardFire);

        SpriteAtlas cardWaterAtlas = Resources.Load<SpriteAtlas>("CardSpriteAtlas/CardWater");
        CardSetting(cardWaterAtlas, KeyWordManager.str_CardWater);

        SpriteAtlas cardAirAtlas = Resources.Load<SpriteAtlas>("CardSpriteAtlas/CardAir");
        CardSetting(cardAirAtlas, KeyWordManager.str_CardAir);

        SpriteAtlas cardGradeAtlas = Resources.Load<SpriteAtlas>("CardSpriteAtlas/CardRank");
        CardGradeSetting(cardGradeAtlas);

        _deleteCardSprite = Resources.Load<Sprite>("CardSpriteAtlas/DeleteCard");
    }

    private void CardSetting(SpriteAtlas spriteAtlas, string tag)
    {
        CardSpriteData cardResouresData = new CardSpriteData();
        cardResouresData.Name = tag;

        StringBuilder cardFront = new StringBuilder(KeyWordManager.str_CardFront);
        cardFront.Append(tag);
        cardResouresData.CardFront = STBGetSprite(ref spriteAtlas, ref cardFront);

        StringBuilder cardcost = new StringBuilder(KeyWordManager.str_CardCost);
        cardcost.Append(tag);
        cardResouresData.CardCost = STBGetSprite(ref spriteAtlas, ref cardcost);

        StringBuilder cardPower = new StringBuilder(KeyWordManager.str_CardPower);
        cardPower.Append(tag);
        cardResouresData.CardDamage = STBGetSprite(ref spriteAtlas, ref cardPower);

        StringBuilder cardHealth = new StringBuilder(KeyWordManager.str_CardHealth);
        cardHealth.Append(tag);
        cardResouresData.CardShield = STBGetSprite(ref spriteAtlas, ref cardHealth);

        StringBuilder cardFrame = new StringBuilder(KeyWordManager.str_CardFrame);
        cardFrame.Append(tag);
        cardResouresData.CardFrame = STBGetSprite(ref spriteAtlas, ref cardFrame);

        StringBuilder cardMask = new StringBuilder(KeyWordManager.str_CardMask);
        cardMask.Append(tag);
        cardResouresData.CardMask = STBGetSprite(ref spriteAtlas, ref cardMask);

        StringBuilder cardArtWork = new StringBuilder(KeyWordManager.str_CardArtWork);
        cardArtWork.Append(tag);
        cardResouresData.CardBackGround = STBGetSprite(ref spriteAtlas, ref cardArtWork);

        _cardResourcessList.Add(cardResouresData);
    }

    private void CardGradeSetting(SpriteAtlas spriteAtlas)
    {
        StringBuilder common = new StringBuilder(KeyWordManager.str_Common);
        Sprite commonSprite = STBGetSprite(ref spriteAtlas, ref common);
        _cardGradeList.Add(commonSprite);

        StringBuilder uncommon = new StringBuilder(KeyWordManager.str_UnCommon);
        Sprite uncommonSprite = STBGetSprite(ref spriteAtlas, ref uncommon);
        _cardGradeList.Add(uncommonSprite);

        StringBuilder rare = new StringBuilder(KeyWordManager.str_Rare);
        Sprite rareSprite = STBGetSprite(ref spriteAtlas, ref rare);
       _cardGradeList.Add(rareSprite);
    }

    private Sprite STBGetSprite(ref SpriteAtlas spriteAtlas, ref StringBuilder stringBuilder)
    {
        return spriteAtlas.GetSprite(stringBuilder.ToString());
    }
}