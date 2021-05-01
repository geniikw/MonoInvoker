using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimpleInvoker {

    [Serializable]
    public class MethodTesterInfo {

        public string MethodName;
        public List<Parameter> list = new List<Parameter>();
        public string ViewString;
        public bool isCoroutine = false;
        public MethodTesterInfo(MethodInfo info) {
            MethodName = info.Name;
            isCoroutine = info.ReturnType.Name == "IEnumerator";

            var paramList = info.GetParameters();

            foreach (var param in paramList) {
                string typeName = StringChanger.Change(param.ParameterType.Name);

                object defaultValue = null;
                if (param.HasDefaultValue)
                    defaultValue = param.DefaultValue;
                else if (param.ParameterType.Name == "String")
                    defaultValue = "";
                else if(param.ParameterType.GetConstructor(Type.EmptyTypes)!= null || 
                        param.ParameterType.IsValueType)
                    defaultValue = Activator.CreateInstance(param.ParameterType);

                list.Add(new Parameter(param.ParameterType, typeName, defaultValue, param.Name));
            }

            ViewString = "<color=blue>" + info.ReturnType.Name + "</color> " + info.Name + "(";
            foreach (var p in list) {
                ViewString += "<color=blue>" + p.typeName + "</color> " + p.name + ",";
            }
            if (paramList.Length > 0)
                ViewString = ViewString.Remove(ViewString.Length - 1);
            ViewString += ")";
        }
    }

    [Serializable]
    public class Parameter {
        public Type type;
        public string typeName;
        public object obj;
        public string name;

        public List<ChildParam> childs;

        public Parameter(Type type, string typeName, object obj, string name) {
            this.type = type;
            this.typeName = typeName;
            this.obj = obj;
            this.name = name;

            var ms = type.GetFields().Where(t => !t.IsStatic).ToList();
            childs = new List<ChildParam>(ms.Count());

            foreach (var m in ms) {
                var ct = m.FieldType;
                var tn = StringChanger.Change(m.FieldType.Name);
                var cn = m.Name;
                var co = m.GetValue(obj);
                childs.Add(new ChildParam(ct, tn, co, cn, m));
            }
        }

        public object OnDrawParam() {
            var fieldName = typeName + " " + name;
            if (typeName == "string")
                obj = (object) EditorGUILayout.TextField(fieldName, (string) obj);
            else if (typeName == "float")
                obj = (object) EditorGUILayout.FloatField(fieldName, (float) obj);
            else if (typeName == "double")
                obj = (object) EditorGUILayout.DoubleField(fieldName, (double) obj);
            else if (typeName == "int")
                obj = (object) EditorGUILayout.IntField(fieldName, (int) obj);
            else if (typeName == "long")
                obj = (object) EditorGUILayout.LongField(fieldName, (long) obj);
            else if (typeName == "Vector3")
                obj = (object) EditorGUILayout.Vector3Field(fieldName, (Vector3) obj);
            else if (typeName == "Vector2")
                obj = (object) EditorGUILayout.Vector2Field(fieldName, (Vector2) obj);
            else if (type == typeof(Color))
                obj = (object) EditorGUILayout.ColorField(fieldName, (Color) obj);
            else if (type.IsObject())
                obj = (object) EditorGUILayout.ObjectField(fieldName, (UnityEngine.Object) obj, type, true);
            else if (childs.Count > 0) {
                EditorGUILayout.BeginVertical("box");

                GUIStyle richStyle = new GUIStyle(GUI.skin.label);
                richStyle.richText = true;
                EditorGUILayout.LabelField(fieldName, richStyle);

                foreach (var c in childs) {
                    var r = c.OnDrawParam();
                    c.fi.SetValue(obj, r);
                }
                EditorGUILayout.EndVertical();
            }

            return obj;
        }
    }

    public class ChildParam : Parameter {

        public FieldInfo fi;
        public ChildParam(Type type, string typeName, object obj, string name, FieldInfo info) : base(type, typeName, obj, name) {
            fi = info;
        }
    }

}