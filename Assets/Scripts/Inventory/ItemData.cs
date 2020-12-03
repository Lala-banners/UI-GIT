using UnityEngine;

public enum ItemType
{
    Food,
    Weapon,
    Apparel,
    Crafting,
    Ingredients,
    Potions,
    Scrolls,
    Quest,
    Money
}

[System.Serializable]
public class ItemData
{
    #region Private Variables
    private int id;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int value;
    [SerializeField] private int amount;
    [SerializeField] private Texture2D icon;
    [SerializeField] private GameObject mesh;
    [SerializeField] private ItemType type;
    [SerializeField] private int damage;
    [SerializeField] private int armour;
    [SerializeField] private int heal;
    [SerializeField] private int healMana;
    #endregion

    #region Public Properties
    public int ID //allowing id to be modified outside script
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public GameObject Mesh
    {
        get { return mesh; }
        set { mesh = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Armour
    {
        get { return armour; }
        set { armour = value; }
    }

    public int Heal
    {
        get { return heal; }
        set { heal = value; }
    }

    public int HealMana
    {
        get { return healMana; }
        set { healMana = value; }
    }
    #endregion

    public static ItemData CreateItem(int itemID)
    {
        string Description = "";
        string copyName = "";
        int copyValue = 0;
        int copyAmount = 0;
        string copyIcon = "";
        string copyMesh = "";
        ItemType copyType = ItemType.Apparel;
        int copyDamage = 0;
        int copyArmour = 0;
        int copyHeal = 0;

        switch (itemID) //Keys on keyboard
        {
            #region Food 0 - 99
            case 0:
                copyName = "Apple";
                Description = "Munchies and Crunchies";
                copyValue = 1;
                copyAmount = 1;
                copyIcon = "Food/Apple";
                copyMesh = "Food/Apple";
                copyType = ItemType.Food;
                copyHeal = 10;
                break;
            case 1:
                copyName = "Meat";
                Description = "Picana";
                copyValue = 10;
                copyAmount = 1;
                copyIcon = "Food/Meat";
                copyMesh = "Food/Meat";
                copyType = ItemType.Food;
                copyHeal = 25;
                break;
            #endregion
            #region Weapon 100 - 199
            case 100:
                copyName = "Axe";
                Description = "Best stabby";
                copyValue = 150;
                copyAmount = 1;
                copyIcon = "Weapon/Axe";
                copyMesh = "Weapon/Axe";
                copyType = ItemType.Weapon;
                copyDamage = 50;
                break;
            case 101:
                copyName = "Sword";
                Description = "Stick'em with the pointy end";
                copyValue = 200;
                copyAmount = 1;
                copyIcon = "Weapon/Sword";
                copyMesh = "Weapon/Sword";
                copyType = ItemType.Weapon;
                copyDamage = 30;
                break;
            #endregion
            #region Apparel 200 - 299
            case 200:
                copyName = "Armour";
                Description = "Protecc";
                copyValue = 75;
                copyAmount = 1;
                copyIcon = "Apparel/Armour/Armour";
                copyMesh = "Apparel/Armour/Armour";
                copyType = ItemType.Apparel;
                copyArmour = 45;
                break;
            case 201:
                copyName = "Boots";
                Description = "Swiper no swiping!";
                copyValue = 20;
                copyAmount = 1;
                copyIcon = "Apparel/Armour/Boots";
                copyMesh = "Apparel/Armour/Boots";
                copyType = ItemType.Apparel;
                copyArmour = 10;
                break;
            case 202:
                copyName = "Braces";
                Description = "Not for teeth";
                copyValue = 20;
                copyAmount = 1;
                copyIcon = "Apparel/Armour/Braces";
                copyMesh = "Apparel/Armour/Braces";
                copyType = ItemType.Apparel;
                copyArmour = 10;
                break;
            case 203:
                copyName = "Gloves";
                Description = "Protect hands";
                copyValue = 25;
                copyAmount = 1;
                copyIcon = "Apparel/Armour/Gloves";
                copyMesh = "Apparel/Armour/Gloves";
                copyType = ItemType.Apparel;
                copyArmour = 15;
                break;
            case 204:
                copyName = "Helmet";
                Description = "Protec brain";
                copyValue = 40;
                copyAmount = 1;
                copyIcon = "Apparel/Armour/Helmet";
                copyMesh = "Apparel/Armour/Helmet";
                copyType = ItemType.Apparel;
                copyArmour = 35;
                break;
            case 205:
                copyName = "Shield";
                Description = "Point it in the general direction of danger and it 'should' work";
                copyValue = 35;
                copyAmount = 1;
                copyIcon = "Apparel/Armour/Shield";
                copyMesh = "Apparel/Armour/Shield";
                copyType = ItemType.Apparel;
                copyArmour = 30;
                break;
            case 206:
                copyName = "Cloak";
                Description = "Flappy Flappy Cape...NO CAPES!";
                copyValue = 25;
                copyAmount = 1;
                copyIcon = "Apparel/Cloak";
                copyMesh = "Apparel/Cloak";
                copyType = ItemType.Apparel;
                break;
            case 207:
                copyName = "Necklace";
                Description = "Sparkles";
                copyValue = 50;
                copyAmount = 1;
                copyIcon = "Apparel/Necklace";
                copyMesh = "Apparel/Necklace";
                copyType = ItemType.Apparel;
                break;
            case 208:
                copyName = "Pants";
                Description = "FOR THE LOVE OF GOD PUT PANTS ON";
                copyValue = 5;
                copyAmount = 1;
                copyIcon = "Apparel/Pants";
                copyMesh = "Apparel/Pants";
                copyType = ItemType.Apparel;
                break;
            case 209:
                copyName = "Ring";
                Description = "Symbol of Stockholm shops";
                copyValue = 500;
                copyAmount = 1;
                copyIcon = "Apparel/Ring";
                copyMesh = "Apparel/Ring";
                copyType = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300 - 399
            case 300:
                copyName = "Gem";
                Description = "Priceless";
                copyValue = 400;
                copyAmount = 1;
                copyIcon = "Crafting/Gem";
                copyMesh = "Crafting/Gem";
                copyType = ItemType.Crafting;
                break;
            case 301:
                copyName = "Ingot";
                Description = "Bar of Iron";
                copyValue = 10;
                copyAmount = 1;
                copyIcon = "Crafting/Ingot";
                copyMesh = "Crafting/Ingot";
                copyType = ItemType.Crafting;
                break;
            #endregion
            #region Potions 500 - 599
            case 500:
                copyName = "Health Potion";
                Description = "Liquid Life";
                copyValue = 50;
                copyAmount = 1;
                copyIcon = "Potions/HealthPotion";
                copyMesh = "Potions/HealthPotion";
                copyType = ItemType.Potions;
                copyHeal = 25;
                break;
            case 501:
                copyName = "Mana Potion";
                Description = "Liquid Magic";
                copyValue = 50;
                copyAmount = 1;
                copyIcon = "Potions/ManaPotion";
                copyMesh = "Potions/ManaPotion";
                copyType = ItemType.Potions;
                copyHeal = 25;
                break;
            #endregion
            #region Scrolls 600 - 699
            case 600:
                copyName = "Book of the Dead";
                Description = "Book that summons minions";
                copyValue = 5000;
                copyAmount = 1;
                copyIcon = "Scrolls/Book";
                copyMesh = "Scrolls/Book";
                copyType = ItemType.Scrolls;
                break;
            case 601:
                copyName = "Fireball Scroll";
                Description = "Scroll that summons a fireball....lets hope not on you";
                copyValue = 1000;
                copyAmount = 1;
                copyIcon = "Scrolls/Scroll";
                copyMesh = "Scrolls/Scroll";
                copyType = ItemType.Scrolls;
                break;
            #endregion
            #region Misc 800 - 899
            case 800:
                copyName = "Coin";
                Description = "Clink Clink";
                copyValue = 1;
                copyAmount = 1;
                copyIcon = "Coins";
                copyMesh = "Coins";
                copyType = ItemType.Money;
                break;
            #endregion
            default:
                itemID = 0;
                copyName = "Apple";
                Description = "Munchies and Crunchies";
                copyValue = 1;
                copyAmount = 1;
                copyIcon = "Food/Apple";
                copyMesh = "Food/Apple";
                copyType = ItemType.Food;
                copyHeal = 10;
                break;
        }

        ItemData temp = new ItemData
        {
            ID = itemID,
            Name = copyName,
            Description = Description,
            Value = copyValue,
            Amount = copyAmount,
            Type = copyType,
            Icon = Resources.Load("Icon/" + copyIcon) as Texture2D,
            Mesh = Resources.Load("Mesh/" + copyMesh) as GameObject,
            Damage = copyDamage,
            Armour = copyArmour,
            Heal = copyHeal
        };
        return temp;
    }
}

