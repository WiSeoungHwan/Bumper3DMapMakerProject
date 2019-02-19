using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryData: MonoBehaviour{

    public static void BinarySerialize<T>(T t, string fileName){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(fileName, FileMode.Create);
        formatter.Serialize(fileStream, t);
        fileStream.Close();
    }
    public static T BinaryDeSerialize<T>(string filePath){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath,FileMode.Open);
        T t = (T)formatter.Deserialize(fileStream);
        fileStream.Close();
        return t ;
    }
    public static T DeserializeObject<T>(byte[] serilizedBytes)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(serilizedBytes))
        {
            return (T)formatter.Deserialize(stream);
        }
    }

	
}
