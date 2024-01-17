using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScientificNumber
{
    public double Coefficient;
    public int Exponent;

    /* Constructors */
    public ScientificNumber(double coefficient, int exponent)
    {
        Coefficient = coefficient;
        Exponent = exponent;
        Normalize();
    }

    public ScientificNumber(double value)
    {
        if (value == 0)
        {
            Coefficient = 0;
            Exponent = 0;
            return;
        }
        Exponent = (int)Math.Floor(Math.Log10(Math.Abs(value)));
        Coefficient = value / Math.Pow(10, Exponent);
        Normalize();
    }

    private void Normalize()
    {
        while (Math.Abs(Coefficient) >= 10 || (Math.Abs(Coefficient) < 1 && Coefficient != 0))
        {
            if (Math.Abs(Coefficient) >= 10)
            {
                Coefficient /= 10;
                Exponent++;
            }
            else
            {
                Coefficient *= 10;
                Exponent--;
            }
        }
    }

    /* ScientificNumber operator */
    public static ScientificNumber operator +(ScientificNumber a, ScientificNumber b)
    {
        // Gestion des cas où l'un des opérandes est null
        if (a.Exponent > b.Exponent)
        {
            return new ScientificNumber(a.Coefficient + b.Coefficient / Math.Pow(10, a.Exponent - b.Exponent), a.Exponent);
        }
        else
        {
            return new ScientificNumber(a.Coefficient / Math.Pow(10, b.Exponent - a.Exponent) + b.Coefficient, b.Exponent);
        }
    }

    public static ScientificNumber operator -(ScientificNumber a, ScientificNumber b)
    {
        if (a.Exponent > b.Exponent)
        {
            return new ScientificNumber(a.Coefficient - b.Coefficient / Math.Pow(10, a.Exponent - b.Exponent), a.Exponent);
        }
        else
        {
            return new ScientificNumber(a.Coefficient / Math.Pow(10, b.Exponent - a.Exponent) - b.Coefficient, b.Exponent);
        }
    }

    public static ScientificNumber operator *(ScientificNumber a, ScientificNumber b)
    {
        return new ScientificNumber(a.Coefficient * b.Coefficient, a.Exponent + b.Exponent);
    }

    public static ScientificNumber operator /(ScientificNumber a, ScientificNumber b)
    {
        return new ScientificNumber(a.Coefficient / b.Coefficient, a.Exponent - b.Exponent);
    }

    // Opérateurs de comparaison
    public static bool operator ==(ScientificNumber a, ScientificNumber b)
    {
        return a.Coefficient == b.Coefficient && a.Exponent == b.Exponent;
    }

    public static bool operator !=(ScientificNumber a, ScientificNumber b)
    {
        return !(a == b);
    }

    public static bool operator >=(ScientificNumber a, ScientificNumber b)
    {
        if (a.Exponent == b.Exponent)
            return a.Coefficient >= b.Coefficient;
        return a.Exponent > b.Exponent;
    }

    public static bool operator <=(ScientificNumber a, ScientificNumber b)
    {
        if (a.Exponent == b.Exponent)
            return a.Coefficient <= b.Coefficient;
        return a.Exponent < b.Exponent;
    }

    /* Double operator */
    public static ScientificNumber operator +(ScientificNumber a, double b)
    {
        return a + new ScientificNumber(b);
    }

    public static ScientificNumber operator -(ScientificNumber a, double b)
    {
        return a - new ScientificNumber(b);
    }

    public static ScientificNumber operator *(ScientificNumber a, double b)
    {
        return a * new ScientificNumber(b);
    }

    public static ScientificNumber operator /(ScientificNumber a, double b)
    {
        return a / new ScientificNumber(b);
    }
    public static bool operator ==(ScientificNumber a, double b)
    {
        return a == new ScientificNumber(b);
    }

    public static bool operator !=(ScientificNumber a, double b)
    {
        return a != new ScientificNumber(b);
    }

    public static bool operator >=(ScientificNumber a, double b)
    {
        return a >= new ScientificNumber(b);
    }

    public static bool operator <=(ScientificNumber a, double b)
    {
        return a <= new ScientificNumber(b);
    }

    /* Comparison operator */
    public override string ToString()
    {
        return FormatNumber();
    }

    private string FormatNumber()
    {
        // Implémentation d'une logique de formatage plus détaillée
        if (Exponent < 6)
        {
            var customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberGroupSeparator = ".";
            // Format avec des points pour les milliers
            return string.Format(customCulture,"{0:N0}", Coefficient * Math.Pow(10, Exponent));
        }
        else
        {
            // Trouver le nom correct en fonction de l'exposant
            var formattedName = GetFormattedName(Exponent);
            var roundedCoeff = Math.Round(Coefficient * Math.Pow(10, Exponent % 3), 3);
            return $"{roundedCoeff} {formattedName}";
        }
    }

    private string GetFormattedName(int exponent)
    {
        // Ajouter une correspondance entre l'exposant et son nom
        var names = new Dictionary<int, string>
        {
            {6, "Million"},
            {9, "Billion"},
            {12, "Trillion"},
            {15, "Quadrillion"},
            {18, "Quintillion"},
            {21, "Sextillion"},
            {24, "Septillion"},
            {27, "Octillion"},
            {30, "Nonillion"},
            {33, "Decillion"},
            {36, "Undecillion"},
            {39, "Duodecillion"},
            {42, "Tredecillion"},
            {45, "Quattuordecillion"},
            {48, "Quindecillion"},
            {51, "Sexdecillion"},
            {54, "Septendecillion"},
            {57, "Octodecillion"},
            {60, "Novemdecillion"},
            {63, "Vigintillion"},
            {66, "Unvigintillion"},
            {69, "Duovigintillion"},
            {72, "Trevigintillion"},
            {75, "Quattuorvigintillion"},
            {78, "Quinvigintillion"},
            {81, "Sexvigintillion"},
            {84, "Septenvigintillion"},
            {87, "Octovigintillion"},
            {90, "Novemvigintillion"},
            {93, "Trigintillion"},
            {96, "Untrigintillion"},
            {99, "Duotrigintillion"},
            {102, "Tretrigintillion"},
            {105, "Quattuortrigintillion"},
            {108, "Quintrigintillion"},
            {111, "Sextrigintillion"},
            {114, "Septentrigintillion"},
            {117, "Octotrigintillion"},
            {120, "Novemtrigintillion"},
            {123, "Quadragintillion"},
            {126, "Unquadragintillion"},
            {129, "Duoquadragintillion"},
            {132, "Trequadragintillion"},
            {135, "Quattuorquadragintillion"},
            {138, "Quinquadragintillion"},
            {141, "Sexquadragintillion"},
            {144, "Septenquadragintillion"},
            {147, "Octoquadragintillion"},
            {150, "Novemquadragintillion"},
            {153, "Quinquagintillion"},
            {156, "Unquinquagintillion"},
            {159, "Duoquinquagintillion"},
            {162, "Trequinquaintillion"},
            {165, "Quattuorquinquagintillion"},
            {168, "Quinquinquagintillion"},
            {171, "Sexquinquagintillion"},
            {174, "Septenquinquagintillion"},
            {177, "Octoquinquagintillion"},
            {180, "Novemquinquagintillion"},
            {183, "Sexagintillion"},
            {186, "Unsexagintillion"},
            {189, "Duosexagintillion"},
            {192, "Tresexagintillion"},
            {195, "Quattuorsexagintillion"},
            {198, "Quinsexagintillion"},
            {201, "Sexsexagintillion"},
            {204, "Septensexagintillion"},
            {207, "Octosexagintillion"},
            {210, "Novemsexagintillion"},
            {213, "Septuagintillion"},
            {216, "Unseptuagintillion"},
            {219, "Duoseptuagintillion"},
            {222, "Treseptuagintillion"},
            {225, "Quattuorseptuagintillion"},
            {228, "Quinseptuagintillion"},
            {231, "Sexseptuagintillion"},
            {234, "Septenseptuagintillion"},
            {237, "Octoseptuagintillion"},
            {240, "Novemseptuagintillion"},
            {243, "Octogintillion"},
            {246, "Unoctogintillion"},
            {249, "Duooctogintillion"},
            {252, "Treoctogintillion"},
            {255, "Quattuoroctogintillion"},
            {258, "Quinoctogintillion"},
            {261, "Sexoctogintillion"},
            {264, "Septenoctogintillion"},
            {267, "Octooctogintillion"},
            {270, "Novemoctogintillion"},
            {273, "Nonagintillion"},
            {276, "Unnonagintillion"},
            {279, "Duononagintillion"},
            {282, "Trenonagintillion"},
            {285, "Quattornonagintillion"},
            {288, "Quinnonagintillion"},
            {291, "Sexnonagintillion"},
            {294, "Septennonagintillion"},
            {297, "Octononagintillion"},
            {300, "Novemnonagintillion"},
            {303, "Centillion"},
            {306, "Uncentillion"},
            {309, "Duocentillion"},
            {312, "Trecentillion"},
            {315, "Quattuorcentillion"},
            {318, "Quincentillion"},
            {321, "Sexcentillion"},
            {324, "Septencentillion"},
            {327, "Octocentillion"},
            {330, "Novemcentillion"},
            {333, "Decicentillion"},
            {336, "Undecicentillion"},
            {339, "Duodecicentillion"},
            {342, "Tredecicentillion"},
            {345, "Quattuordecicentillion"},
            {348, "Quindecicentillion"},
            {351, "Sexdecicentillion"},
            {354, "Septendecicentillion"},
            {357, "Octodecicentillion"},
            {360, "Novemdecicentillion"},
            {363, "Viginticentillion"},
            {366, "Unviginticentillion"},
            {369, "Duoviginticentillion"},
            {372, "Trevigintacentillion"},
            {375, "Quattuorviginticentillion"},
            {378, "Quinviginticentillion"},
            {381, "Sexviginticentillion"},
            {384, "Septenviginticentillion"},
            {387, "Octoviginticentillion"},
            {390, "Novemviginticentillion"},
            {393, "Trigintacentillion"},
            {396, "Untrigintacentillion"},
            {399, "Duotrigintacentillion"},
            {402, "Tretrigintacentillion"},
            {405, "Quattuortrigintacentillion"},
            {408, "Quintrigintacentillion"},
            {411, "Sextrigintacentillion"},
            {414, "Septentrigintacentillion"},
        };

        if (names.TryGetValue((exponent - exponent % 3), out var name))
        {
            return name;
        }
        else
        {
            // Pour les valeurs non mappées, retourner la notation scientifique
            return $"e{exponent}";
        }
    }
}