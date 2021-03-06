using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {

	public string name;
	public int score;
	public int difficulty;
    public string difficultyString;

    public Player (string name, int score, int difficulty)
    {
		this.name = name;
		this.score = score;
        this.difficulty = difficulty;
	}

    public Player(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public int Score
	{
		get { return score; }
		set { score = value; }
	}

	public int Difficulty
	{
		get { return difficulty; }
		set { difficulty = value; }
	}
}
