// <copyright file="SaveManager.cs" company="IZOTOP">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>ZedeV2</author>
// <summary>Class that saves data into file.</summary>

using UnityEngine;
using System.IO;

public class SaveManager {

    public static string password;

    #region WRITE
    public static void saveInt(string key, int data, string FileName)
    {
        Write(FileName, key, ""+data);
    }

    public static void saveString(string key, string data, string FileName)
    {
        Write(FileName, key, data);
    }

    public static void saveFloat(string key, float data, string FileName)
    {
        Write(FileName, key, "" + data);
    }

    public static void saveBool(string key, bool data, string FileName)
    {
        Write(FileName, key, "" + data);
    }
    #endregion

    #region READ
    public static int readInt(string key, string FileName)
    {
        string readed = ReadByKey(key, FileName);
        return int.Parse(readed);
    }

    public static string readString(string key, string FileName)
    {
        string readed = ReadByKey(key, FileName);
        return readed;
    }

    public static float readFloat(string key, string FileName)
    {
        string readed = ReadByKey(key, FileName);
        return float.Parse(readed);
    }

    public static bool readBool(string key, string FileName)
    {
        string readed = ReadByKey(key, FileName);
        return bool.Parse(readed);
    }
    #endregion

    #region FUNCTIONS
    public static int findKey(string key, string FileName)
    {
        string line = "";

        for(int i = 0; i< File.ReadAllLines(getPathFromFileName(FileName)).Length; i++)
        {
            line = File.ReadAllLines(getPathFromFileName(FileName))[i];
            if (line.Contains(key)) return i;
        }

        return -1;
    }

    private static void Replace(string text, string FileName, int i)
    {
        string[] arrLine = File.ReadAllLines(getPathFromFileName(FileName));
        arrLine[(i != 0 ? i-1 : 0)] = text;
        File.WriteAllLines(getPathFromFileName(FileName), arrLine);
    }

    private static void Write(string FileName, string key, string content)
    {
        string path = getPathFromFileName(FileName);

        string encrypted = Encrypt.EncryptString(content, password);

        if (!File.Exists(path))
        {
            File.Create(path);
        }

        if (findKey(key, FileName) != -1)
        {
            Replace(key + "=" + encrypted, FileName, findKey(key, FileName));
        }
        else
        {
            File.WriteAllText(path, key+"="+ encrypted);
        }
    }

    private static string ReadByKey(string key, string FileName)
    {
        int i = findKey(key, FileName);
        if (i == -1)
        {
            return "";
        }
        else
        {
            string toDecrypt = File.ReadAllLines(getPathFromFileName(FileName))[i].Substring(key.Length+1);
            return Encrypt.DecryptString(toDecrypt, password);
        }
    }

    public static string getPathFromFileName(string FileName)
    {
        return Application.dataPath + "/" + FileName;
    }
    #endregion
}
