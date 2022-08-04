using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameGeneration : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "qu", "r", "s", "sh"/*, "zh"*/, "t", "v", "w", "x" };
    string[] vowels = { "a", "e", "i", "o", "u"/*, "ae"*/, "y" };

    string RandConsonant()
    {
        return consonants[Random.Range(0, consonants.Length)];
    }

    string RandVowel()
    {
        return vowels[Random.Range(0, vowels.Length)];
    }

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        string name = "";

        int length = Random.Range(4, 10);

        // Generate the first name
        name += RandConsonant();
        name = name.Substring(0, 1).ToUpper();
        name += RandVowel();

        int l = 2;
        int consinrow = 0;

        while (l < length)
        {
            if (Random.Range(0, 5) >= consinrow)
            {
                name += RandConsonant();
                consinrow++;
            }
            else
            {
                name += RandVowel();
                consinrow = 0;
            }

            l++;

            //name += Random.Range(0, 2) == 0 ? RandConsonant() : RandVowel();
            //name += RandConsonant();
            //l++;
            //name += RandVowel();
            //l++;

        }

        // Generate the last name

        text.text = name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Generate();
        }
    }
}
