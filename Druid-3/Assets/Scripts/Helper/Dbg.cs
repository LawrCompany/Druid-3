using UnityEngine;


namespace Helper
{
    public static class Dbg
    {
        public static bool IsDebug = true;
        public static void Log(object value)
        {
            if (IsDebug)
            {
                Debug.Log(value);
            }
        }
    }
}