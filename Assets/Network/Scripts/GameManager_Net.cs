using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager_Net : MonoBehaviour {

    public static GameManager_Net instance;
    public MatchSettings matchSettings;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene.");
        }
        else
        {
            instance = this;
        }
    }



    #region Player Tracking 
    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player_Net> players = new Dictionary<string, Player_Net>();

    public static void RegisterPlayer(string _netID, Player_Net _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }
    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player_Net GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    //void OnGUI ()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string _playerID in players.Keys)
    //    {
    //        GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}
    #endregion
}
