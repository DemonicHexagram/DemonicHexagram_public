using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    private List<CardData> _cardList;
    private CountingData _countingData;
    public List<CardData> CardList { get { return _cardList; } }
    public Vector3 PlayerTransformVector3 = KeyWordManager.Vec3_PlayerDefaultTransform;
    public CountingData CountingData { get { return _countingData; } }
    public void Initialize()
    {
        _countingData = new CountingData();
        _countingData.Init(); 
        List<Dictionary<string, object>> data_list = CSVReader.Read("CardList");
        CardListParsing(data_list);
    }

    private void CardListParsing(List<Dictionary<string, object>> data_list)
    {
        _cardList = new List<CardData>();
        for (int i = 0; i < data_list.Count; i++)
        {
            CardData cardData = new();

            cardData.CardCode = int.Parse(data_list[i][KeyWordManager.str_CardCode].ToString());
            cardData.Name = data_list[i][KeyWordManager.str_CardName].ToString();
            cardData.Cost = int.Parse(data_list[i][KeyWordManager.str_CardCost].ToString());
            cardData.Describle = data_list[i][KeyWordManager.str_CardDescrible].ToString();
            cardData.Elemental = (Elemental)int.Parse(data_list[i][KeyWordManager.str_CardElemental].ToString());
            cardData.Damage = int.Parse(data_list[i][KeyWordManager.str_CardDamage].ToString());
            cardData.Shield = int.Parse(data_list[i][KeyWordManager.str_CardShield].ToString());
            cardData.Type = int.Parse(data_list[i][KeyWordManager.str_CardType].ToString());
            cardData.Effect = new int[3];
            cardData.Effect[0] = int.Parse(data_list[i][KeyWordManager.str_CardEffectWeak].ToString());
            cardData.Effect[1] = int.Parse(data_list[i][KeyWordManager.str_CardEffectStrength].ToString());
            cardData.Effect[2] = int.Parse(data_list[i][KeyWordManager.str_CardEffectPoison].ToString());
            cardData.Mana = int.Parse(data_list[i][KeyWordManager.str_CardMana].ToString());
            cardData.Draw = int.Parse(data_list[i][KeyWordManager.str_CardDraw].ToString());
            cardData.Count = int.Parse(data_list[i][KeyWordManager.str_CardCount].ToString());

            _cardList.Add(cardData);
        }
    }

    public void Dispose()
    {
        _cardList.Clear();

        GC.SuppressFinalize(this);
    }

    public List<CardData> ElementalSortCardList()
    {
        List<CardData> newCardlist = new List<CardData>();
        newCardlist = CardList;

        for(int i = 0; i < newCardlist.Count; i++)
        {
            for(int j = i + 1; j < newCardlist.Count; j++)
            {
                if(newCardlist[i].Elemental > newCardlist[j].Elemental)
                {
                    CardData temp = newCardlist[i];
                    newCardlist[i] = newCardlist[j];
                    newCardlist[j] = temp;  
                }
            }
        }
        return newCardlist;  
    }
}
