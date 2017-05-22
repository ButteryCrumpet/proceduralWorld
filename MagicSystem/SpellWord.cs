using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rewrite for proper dictionary use, try as full class?
[System.Serializable]
public struct SpellWord {

    public string Name;

    public SpellEnums.Elements element;
    public SpellEnums.Types type;
    public SpellEnums.Targeting target;

    public SpellEnums.Attributes[] attributesArray;  //unsafe way of doing this!!! need to write a new dictionary class to serialise it in the editor
    public int[] attributeValues;

    public Dictionary<SpellEnums.Attributes, float> attributes
    {
        get
        {
            Dictionary<SpellEnums.Attributes, float> dict = new Dictionary<SpellEnums.Attributes, float>();
            for (int i = 0; i < attributesArray.Length; i++)
            {
                dict.Add(attributesArray[i], attributeValues[i]);
            }
            return dict;
        }
    }

    public string wordAsString
    {
        get
        {
            string info = "";

            foreach (KeyValuePair<SpellEnums.Attributes, float> attribute in attributes)
            {
                info += attribute.Key + ": " + attribute.Value;
                info += "\n";
            }

            return info;
        }
    }
}