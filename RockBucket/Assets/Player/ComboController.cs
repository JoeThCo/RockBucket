using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class ComboController
{
    private static ComboScriptableObject[] allCombos;

    static Dictionary<ComboScriptableObject, int> ComboDictionary = new Dictionary<ComboScriptableObject, int>();

    public delegate void ComboUpdated(string combo);
    public static event ComboUpdated OnComboUpdated;

    public static void Load()
    {
        allCombos = Resources.LoadAll<ComboScriptableObject>("Combo");
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

        OnComboUpdated?.Invoke(GetComboString());
    }

    public static void Clear()
    {
        ComboDictionary = new Dictionary<ComboScriptableObject, int>();
        OnComboUpdated.Invoke(string.Empty);
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