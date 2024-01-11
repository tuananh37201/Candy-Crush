/*
    Read data from file json
*/

using System;

[System.Serializable]
public class LevelData
{
    // Board Layout
    public int _width;
    public int _height;
    public TileType[] _boardLayout;

    // Move 
    public int _move;

    // Score
    public int _goalScore;
    public int _score1;
    public int _score2;
    public int _score3;

    // Bổ sung thêm: Các loại boost, điều kiện chiến thắng

}