using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MoonAuSosiGi.Net.Editor
{
	///<summary>
	///	싱글톤을 생성할 수 있는 에디터 윈도우 
	///</summary>
	public class MoonAuSosiGiNetCreateSingletoneWindow : MoonAuSosiGiNetCreateScriptWindow
    {

        #region Variables ---------------------------------------------------------------

		#endregion ----------------------------------------------------------------------

		#region Property ----------------------------------------------------------------

		#endregion ----------------------------------------------------------------------

		#region Method ------------------------------------------------------------------

        /// <summary>
        /// 에디터 그리기
        /// </summary>
        protected override void OnGUI()
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("Namespace Name");
                m_namespaceName = GUILayout.TextField(m_namespaceName);
                GUILayout.Label("Class Name");
                m_className = GUILayout.TextField(m_className);
                GUILayout.Label("Class Comment");
                m_classComment = GUILayout.TextField(m_classComment);
                
                m_unityDefaultMethodCreate = GUILayout.Toggle(m_unityDefaultMethodCreate, "Create Default Method :: Awake / Start / Update");
                if (GUILayout.Button("Create Singletone Script"))
                    CreateScriptButton();

            }
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 스크립트 생성
        /// </summary>
        protected override void CreateScriptButton()
        {
            if (string.IsNullOrEmpty(m_className))
            {
                EditorUtility.DisplayDialog("Error!", "Class Name is NULL", "OK");
                return;
            }

            var selectObject = Selection.activeObject;
            string selectPath = null;
            // 프로젝트를 선택중이다 
            if (selectObject != null)
            {
                // 비어있을 경우 ( Inspector 선택 ) 가 있을 수 있다.
                selectPath = AssetDatabase.GetAssetPath(selectObject.GetInstanceID());
            }

            // 선택된 패스가 없으면 기본 패스
            if (string.IsNullOrEmpty(selectPath))
            {
                selectPath = "Assets";
            }

            // 파일 경로일 경우 쪼갬
            if (File.Exists(selectPath))
            {
                var token = selectPath.Split('/');
                System.Text.StringBuilder path = new System.Text.StringBuilder();

                for (int i = 0; i < token.Length - 1; i++)
                {
                    path.Append(token[i]);
                    path.Append("/");
                }
                selectPath = path.ToString();
            }

            // 중복시 
            if(File.Exists(selectPath + "/" + m_className + ".cs"))
            {
                if(EditorUtility.DisplayDialog("Warning","이미 같은 이름의 스크립트가 있습니다.\n무시하고 새로 만드시겠습니까?","OK","CANCEL") == false)
                {
                    return;
                }
            }

            System.Text.StringBuilder scriptCode = new System.Text.StringBuilder();
            scriptCode.Append("using System;\nusing System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;using MoonAuSosiGi.Net.Base;\n\n");

            // 네임스페이스가 있을 때만 들어감
            if (string.IsNullOrEmpty(m_namespaceName) == false)
            {
                scriptCode.Append("namespace ");
                scriptCode.Append(m_namespaceName);
                scriptCode.Append("\n{\n");
            }

            string namespaceTab = (string.IsNullOrEmpty(m_namespaceName) == false) ? "\t" : "";

            // 주석이 있을 때만 들어감
            if (string.IsNullOrEmpty(m_classComment) == false)
            {
                // -- 주석 선언부 --------------------
                scriptCode.Append(namespaceTab);
                scriptCode.Append("///<summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("///");
                scriptCode.Append(namespaceTab);
                scriptCode.Append(m_classComment);
                scriptCode.Append("\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("///</summary>\n");
                // -----------------------------------
            }

            // -- 클래스 선언부 ------------------------------------------------------------------------------------
            scriptCode.Append(namespaceTab);
            scriptCode.Append("public class ");
            scriptCode.Append(m_className);

            scriptCode.Append(" : ");
           
            scriptCode.Append("Singletone<");
            scriptCode.Append(m_className);
            scriptCode.Append(">\n");

            scriptCode.Append(namespaceTab);
            scriptCode.Append("{\n\n");
            // -- regin 선언 (변수) --------------------------------------------------------------------------------
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#region Variables ---------------------------------------------------------------\n\n");
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#endregion ----------------------------------------------------------------------\n\n");

            // -- regin 선언 (프로퍼티) --------------------------------------------------------------------------------
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#region Property ----------------------------------------------------------------\n\n");
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#endregion ----------------------------------------------------------------------\n\n");

            // -- region 선언 (Unity Method) -----------------------------------------------------------------------
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#region Unity Method ------------------------------------------------------------\n\n");
            if (m_unityDefaultMethodCreate == false)
            {
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t#endregion ----------------------------------------------------------------------\n\n");
            }
            
            // MonoBehaviour 기본 메소드 생성 유무
            if (m_unityDefaultMethodCreate == true)
            {
                // -- Unity Method 기본 선언 Awake / Start / Update
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///<summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///Awake : 스크립트 인스턴스가 로딩될 때 호출\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///</summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\tvoid Awake()\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t{\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t}\n");

                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///<summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///Start : Update가 처음 호출되기 바로 전에 호출\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///</summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\tvoid Start()\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t{\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t}\n");

                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///<summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///Update : 매 프레임마다 호출되는 함수\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t///</summary>\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\tvoid Update()\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t{\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t}\n");
                scriptCode.Append(namespaceTab);
                scriptCode.Append("\t#endregion ----------------------------------------------------------------------\n\n");
            }
            // -- regin 선언 (메소드) --------------------------------------------------------------------------------
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#region Method ------------------------------------------------------------------\n\n");
            scriptCode.Append(namespaceTab);
            scriptCode.Append("\t#endregion ----------------------------------------------------------------------\n\n");

            // -- 클래스 내부 끝 -------------------------------------------------------------------------------------
            scriptCode.Append(namespaceTab);
            scriptCode.Append("}\n");

            // 네임스페이스가 있을 때만 들어감
            if (string.IsNullOrEmpty(m_namespaceName) == false)
                scriptCode.Append("}");
            // -------------------------------------------------------------------------------------------------------
            CSFileCreate(selectPath + "/" + m_className + ".cs", scriptCode.ToString());
            TempFileSave();
        }
        #endregion ----------------------------------------------------------------------

    }
}
