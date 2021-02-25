using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.IO.File;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames;
using System;
using System.ComponentModel;

public class Counters : Bonuses {
	private static ISavedGameMetadata savedGameMetadata;
	public static Dictionary<string, int> AllCounters { private set; get; } = new Dictionary<string, int> 
	{
		{"Crystals", 0},
		{"Sphere", 50},
		{"MaxDistance", 0},
		{"NowDistance", 0},
		{"SecondSpent", 0},
		{"AllSecondSpent", 0},
		{"StartTime", 20},
		{"ClockTime", 7},
		{"EnemyTime", 4}
	};
	private void Awake()
	{
		 if(PlayGamesPlatform.Instance.IsAuthenticated())
			LoadData(savedGameMetadata);
		 else
			LoadDataLocal();
		PreviousRecord = AllCounters["MaxDistance"];
	}

	public static bool IsNewRecord => PreviousRecord < AllCounters["MaxDistance"];
	public static int PreviousRecord { get; private set; }

	protected static void SetMaxDistance(int Count)
	{
		PreviousRecord = AllCounters["MaxDistance"];
		if (AllCounters["MaxDistance"] < Count)
		{
			if(PlayGamesPlatform.Instance.IsAuthenticated())
			Social.ReportScore(Count, "CgkIlY_xuuELEAIQAA" , (bool success) => { });
			AllCounters["MaxDistance"] = Count;
		}
		AllCounters["NowDistance"] = Count;
    
	}
	protected static void EncreaseSphere()
    {
		AllCounters["Sphere"]++;
    }
	protected static void EncreaseCrystals()
	{
		AllCounters["Crystals"]++;
	}

	public static int SphereCost => (AllCounters["NowDistance"] / 50 +1)*10;
	protected static bool CheckSphere() => SphereCost <= AllCounters["Sphere"];
	protected static void Сontinue()
	{
		if (AllCounters["Sphere"] <= SphereCost) return;
		AllCounters["Sphere"]-=SphereCost;
		TimeManager.Instance.TimeUpContinue();
	}

	protected static void EncreaseMaxDistance(int Count)
	{
		if(AllCounters["MaxDistance"]<Count)
			AllCounters["MaxDistance"]=Count;
	}
	protected static void EncreaseDistance()
	{
		AllCounters["NowDistance"] ++;
	}
	private void SaveData(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

		SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
		builder = builder
			.WithUpdatedPlayedTime(totalPlaytime)
			.WithUpdatedDescription("Saved game at " + DateTime.Now);
		SavedGameMetadataUpdate updatedMetadata = builder.Build();
		savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
	}
	private void LoadData(ISavedGameMetadata game)
	{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
	}
	
	private byte[] Serialize() {
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream stream = new MemoryStream();
		bf.Serialize(stream, AllCounters);
		stream.Close();
		var bytes = stream.ToArray();
		return bytes;
	}

	private void Deserialize(byte[] bytes)
	{
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream stream2 = new MemoryStream(bytes);
		AllCounters = (Dictionary<string, int>)bf.Deserialize(stream2);
	}
	private void OnApplicationPause(bool status)
	{
		var fromSeconds = TimeSpan.FromSeconds(Time.time);
		if (PlayGamesPlatform.Instance.IsAuthenticated())
			SaveData(savedGameMetadata, Serialize(), fromSeconds);
		else
			SaveDataLocal();
	}

	private void OnApplicationQuit()
	{
		var fromSeconds = TimeSpan.FromSeconds(Time.time);
		if (PlayGamesPlatform.Instance.IsAuthenticated())
			SaveData(savedGameMetadata, Serialize(), fromSeconds);
		else
			SaveDataLocal();
	}
	public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			// handle reading or writing of saved game.
		}
		else
		{
			// handle error
		}
	}
	public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			Deserialize(data);
		}
		else
		{
			// handle error
		}
	}
    private void SaveDataLocal()
    {
		var path = Application.persistentDataPath + "/SaveData.dat";
		var bf = new BinaryFormatter();
		var file = Exists(path) ? OpenWrite(path) : Create(path);
		bf.Serialize(file, AllCounters);
		file.Close();
	}
	private void LoadDataLocal()
	{
		var path = Application.persistentDataPath + "/SaveData.dat";
		if (!Exists(path)) return;
		var bf = new BinaryFormatter();
		var file = OpenRead(path);
		AllCounters = (Dictionary<string, int>)bf.Deserialize(file);
		file.Close();
	}
}
