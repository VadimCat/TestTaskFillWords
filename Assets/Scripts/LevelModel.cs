using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

public class LevelModel
{
    private readonly LevelConfig _levelConfig;

    public int LevelWidth => _levelConfig.Filling.GetLength(0);
    public int LevelHeight => _levelConfig.Filling.GetLength(1);

    public event Action<Vector2Int, char> CellUnlock;
    public event Action InputFail;

    private HashSet<string> _unlockedWords = new();

    public LevelModel(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    public void TryUnlockWord(string word)
    {
        if (!_levelConfig.AvailableWords.Contains(word) && _unlockedWords.Contains(word))
        {
            InputFail?.Invoke();
        }
        else
        {
            List<List<Vector2Int>> letterPoses = FindMatchingLettersPositions(word);

            var path = TryFindLine(letterPoses);
            
            bool isWordFound = path.Count == word.Length;

            if (isWordFound)
            {
                _unlockedWords.Add(word);
                foreach (var position in path)
                {
                    CellUnlock?.Invoke(position, _levelConfig.Filling[position.x, position.y]);
                }
            }
            else
            {
                InputFail?.Invoke();
            }
        }
    }

    private List<List<Vector2Int>> FindMatchingLettersPositions(string word)
    {
        var letterPoses = new List<List<Vector2Int>>();
        Dictionary<char, List<Vector2Int>> lettersPosesDict = new(word.Length);
        
        foreach (var letter in word)
        {
            if(lettersPosesDict.ContainsKey(letter))
                continue;
            
            List<Vector2Int> poses = new List<Vector2Int>();
            lettersPosesDict[letter] = (poses);

            for (int i = 0; i < _levelConfig.Filling.GetLength(0); i++)
            {
                for (int j = 0; j < _levelConfig.Filling.GetLength(1); j++)
                {
                    if (letter == _levelConfig.Filling[i, j])
                    {
                        poses.Add(new Vector2Int(i, j));
                    }
                }
            }
        }

        foreach (var letter in word)
        {
            letterPoses.Add(lettersPosesDict[letter]);
        }
        
        return letterPoses;
    }

    private List<Vector2Int> TryFindLine(List<List<Vector2Int>> letterPoses,
        List<Vector2Int> usedElements = null, int i = 0)
    {
        List<Vector2Int> bestMatch = new();

        if (usedElements == null)
        {
            usedElements = new List<Vector2Int>();
        }

        foreach (var pos in letterPoses[i])
        {
            if (!usedElements.Contains(pos) && IsInLine(usedElements, pos))
            {
                usedElements.Add(pos);
                if (i + 1 < letterPoses.Count)
                {
                    int a = i + 1;
                    var currentMatch = TryFindLine(letterPoses, new List<Vector2Int>(usedElements), a);
                    if (bestMatch.Count < currentMatch.Count)
                    {
                        bestMatch = currentMatch;
                    }
                }
                else
                {
                    if (bestMatch.Count < usedElements.Count)
                    {
                        bestMatch = new List<Vector2Int>(usedElements);
                    }
                }

                usedElements.RemoveAt(usedElements.Count - 1);
            }
        }

        return bestMatch;
    }

    private bool IsInLine(List<Vector2Int> usedElements, Vector2Int pos)
    {
        //point starts the line
        if (usedElements.IsEmpty())
            return true;

        if (IsNearbyPoints(usedElements[^1], pos))
        {
            //Any of two nearby points forms a line 
            if (usedElements.Count == 1)
            {
                return true;
            }

            //if two cells with one cell between has len = 2 or len^2 = 4 they are in line two
            return IsInLineThroughOne(usedElements[^2], pos);
        }

        return false;
    }

    private bool IsInLineThroughOne(Vector2Int p1, Vector2Int p2)
    {
        return Mathf.Approximately(GetSqrDist(p1, p2), 4);
    }

    //TODO: Move to extensions
    private bool IsNearbyPoints(Vector2Int p1, Vector2Int p2)
    {
        int xDelta = Mathf.Abs(p1.x - p2.x);
        int yDelta = Mathf.Abs(p1.y - p2.y);
        return xDelta + yDelta == 1;
    }

    private float GetSqrDist(Vector2Int p1, Vector2Int p2)
    {
        return Mathf.Pow(p1.x - p2.x, 2) + Mathf.Pow(p1.y - p2.y, 2);
    }
}