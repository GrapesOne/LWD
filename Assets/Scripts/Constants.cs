using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths
{
    private const string LevelType = "txt";
    
    public static string ToLevel(string file) => $"{P}/{R}/{L}/{file}.{LevelType}";
    public static string ShortToLevel(string file) => $"{A}/{R}/{L}/{file}.{LevelType}";
    public static string ToBase(string file) => $"{A}/{R}/{S}/{B}/{file}.asset";
    public static string ResourcesLevel(string file) => $"{L}/{file}";
    public static string ResourcesBase(string file) =>  $"{S}/{B}/{file}";
    public static readonly string ToLevels = $"{P}/{R}/{L}";
    public static readonly string ShortToLevels = $"{A}/{R}/{L}";
    public static readonly string ResourcesBases =  $"{S}/{B}";
    public static readonly string ResourcesPlayable =  $"{S}/{E}/{G}";
    public static readonly string ResourcesNotPlayable =  $"{S}/{E}/{N}";
    public static readonly string ResourcesEntities =  $"{S}/{E}";
    public const string ResourcesLevels = L;
    private const string A = "Assets";
    private const string R = "Resources";
    private const string S = "ScriptableObject";
    private const string B = "Bases";
    private const string E = "Entities";
    private const string N = "NotPlayable";
    private const string G = "Playable";
    private const string L = "Levels";
    private static readonly string P = $"{Application.dataPath}";
    
}

