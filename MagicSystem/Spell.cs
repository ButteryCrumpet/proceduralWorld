using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell {

    public string Name;

    public Dictionary<SpellEnums.Attributes, float> attributes = new Dictionary<SpellEnums.Attributes, float>() {
        { SpellEnums.Attributes.Damage, 10 },
        { SpellEnums.Attributes.EffectRadius, 5 },
        { SpellEnums.Attributes.Range,10},
        { SpellEnums.Attributes.Speed, 10 }
    };

    public SpellEnums.Elements element = SpellEnums.Elements.Arcane;
    public SpellEnums.Types type = SpellEnums.Types.Onhit;
    public SpellEnums.Targeting target = SpellEnums.Targeting.Untargeted;


    public Spell(List<SpellWord> spellWords)
    {
        foreach (SpellWord spellWord in spellWords)
        {
            SetBases(spellWord);
            SetEnums(spellWord);
            SetName(spellWord);
        }
    }

    private void SetName(SpellWord spellWord)
    {
        if (Name == null)
            Name = spellWord.Name;
        else
            Name += spellWord.Name;
    }

    private void SetBases(SpellWord spellWord)
    {
        foreach (KeyValuePair<SpellEnums.Attributes, float> attribute in spellWord.attributes)
        {
            attributes[attribute.Key] += attribute.Value;
        }
    }

    private void SetEnums(SpellWord spellWord)
    {
        element = spellWord.element;
        type = spellWord.type;
        target = spellWord.target;
    }

    public string spellToString()
    {
        string asString = Name;
        asString += "\nDamage: " + attributes[SpellEnums.Attributes.Damage] + " Element: " + element;
        asString += "\nEffect Radius: " + attributes[SpellEnums.Attributes.EffectRadius] + " Range: " + attributes[SpellEnums.Attributes.Range];
        asString += "\n Speed: " + attributes[SpellEnums.Attributes.Speed] + " Type: " + type;
        return asString;
    }
}
