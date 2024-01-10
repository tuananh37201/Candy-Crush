/*
    Save data from scriptable (LevelDataScriptable) to file json
*/

using System.IO;
using UnityEngine;

public class SaveScriptable : MonoBehaviour
{
    public LevelData_Scriptable levelData; // đây là scriptable bạn muốn chuyển sang json
    public string fileName; // đây là tên file json bạn muốn lưu

    private void Start()
    {
        fileName = levelData.ToString() + ".json";
        string json = JsonUtility.ToJson(levelData); // chuyển đổi scriptable thành json
        string path = "Assets/Resources/Level" + "/" + fileName; // tạo đường dẫn đến file json
        File.WriteAllText(path, json); // lưu json vào file
        Debug.Log("Saved scriptable to json file: " + path); // in ra thông báo
    }
}
