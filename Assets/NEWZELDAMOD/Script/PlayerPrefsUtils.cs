using UnityEngine;

public static class PlayerPrefsUtils
{
    public static void SetVector3(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "_X", value.x);
        PlayerPrefs.SetFloat(key + "_Y", value.y);
        PlayerPrefs.SetFloat(key + "_Z", value.z);
        PlayerPrefs.Save();
    }

    public static Vector3 GetVector3(string key, Vector3 defaultValue)
    {
        if (PlayerPrefs.HasKey(key + "_X") && PlayerPrefs.HasKey(key + "_Y") && PlayerPrefs.HasKey(key + "_Z"))
        {
            float x = PlayerPrefs.GetFloat(key + "_X");
            float y = PlayerPrefs.GetFloat(key + "_Y");
            float z = PlayerPrefs.GetFloat(key + "_Z");
            return new Vector3(x, y, z);
        }
        return defaultValue;
    }
}
