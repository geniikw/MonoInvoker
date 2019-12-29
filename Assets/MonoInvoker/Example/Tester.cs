using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    //you can call this method in play mode
    //if you want call editor mode, use [TestMethod(true)] or [TestMethod]
    [TestMethod(false)]
    void Start() {
        Debug.Log("Start()");
    }
    
    [TestMethod]
    public void UnityParamTest(Material material, Color color, Vector3 v3){
        Debug.Log(material.name);
    }

     //you can call coroutine in playing mode.
    [TestMethod]
    IEnumerator Coroutine(int delay, string msg){
        yield return new WaitForSeconds(delay);
        Debug.Log(msg);
    }

    //you can change parameter you want in inspector
    //I recommend set default value.
    [TestMethod]
    void SimpleCall(string s = "!"){
        Debug.Log(s);
    }

    //you can handle custom class but nested not yet.
    [TestMethod]
    void SimpleCall(Foo foo1,Vector3 v3, Color color, float fv, string st){
        Debug.Log(foo1.b);
    }

    [TestMethod]
    void Call(int intvalue, float floatvalue, double doubleValue, long longValue){

    }

    public class Foo{
        public int a = 1;
        public string b = "created by Kim giwon";
    }

}
