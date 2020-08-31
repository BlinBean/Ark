using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillMethod {
    public static void Hoshiguma03(this CharacterBase cha ,float atk,float def,float spellDef)
    {

        atk = atk + atk * 0.95f;
        def = def + def * 0.6f;
        spellDef = spellDef +spellDef* 0.6f;
        

        //Debug.Log(a);
    }
}
