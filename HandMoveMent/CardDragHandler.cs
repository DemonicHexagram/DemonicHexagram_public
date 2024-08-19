// CardPositionManager.cs
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CardDragHandler
{
    private CardContainer container;
    public CardDragHandler() { }
    public CardDragHandler(CardContainer cardContainer)
    {
        container = cardContainer;
    }

    public void OnCardDragStart(CardWrapper card)
    {
        container.CurrentDraggedCard = card;
        card.isHovered = true;
        if (card.currentCard.CardData.Type == 0)
        {
            container.bezierCurve.SetActive(true);
            container.bezierCurveFixWithUI.startPoint.position = card.targetPosition + KeyWordManager.vec_bezircurvStartPosOffset;
        }
    }

    public void OnCardDragEnd(CardWrapper currentDraggedCard)
    {
        if (IsCursorInPlayArea())
        {
            Card cardComponent = currentDraggedCard.GetComponent<Card>();

            Enemy hitEnemy = CastRayToFindEnemy();
            bool cardUsedSuccessfully = false;

            if (cardComponent.CardData.Cost <= BattleManager.Instance._curcost)
            {
                switch (cardComponent.CardData.Type)
                {
                    case 0:
                        if (hitEnemy != null)
                        {
                            cardUsedSuccessfully = ApplyCardEffect(cardComponent, hitEnemy);
                        }
                        break;
                    case 1:
                        cardUsedSuccessfully = ApplyCardEffect(cardComponent, null);
                        break;
                    case 2:
                        cardUsedSuccessfully = ApplyCardEffect(cardComponent, null);
                        break;
                    case 3:
                        cardUsedSuccessfully = ApplyCardEffect(cardComponent, null);
                        break;
                    default:
                        break;
                }

                if (cardUsedSuccessfully)
                {
                    BattleManager.Instance.battleDeck.MoveCard(BattleManager.Instance.battleDeck.hand, BattleManager.Instance.battleDeck.grave, cardComponent.CardNumber);
                    container.UseCard(currentDraggedCard);
                }
            }
        }
        container.bezierCurve.SetActive(false);
        container.CurrentDraggedCard.ResetCardState();
        container.CurrentDraggedCard = null;
        GameManager.Instance.isCardDragged = false;
    }

    private bool ApplyCardEffect(Card card, Enemy targetEnemy)
    {
        switch(card.CardData.Type)
        {
            case 0:
                card.OnUseCard(targetEnemy);
                return true;
            case 1:
                card.OnUseCard(targetEnemy);
                return true;
            case 2:
                card.OnUseCard(targetEnemy);
                return true;
            case 3:
                card.OnUseCard(targetEnemy);
                return true;
        }
        //if (card.CardData.Type == 0)
        //{
        //    card.OnUseCard(targetEnemy);

        //    container.enemyHit.transform.position = targetEnemy.particlePosition.position;
        //    container.enemyHit.Play();

        //    return true;
        //}
        //else if (card.CardData.Type == 1)
        //{
        //    card.OnUseCard(targetEnemy);
        //    return true;
        //}
        //else if (card.CardData.Type == 2)
        //{
        //    card.OnUseCard(targetEnemy);
        //    return true;
        //}
        //else if (card.CardData.Type == 2)
        //{
        //    card.OnUseCard(targetEnemy);
        //    return true;
        //}
        return false;
    }

    public Enemy CastRayToFindEnemy()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, container.enemyLayer))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            container.bezierCurveFixWithUI.isEnemyAimed = true;
            return enemy;
        }
        container.bezierCurveFixWithUI.isEnemyAimed = false;
        return null;
    }

    private bool IsCursorInPlayArea()
    {
        if (container.cardPlayConfig.playArea == null) return false;

        var cursorPosition = Input.mousePosition;
        var playArea = container.cardPlayConfig.playArea;
        var playAreaCorners = new Vector3[4];
        playArea.GetWorldCorners(playAreaCorners);
        return cursorPosition.x > playAreaCorners[0].x &&
               cursorPosition.x < playAreaCorners[2].x &&
               cursorPosition.y > playAreaCorners[0].y &&
               cursorPosition.y < playAreaCorners[2].y;
    }
}
