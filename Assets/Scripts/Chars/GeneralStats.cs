using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStats
{
    //Base Statistics
    public string Name;
    public int Health = 560;
    public int Mana = 340;
    public float HealthRegen = 5.5f;
    public float ManaRegen = 11f;
    public float Armor = 25f;
    public float MagicResist = 30f;
    public float AttackDamage = 56f;
    public float AbilityPower = 0f;
    public float CritDamage = 1.75f;
    public int AttackRange = 450;

    public int MoveSpeed = 8;
    
    //AttackSpeed
    public float BaseAttackSpeed = 0.9f;
    public float AttackWinUp = 0.15f; //Para definir en qué momento de la animacion aplicamos daño.
    public float AS_Ratio = 0f;
    public float BonusAs = 0f;

    public int MissileSpeed = 10;

    public int Level = 1;
    public int NeededExperience = 100;
    public int Experience = 0;
    

   
    /*
    public void EarnExperience(int exp)
    {
        Experience += exp;
        if (Experience >= NeededExperience)
        {
            int rest = Experience - NeededExperience;
            //LevelUp();
            Experience = 0;
            EarnExperience(rest);
        }
    }

    public void LevelUp()
    {
        Health += 10;
        Strength += 3;
        Defense += 2;
        Magic += 1;

        NeededExperience += (int)(NeededExperience * 0.1f);
    }
    */
    public void ShowInfo()
    {
        //Debug.Log("TERMINAR MOSTRAR INFO ^^");

        Debug.Log("Name: " + Name + "\n Health: " + Health);
    }

   //Constructor
   public GeneralStats()
   {
       Name = "UnNamed";
   }

   public GeneralStats(string name)
   {
       Name = name;
   }
   
   //Faltaria el que construye tod como dios manda.....
}
