using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    string filePath;

    public string saveFileName;

    public static SaveSystem instance;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/" + saveFileName + ".saveData";

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(GameData saveData)
    {
        //find a file path and create a file
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        //get the binary formatter ready
        BinaryFormatter converter = new BinaryFormatter();

        //convert the data and send it to the file
        converter.Serialize(dataStream, saveData);

        //"if not used, things break"- John 3/14/2024
        dataStream.Close();
    }
    
    public GameData LoadGame()
    {
        //check if the file already exists
        //if so get the existing file and return it

        //if the file does not exist. return an error message and cancel the function
    }
}
