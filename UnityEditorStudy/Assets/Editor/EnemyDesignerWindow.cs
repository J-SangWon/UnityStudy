using UnityEngine;
using UnityEditor;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class EnemyDesignerWindow : EditorWindow
{
    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();

    }

    int countX = 0;
    int countZ = 0;
    //라벨사용해보기
    private void OnGUI()
    {
        GUILayout.Label("적에 관한 내용을 만드는 툴", EditorStyles.boldLabel);
        GUILayout.Label("이건그냥 라벨");
        EditorGUILayout.LabelField("라벨필드:", "보스");

        GUILayout.Label("큐브만들기", EditorStyles.boldLabel);

        if (GUILayout.Button("Create CubeX"))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "맵X" + countX;
            cube.transform.position = new Vector3(countX++, 0, countZ);

        }
        if (GUILayout.Button("Create CubeZ"))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "맵Z" + countZ;
            cube.transform.position = new Vector3(countX, 0, countZ++);

        }
    }
}