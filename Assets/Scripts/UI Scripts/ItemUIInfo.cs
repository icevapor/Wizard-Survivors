using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemUIInfo
{
    public static Sprite[] itemSprites;
    public static string[] itemDescriptions = new string[]
    {
        "",
        "The Bubble Wand creates bubbles of magic that drift toward demons and explode in their eyes. Ouch.",
        "This staff was designed to make wizards look cool, generating explosions behind the user at will. Explosions still hurt though.",
        "A true wizard doesn’t let anyone or anything make physical contact with them. This ring was enchanted to make that easier!",
        "Your last batch of mana potions ended up the wrong color. Why not chuck some at your enemies and see what happens?",
        "An essential tool that no self respecting wizard leaves the house without. Used to tame the fowlest of avian demons.",
        "Unfortunately this accessory tends to attract more celestial bodies than human ones.",
        "Pop these sucks like grapes and you’ll be feeling good as new!",
        "The mystery of fairy mobility revealed at last! I guess they just had heelys.",
        "This legendary wand was crafted using the wood of a tree behind my house that's, like, pretty old.",
        "Magic rock that’s guaranteed to extend your life by about, say, 15% per level.",
        "Easier access to your wide array of items. Added benefit of making you feel like one of those guys that build stuff."
    };

    public static string[] passiveEffects = new string[]
    {
        "",
        "HP regenerates 0.5% per second per level",
        "Increases movement speed by 7% per level",
        "Increases weapon damage by 10% per level",
        "Increases HP by 15% per level",
        "Reduces cooldown of weapons by 7% per level"  
    };

    public static string[] weaponEffects = new string[]
    {
        "",
        "Obtain the Bubble Wand",
        "The Bubble Wand gains an additional projectile",
        "The Bubble Wand fires 25% faster",
        "The Bubble Wand deals 25% more damage",
        "The Bubble Wand gains two additional projectiles",
        "Obtain the Explosion Staff",
        "Explosions are fired more frequently",
        "Explosions are larger",
        "Explosions deal 50% more damage",
        "Explosions are much larger",
        "Obtain the Wind Ring",
        "The Wind Ring activates more frequently",
        "The Wind Ring deals 100% more damage",
        "The Wind Ring is larger",
        "The Wind Ring activates even more frequently",
        "Obtain the Unstable Potion",
        "Enemies under the effect of the unstable potion gain bonus damage",
        "The Unstable Potion effect lasts longer",
        "The Unstable Potion can be thrown more frequently",
        "The Unstable Potion effect lasts even longer",
        "Obtain Bread",
        "Feeding ducks rye bread makes them stronger",
        "More bread means more ducks",
        "Slipping protein powder into the bread will create powerful ducks",
        "Too many ducks",
        "Obtain the Bracelet of Revolution",
        "The Bracelet attracts an additional orbital",
        "The Orbitals deal more damage",
        "The Orbitals revolve around you more quickly",
        "The Bracelet attracts two additional orbitals"
    };
}
