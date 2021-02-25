using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class LevelBase : ScriptableObject
{
    public static readonly Vector2Int size = new Vector2Int(16,16);
    public int[,] level;
    public TextAsset tex;
    public SettingsEnum.Difficulties difficult;
    public SettingsEnum.Environment environment;
    public bool Init()
    {
        if(tex==null) return false;
        level = new int[size.x,size.y];
        for (var i = 0; i < size.x; i++)
        for (var j = 0; j < size.y; j++)
            level[i, j] = EncodeFormat.From(tex.text[i * size.y + j]);

        return true;
    }
}

public static class SettingsEnum
{
    public static Dictionary<Difficulties, string> DifficultiesToString = new Dictionary<Difficulties, string>
    {
        {Difficulties.easy, "e"},
        {Difficulties.medium, "m"},
        {Difficulties.hard, "h"},
        {Difficulties.insane, "i"},
    };
    public static Dictionary<Environment, string> EnvironmentToString = new Dictionary<Environment, string>
    {
        {Environment.stone, "s"},
        {Environment.winter, "w"},
        {Environment.dessert, "d"},
        {Environment.clouds, "c"},
    };
    public enum Difficulties
    {
        easy = 1,
        medium,
        hard,
        insane
    }
    public enum Environment
    {
        stone = 1,
        winter,
        dessert,
        clouds
    }
}

public static class EncodeFormat
{
    public const char slf = (char)('0'-20);
    public const int cslf = 3;
    public static char To(char c) => (char) (c * cslf + slf);
    public static char To(int c) => (char) (c * cslf + slf);
    public static char From(char c) => (char) ((c - slf)/cslf) ;
    public static char From(int c) => (char) ((c - slf)/cslf) ;
}
