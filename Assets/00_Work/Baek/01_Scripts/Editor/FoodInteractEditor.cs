#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(FoodInteract))]
public class FoodInteractEditor : Editor
{
    const string info = "\n�޽��� �������ָ� �ݶ��̴��� ������� �˾Ƽ� �ٲ�����.\n����� ������ �������� �׳� ���Ͼƴϴ�.(�̰� ������)\n";
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(message: info, MessageType.Info);
        base.OnInspectorGUI();
    }
}
#endif
