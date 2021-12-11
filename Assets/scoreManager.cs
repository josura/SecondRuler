using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using System;

public class scoreManager : MonoBehaviour {

    [SerializeField] GameObject endGUI;
    private Score[] scores;
    [SerializeField] Text[] PlaceName = new Text[5];
    [SerializeField] Text[] PlaceScore = new Text[5];

    public class Score
    {
        public string name;
        public int score;

        public Score()
        {
            name = "******";
            score = 0;
        }

        public Score(string nome, int scr)
        {
            name = nome;
            score = scr;
        }

        public void assign(string nome, int scr)
        {
            name = nome;
            score = scr;
        }

        public static bool operator <(Score a, Score b) { return a.score < b.score; }
        public static bool operator >(Score a, Score b) { return a.score > b.score; }
        public static bool operator ==(Score a, Score b) { return a.score == b.score; }
        public static bool operator !=(Score a, Score b) { return a.score != b.score; }
        public static bool operator <=(Score a, Score b) { return a < b || a == b; }
        public static bool operator >=(Score a, Score b) { return a > b || a == b; }
        
    }

    [System.Serializable]
    public class scoreData
    {
        public string[] orderedNames;
        public int[] orderedScores;
    }

    public scoreData scoredat = new scoreData();
    private static scoreManager _instance;

    public static bool isGameEnded;
    [SerializeField] bool esterno;
    public static scoreManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);   //to keep the singleton between scenes
        scores = new Score[5];
        for (int i = 0; i < 5; i++)
        {
            scores[i] = new Score();
        }
        scoredat.orderedNames = new string[5];
        scoredat.orderedScores = new int[5];
        for (int i = 0; i < 5; i++)
        {
            scoredat.orderedNames[i] = "******";
            scoredat.orderedScores[i] = 0;
        }
        
    }

    private void Start()
    {
        initializeBoard();
        
    }

    private void Update()
    {
        
    }

    public void printScores()
    {
        string pip = "";
        foreach(Score sc in scores)
        {
            pip += sc.name + " " + sc.score + ",";
        }
        Debug.Log(pip);
    }

    int contains(Score[] arr,Score name)
    {
        for (int i = 0; i < 5; i++)
        {
            if (arr[i].name==name.name)
            {
                return i;
            }
        }
        return -1;
    }

    int indexMin(Score[] arr)
    {
        int min = -1,index = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (min>arr[i].score)
            {
                min = arr[i].score;
                index = i;
            }
        }
        return index;
    }

    void insertSorted(Score[] arr, Score key)
    {
        int index = contains(scores, key);
        if (index>=0)
        {
            if (arr[index] < key)
            {
                arr[index].score = key.score;
                for (int i = index; i > 0; i--)
                {
                    if (arr[i] > arr[i - 1])
                    {
                        Score temp = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = temp;
                    }

                }
               
            }
            return;
        }
        int pos=-1;
        for (int i = 0; i < 5 && pos<0; i++)
        {
            if (key>=arr[i] )
            {
                pos = i;
            }
        }
        Debug.Log("posizione"+pos);
        if (pos >= 0)
        {
            for (int i = 3; i > pos; i--)
                arr[i+1] = arr[i];
            arr[pos] = key;
        }

    }

    public void addscore(string name, int score)
    {
        Score newTemp = new Score(name, score);
        insertSorted(scores,newTemp);
        /*int index = contains(scores, newTemp);
        if (index>=0)
        {
            if (scores[index].score < newTemp.score)
            {
                scores[index] = newTemp;
            }

        }
        else
        {
            int indesmin = indexMin(scores);
            if (indesmin >= 0)
            {
                if (newTemp.score > scores[indesmin].score)
                {
                    scores[indesmin] = newTemp;
                }
            }
        }
        Array.Sort<Score>(scores,delegate(Score m , Score n) { return n.score - m.score; });*/

        
    }

    private void  initializeBoard()
    {

        for (int i = 0;i < 5; i++)
        {
            PlaceName[i].text = "******";
            PlaceScore[i].text = "0";
        }
    }

    public void displayScoreboard()
    {
        /*string[] mykeys = new string[scoreDictionary.Count];
        int[] myvalues = new int[scoreDictionary.Count];
        scoreDictionary.Keys.CopyTo(mykeys, 0);
        scoreDictionary.Values.CopyTo(myvalues, 0);*/

        for (int i = 0; i < scores.Length && i < 5; i++)
        {
            PlaceName[i].text = scores[i].name;
            PlaceScore[i].text = scores[i].score.ToString();
        }

    }
    public void saveScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        
        FileStream file = File.Open(Application.persistentDataPath + "/scoredat.data", FileMode.OpenOrCreate);
        /*string[] mykeys = new string[scoreDictionary.Count];
        int[] myvalues = new int[scoreDictionary.Count];
        scoreDictionary.Keys.CopyTo(mykeys, 0);
        scoreDictionary.Values.CopyTo(myvalues, 0);*/
        for (int i = 0; i < scores.Length && i < 5; i++)
        {
            if (scores[i].name[0] != '*')
            {
                scoredat.orderedNames[i] = scores[i].name;
                scoredat.orderedScores[i] = scores[i].score;
                Debug.Log("salvando score" + scores[i].name + " " + scores[i].score);
            }
            
        }
        bf.Serialize(file, scoredat);
        file.Close();
    }

    public void loadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/scoredat.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scoredat.data", FileMode.Open);

            scoredat = (scoreData)bf.Deserialize(file);

            file.Close();
            for (int i = 0; i < scoredat.orderedNames.Length; i++)
            {
                //if ( scoredat.orderedNames[i][0]!='*')
                //{
                    scores[i].assign(scoredat.orderedNames[i], scoredat.orderedScores[i]);
                    Debug.Log("caricando score" + scoredat.orderedNames[i] + " " + scoredat.orderedScores[i]);
                //}
                
            }
        }
        else
        {
            Debug.LogError("scores file not found");
        }
    }

}
