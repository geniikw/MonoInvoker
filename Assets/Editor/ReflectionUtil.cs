using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimpleInvoker {
    public static class ReflectionUtil {
        public static List<MethodInfo> ExtractTestMethod (Type type) {
            var list = new List<MethodInfo> ();

            foreach (var mi in type.GetMethods (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                var at = mi.GetCustomAttributes (false);
                var isTestMethod = false;
                foreach (var a in at)
                    if (a is TestMethodAttribute){
                        if(!Application.isPlaying && !((TestMethodAttribute)a).isEnableNotPlaying)
                            continue;

                        isTestMethod = true;
                    }

                if (!isTestMethod)
                    continue;

                list.Add (mi);
            }
            return list;
        }

        public static bool IsObject(this Type type){
            if(type == typeof(UnityEngine.Object))
                return true;
            else if(type.BaseType == null)
                return false;
            else if(IsObject(type.BaseType))
                return true;

            return false;
        }
    }

}