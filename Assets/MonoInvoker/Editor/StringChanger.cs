using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringChanger 
{
    public static Dictionary<string,string> dic= new Dictionary<string, string>(){
        {"Single","float"},
        {"String","string"},
        {"Int32","int"},
        {"Int64","long"},
        {"Double","double"},
    };

    public static string Change(string name){
        if(dic.ContainsKey(name))
            return dic[name];
        return name;
    }

    
}
