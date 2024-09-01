using UnityEngine;
using UnityEditor;
using Model;
using System;
using Utility;

[CustomEditor(typeof(GridData))]

public sealed class GridDataEditor : Editor
{
    private GridData _gridData;
    private GUILayoutOption[] _buttonLayout = new GUILayoutOption[2]
    {
        GUILayout.MaxWidth(30),
        GUILayout.MaxHeight(30)
    };
    
    private SerializedProperty _tileTypeGrid;
    private SerializedProperty _width;
    private SerializedProperty _height;
    private SerializedProperty _gridDataType;
    private SerializedProperty _gridMirrorMode;
    

    private void OnEnable() 
    {
        _tileTypeGrid = serializedObject.FindProperty("TileGrid");
        _width = serializedObject.FindProperty("Width");
        _height = serializedObject.FindProperty("Height");
        _gridDataType = serializedObject.FindProperty("GridDataType");
        _gridMirrorMode = serializedObject.FindProperty("GridMirrorMode");
    }   
 
    public override void OnInspectorGUI()
    {
        if (_tileTypeGrid == null) return;
        serializedObject.Update();
 
        _gridData = (GridData)target;
        _gridData.FillTileGridIfEmpty();

        DrawWitdhHeightSliders();

        DrawEnumFields();

        DrawButtons();
 
        DrawGrid();
 
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawWitdhHeightSliders()
    {
        int initialWidth = _gridData.Width;
        int initialHeight = _gridData.Height;

        int width = EditorGUILayout.IntSlider("Width", _gridData.Width, 1, 25); 
        _width.intValue = width;
        int height = EditorGUILayout.IntSlider("Height", _gridData.Height, 1, 25);
        _height.intValue = height;

        if (initialWidth != width || initialHeight != height) _gridData.TransferTilesToNewGrid();
    }

    private void DrawEnumFields()
    {
        GridDataType gridDataType = (GridDataType)EditorGUILayout.EnumPopup("GridType", _gridData.GridDataType);
        _gridDataType.enumValueIndex = (int)gridDataType;
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("ResetGrid")) 
        {
            _gridData.ReinstantiateGrid();
        }
    }
 
    //TO DO replace GUILayout calls
    private void DrawGrid()
    {
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        
        for (int y = 0; y < _gridData.Height; y++)
        {    
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            SerializedProperty tileTypeColumn = _tileTypeGrid.GetArrayElementAtIndex(y).FindPropertyRelative("TileTypesRow");
            
            for (int x = 0; x < _gridData.Width; x++)
            {
                SerializedProperty tile = tileTypeColumn.GetArrayElementAtIndex(x);
                TileType tileType = (TileType)tile.intValue;
                
                GUI.color = GetTileColor(tileType);

                if (GUILayout.Button("", _buttonLayout))
                {
                    TileType newTileType = SwitchTileTypes(tileType);

                    tile.intValue = (int)newTileType;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
    }
 
    private TileType SwitchTileTypes(TileType initialType)
    {
        GridDataType gridDataType = _gridData.GridDataType;

        TileType newTileType = TileTypeSwitcher.SwitchTileType(initialType, gridDataType);
    
        return newTileType;
    }

    private Color32 GetTileColor(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Empty: return new Color32(70, 70, 70, 50);
            case TileType.Solid: return new Color32(220, 220, 220, 255);
            case TileType.Red: return new Color32(200, 75, 75, 255); 
            case TileType.Green: return new Color32(75, 200, 75, 255); 
            case TileType.Blue: return new Color32(75, 75, 200, 255);
            default: throw new NotImplementedException();
        }
    }
}
