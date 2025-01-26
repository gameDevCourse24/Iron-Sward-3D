using UnityEngine;
using System.Runtime.CompilerServices;
//this script is used to log messages with the class name and the method name that called print.p fron any script in the project
public static class pprint
{
    public static void p(
        string message, 
        Object context = null, // מקבל את האובייקט שקורא לפונקציה
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string callerName = ""
    )
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath); // מחלץ את שם המחלקה מהנתיב
        string objectName = context != null ? context.name : "UnknownObject"; // אם יש אובייקט, מחלץ את שמו

        Debug.Log($"[{className}.{callerName}] (Object: {objectName}) {message}", context);
    }
}
