using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SpellManager {

    [HideInInspector]
    public static List<Spell> spells = new List<Spell>();

    public static void CommitSpell(Spell spell) //add to Spell class as a static method
    {
        spells.Add(spell);
    }
}
