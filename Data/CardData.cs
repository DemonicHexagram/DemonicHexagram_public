public class CardData
{
    private int _cardCode;                     
    private string _name;                      
    private string _describle;                  
    private int _cost;                         
    private CardGrade _grade; 

    private Elemental _elemental;           
    private int _damage;                    
    private int _shield;                    
    private int _type;                      
    private int[] _effect;
    private int _draw;
    private int _mana;
    private int _count;
    public int CardCode { get { return _cardCode; } set { _cardCode = value; } }
    public int Cost { get { return _cost; } set { _cost = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public string Describle { get { return _describle; } set { _describle = value; } }
    public CardGrade Grade { get { return _grade;} set { _grade = value; } }
    public Elemental Elemental { get { return _elemental; } set { _elemental = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public int Shield { get { return _shield; } set { _shield = value; } }
    public int Type { get { return _type; } set { _type = value; } }
    public int[] Effect { get { return _effect; } set { _effect = value; } }

    public int Draw { get { return _draw; } set { _draw = value; } }

    public int Mana { get { return _mana; } set { _mana = value; } }

    public int Count { get { return _count; } set { _count = value; } }

    public CardData DeepCopy()
    {
        CardData card = new();
        card.CardCode = _cardCode;
        card.Name = _name;
        card.Describle = _describle;
        card.Cost = _cost;
        card.Elemental = _elemental;
        card.Damage = _damage;
        card.Shield = _shield;
        card.Type = _type;
        card.Effect = _effect;
        card.Draw = _draw;
        card.Mana = _mana;
        card.Count = _count;
        return card;
    }
}
    