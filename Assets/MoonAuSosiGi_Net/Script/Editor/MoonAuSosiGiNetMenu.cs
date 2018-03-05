using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MoonAuSosiGi.Net.Editor
{

    /// <summary>
    /// MoonAuSosiGi.Net 의 모든 에디터를 관리하는 메뉴 에디터.
    /// </summary>
    static public class MoonAuSosiGiNetMenu
    {
        #region MoonAuSosiGiNet Menu Open -----------------------------------------------
        [MenuItem("MoonAuSosiGi.Net/Create/NormalScript")]
        static public void OpenCreateNormalScript()
        {
            EditorWindow.GetWindow<MoonAuSosiGiNetCreateScriptWindow>().Show();
        }

        [MenuItem("MoonAuSosiGi.Net/Create/SingleToneScript")]
        static public void OpenCreateSingleScript()
        {
            EditorWindow.GetWindow<MoonAuSosiGiNetCreateSingletoneWindow>().Show();
        }

        #endregion ----------------------------------------------------------------------
    }
}