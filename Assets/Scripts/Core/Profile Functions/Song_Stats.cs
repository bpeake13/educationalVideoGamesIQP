using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Song_Stats {

	public string songName = "n/a";
	public float bestScore = 0f;
	public float totalScore = 0f;
	public float meanScore = 0f;
	public int mostHits = 0;
	public int totalEnemiesDefeated = 0;
	public float meanEnemiesDefeated = 0f;
	public int mostEnemiesDefeated = 0;
	public int fewestMisses = 999;
	public int attempts = 0;
}
