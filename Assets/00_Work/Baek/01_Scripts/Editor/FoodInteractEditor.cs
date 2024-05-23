#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(FoodInteract))]
public class FoodInteractEditor : Editor
{
    const string info = "\n메쉬만 변경해주면 콜라이더랑 비쥬얼이 알아서 바꿔진다.\n노란색 오류는 무시하자 그냥 별일아니다.(이거 ㄹㅇ임)\n";
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(message: info, MessageType.Info);
        base.OnInspectorGUI();
    }
}
#endif
