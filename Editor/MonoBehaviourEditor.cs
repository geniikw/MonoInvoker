using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimpleInvoker {
    [CustomEditor (typeof (MonoBehaviour), true)]
    public class MonoBehaviourEditor : Editor {
        [SerializeField]
        public List<MethodTesterInfo> mtInfoList = null;
        private List<MethodInfo> m_methodList = null;

        bool isRepeat = false;
        private void OnEnable () {
            m_methodList = ReflectionUtil.ExtractTestMethod(target.GetType());
            if(mtInfoList == null){
                mtInfoList = m_methodList.Select(m=>new MethodTesterInfo(m)).ToList();
            }
        }

        public override void OnInspectorGUI () {
            base.OnInspectorGUI ();

            for(int n = 0 ; n < mtInfoList.Count ; n++){

                var mi = m_methodList[n];
                var paramObject = mtInfoList[n];
                if(paramObject.isCoroutine && !Application.isPlaying)
                    continue;

                EditorGUILayout.BeginVertical ("box");

                GUIStyle richStyle = new GUIStyle (GUI.skin.label);
                richStyle.richText = true;
                EditorGUILayout.LabelField (paramObject.ViewString, richStyle);
                foreach (var p in paramObject.list) {
                    p.OnDrawParam();
                }

                if (GUILayout.Button ("Execute")) {
                    if(paramObject.isCoroutine)
                    {
                        ((MonoBehaviour) target).StartCoroutine((IEnumerator)mi.Invoke (target, paramObject.list.Select (p => p.obj).ToArray ()));
                    }
                    mi.Invoke (target, paramObject.list.Select (p => p.obj).ToArray ());
                }

                EditorGUILayout.EndVertical ();
            }
        }
    }
}