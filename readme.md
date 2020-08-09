# MonoInvoker 

## step

1. In monobehaviour class, add [TestMethod] above method. you want simply call.
2. now you can see method call interface in inspector.

# Caution

1.You can [TestMethod(false)] if you dont want play in editor mode. 
    basically, if a method contains StartCoroutine, it doent work in editor mode.
    
2.If you use custom editor. editor class have to inherit MonoInvoker.MonoBehaviourEditor not UnityEditor.Editor.

```csharp
//part of EditorClass
[CustomEditor (typeof (Tester), true)]
public class TestEditor : MonoInvoker.MonoBehaviourEditor {
     public override void OnInspectorGUI () {
         base.OnInspectorGUI();
         
         //your code.
     }
}
```

