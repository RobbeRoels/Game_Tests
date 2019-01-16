using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
    public BoardCreator.TileType[][] _tiles;                               // A jagged array of tile types representing the board, like a grid.
    public Room[] _rooms;                                     // All the rooms that are created for this board.
    public Corridor[] _corridors;                             // All the corridors that connect the rooms.
    public int _level;
    public Feeling _dungeonFeeling; 


    public Board(BoardCreator.TileType[][] tiles, Room[] rooms, Corridor[] corridors, int level) {
        _tiles = tiles;
        _rooms = rooms;
        _corridors = corridors;
        _level = level;
        var enumVariable = Enum.GetValues(typeof(Feeling));
        var random = new System.Random().Next(enumVariable.Length);

        _dungeonFeeling = Feeling.NONE;//(Feeling)enumVariable.GetValue(random);

       }

    public enum Feeling { HAPPINESS, SURPRISE, HOPE, LONELYNESS, SADNESS, FEAR, NONE }

}
