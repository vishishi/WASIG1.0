using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpellType
{
    Healing,

    Buff,

    Debuff
}

public class Challenge : MonoBehaviour
{
    [System.Serializable]
    public struct SpellInfo
    {
        public string spellName;
        public SpellType spellType;
        public Color glowColor;
        public float manaCost;

    }

    public SpellInfo [] spellInfo;
    
    private void Awake()
    {
        
    }
    void Start()
    {
        foreach (var spell in spellInfo)
        {
           if (spell.manaCost < 20)
            {
                Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(spell.glowColor)}>[{spell.spellType}] {spell.spellName}</color> - {spell.manaCost} mana");
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
