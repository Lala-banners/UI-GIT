using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        string description = "";
        string name = "";
        int value = 0;
        int amount = 0;
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Apparel;
        int damage = 0;
        int armour = 0;
        int heal = 0;

        switch (itemID) //Keys on keyboard
        {
            #region Food 0 - 99
            case 0:
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 1;
                amount = 1;
                icon = "Food/Apple";
                mesh = "Food/Apple";
                type = ItemType.Food;
                heal = 10;
                break;
            case 1:
                name = "Meat";
                description = "Picana";
                value = 10;
                amount = 1;
                icon = "Food/Meat";
                mesh = "Food/Meat";
                type = ItemType.Food;
                heal = 25;
                break;
            #endregion
            #region Weapon 100 - 199
            case 100:
                name = "Axe";
                description = "Best stabby";
                value = 150;
                amount = 1;
                icon = "Weapon/Axe";
                mesh = "Weapon/Axe";
                type = ItemType.Weapon;
                damage = 50;
                break;
            case 101:
                name = "Sword";
                description = "Stick'em with the pointy end";
                value = 200;
                amount = 1;
                icon = "Weapon/Sword";
                mesh = "Weapon/Sword";
                type = ItemType.Weapon;
                damage = 30;
                break;
            #endregion
            #region Apparel 200 - 299
            case 200:
                name = "Armour";
                description = "Protecc";
                value = 75;
                amount = 1;
                icon = "Apparel/Armour/Armour";
                mesh = "Apparel/Armour/Armour";
                type = ItemType.Apparel;
                armour = 45;
                break;
            case 201:
                name = "Boots";
                description = "Swiper no swiping!";
                value = 20;
                amount = 1;
                icon = "Apparel/Armour/Boots";
                mesh = "Apparel/Armour/Boots";
                type = ItemType.Apparel;
                armour = 10;
                break;
            case 202:
                name = "Braces";
                description = "Not for teeth";
                value = 20;
                amount = 1;
                icon = "Apparel/Armour/Braces";
                mesh = "Apparel/Armour/Braces";
                type = ItemType.Apparel;
                armour = 10;
                break;
            case 203:
                name = "Gloves";
                description = "Protect hands";
                value = 25;
                amount = 1;
                icon = "Apparel/Armour/Gloves";
                mesh = "Apparel/Armour/Gloves";
                type = ItemType.Apparel;
                armour = 15;
                break;
            case 204:
                name = "Helmet";
                description = "Protec brain";
                value = 40;
                amount = 1;
                icon = "Apparel/Armour/Helmet";
                mesh = "Apparel/Armour/Helmet";
                type = ItemType.Apparel;
                armour = 35;
                break;
            case 205:
                name = "Shield";
                description = "Point it in the general direction of danger and it 'should' work";
                value = 35;
                amount = 1;
                icon = "Apparel/Armour/Shield";
                mesh = "Apparel/Armour/Shield";
                type = ItemType.Apparel;
                armour = 30;
                break;
            case 206:
                name = "Cloak";
                description = "Flappy Flappy Cape...NO CAPES!";
                value = 25;
                amount = 1;
                icon = "Apparel/Cloak";
                mesh = "Apparel/Cloak";
                type = ItemType.Apparel;
                break;
            case 207:
                name = "Necklace";
                description = "Sparkles";
                value = 50;
                amount = 1;
                icon = "Apparel/Necklace";
                mesh = "Apparel/Necklace";
                type = ItemType.Apparel;
                break;
            case 208:
                name = "Pants";
                description = "FOR THE LOVE OF GOD PUT PANTS ON";
                value = 5;
                amount = 1;
                icon = "Apparel/Pants";
                mesh = "Apparel/Pants";
                type = ItemType.Apparel;
                break;
            case 209:
                name = "Ring";
                description = "Symbol of Stockholm shops";
                value = 500;
                amount = 1;
                icon = "Apparel/Ring";
                mesh = "Apparel/Ring";
                type = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300 - 399
            case 300:
                name = "Gem";
                description = "Priceless";
                value = 400;
                amount = 1;
                icon = "Crafting/Gem";
                mesh = "Crafting/Gem";
                type = ItemType.Crafting;
                break;
            case 301:
                name = "Ingot";
                description = "Bar of Iron";
                value = 10;
                amount = 1;
                icon = "Crafting/Ingot";
                mesh = "Crafting/Ingot";
                type = ItemType.Crafting;
                break;
            #endregion
            #region Potions 500 - 599
            case 500:
                name = "Health Potion";
                description = "Liquid Life";
                value = 50;
                amount = 1;
                icon = "Potions/HealthPotion";
                mesh = "Potions/HealthPotion";
                type = ItemType.Potions;
                heal = 25;
                break;
            case 501:
                name = "Mana Potion";
                description = "Liquid Magic";
                value = 50;
                amount = 1;
                icon = "Potions/ManaPotion";
                mesh = "Potions/ManaPotion";
                type = ItemType.Potions;
                heal = 25;
                break;
            #endregion
            #region Scrolls 600 - 699
            case 600:
                name = "Book of the Dead";
                description = "Book that summons minions";
                value = 5000;
                amount = 1;
                icon = "Scrolls/Book";
                mesh = "Scrolls/Book";
                type = ItemType.Scrolls;
                break;
            case 601:
                name = "Fireball Scroll";
                description = "Scroll that summons a fireball....lets hope not on you";
                value = 1000;
                amount = 1;
                icon = "Scrolls/Scroll";
                mesh = "Scrolls/Scroll";
                type = ItemType.Scrolls;
                break;
            #endregion
            #region Misc 800 - 899
            case 800:
                name = "Coin";
                description = "Clink Clink";
                value = 1;
                amount = 1;
                icon = "Coins";
                mesh = "Coins";
                type = ItemType.Money;
                break;
            #endregion
            default:
                itemID = 0;
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 1;
                amount = 1;
                icon = "Food/Apple";
                mesh = "Food/Apple";
                type = ItemType.Food;
                heal = 10;
                break;
        }

        Item temp = new Item
        {
            ID = itemID,
            Name = name,
            Description = description,
            Value = value,
            Amount = amount,
            Type = type,
            Icon = Resources.Load<Sprite>("Icon/" + icon),
            Mesh = Resources.Load("Mesh/" + mesh) as GameObject,
            Damage = damage,
            Armour = armour,
            Heal = heal
        };
        return temp;
    }
}



