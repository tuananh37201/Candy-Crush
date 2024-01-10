/*
    Enter data from Inspector
*/

using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataScriptable", menuName = "Scriptable/Level", order = 0)]
public class LevelData_Scriptable : ScriptableObject
{
    [Header("Board Dimensions")]
        public int _width;
        public int _height;

        [Header("Starting Tiles")]
        public TileType[] _boardLayout;

        // [Header("Available Candys")]
        // public GameObject[] candys;

        // [Header("Score Goals")]
        // public int EndGameScore;
}