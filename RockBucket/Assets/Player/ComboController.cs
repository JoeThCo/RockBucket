using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class ComboController
{
    private static ComboScriptableObject[] allCombos;

    static Dictionary<ComboScriptableObject, int> ComboDictionary = new Dictionary<ComboScriptableObject, int>();

    public delegate void ComboUpdated(string combo, int points);
    public static event ComboUpdated OnComboUpdated;

    public delegate void HighScore(int score);
    public static event HighScore OnHighScore;

    private static int highScore = 0;

    public static void Load()
    {
        allCombos = Resources.LoadAll<ComboScriptableObject>("Combo");
        OnHighScore += ComboController_OnHighScore;
    }

    private static void ComboController_OnHighScore(int score)
    {
        highScore = score;
    }

    private static ComboScriptableObject Get(string name)
    {
        foreach (ComboScriptableObject obj in allCombos)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    public static void Add(string name)
    {
        ComboScriptableObject obj = Get(name);

        if (ComboDictionary.ContainsKey(obj))
            ComboDictionary[obj]++;
        else
            ComboDictionary[obj] = 1;

        OnComboUpdated?.Invoke(GetComboString(), GetComboPoints());


        SoundEffectController.Play("ComboAdded");
    }

    public static void Clear()
    {
        int score = GetComboPoints();
        if (InBucket.IsRockInBucket && score > highScore)
        {
            OnHighScore?.Invoke(score);
        }

        ComboDictionary = new Dictionary<ComboScriptableObject, int>();
        OnComboUpdated.Invoke(string.Empty, 0);
    }

    private static int GetComboPoints() 
    {
        int total = 0;
        foreach (KeyValuePair<ComboScriptableObject, int> obj in ComboDictionary) 
        {
            total += obj.Key.Score * obj.Value;
        }

        return total;
    }

    private static string GetComboString()
    {
        StringBuilder sb = new StringBuilder();

        int i = 0;
        foreach (KeyValuePair<ComboScriptableObject, int> obj in ComboDictionary)
        {
            sb.Append($"{obj.Key.name}");
            if (obj.Value > 1)
                sb.Append($" x{obj.Value}");

            if (i + 1 < ComboDictionary.Keys.Count)
                sb.Append($" + ");

            sb.AppendLine();
            i++;
        }

        return sb.ToString();
    }
}