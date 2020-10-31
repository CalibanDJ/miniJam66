﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> BaseAdditives { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    // Start is called before the first frame update
    public BaseStat(int baseValue, string statName, string statDescription)
    {
        BaseAdditives = new List<StatBonus>();
        BaseValue = baseValue;
        StatName = statName;
        StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public int GetCalculatedStatValue()
    {
        BaseAdditives.ForEach(x => FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }
}
