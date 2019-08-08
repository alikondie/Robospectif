using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFillingObjects : MonoBehaviour
{

}

[Serializable]
public class CarteRejetee
{

    public int Joueur;
    public string Dimension;
    public string Locomotion;
    public string Equipement1;
    public string Equipement2;
    public string Equipement3;
}

public class Conception
{
    public int Joueur;
    public string Dimension;
    public string Locomotion;
    public string Conduite;
    public string Equipement1;
    public string Equipement2;
    public string Equipement3;

}

public class Personnage
{
    public int Joueur;
    public string Person;
    public string Environnement;
    public string UsagePropose;
}

public class PlayerInfoData{

    public int id;
    public string Nom;
    public string Prenom;
    public string Sex;
    public string Age;
    public string Specialite;
    public string Etablissement;
    public string Remarques;
}

public class Debate{

    public int Tour;
    public string Personnage;
    public int JoueurDonnant;
    public int JoueurRecevant;
    public string Jeton;
    public string Argument;


}

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> list, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}