// Name this script "EffectRadiusEditor"
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAI))]
public class EnemyAIEditor : Editor
{
    public void OnSceneGUI()
    {
        EnemyAI t = (target as EnemyAI);

        EditorGUI.BeginChangeCheck();
        Handles.color = Color.yellow;
        float detectionRadius = Handles.RadiusHandle(Quaternion.identity, t.transform.position, t.DetectionRadius);
        Handles.color = Color.red;
        float combatRange = Handles.RadiusHandle(Quaternion.identity, t.transform.position, t.CombatRange);
        Handles.color = Color.blue;
        Vector3[] waypoints = new Vector3[t.waypoints.Length];
        for (int i = 0; i < t.waypoints.Length; i++)
        {
            waypoints[i] = Handles.FreeMoveHandle(t.waypoints[i], Quaternion.identity, 0.1f, Vector3.one, Handles.CircleHandleCap);
            if(i > 0 && i < t.waypoints.Length && t.waypoints.Length > 1)
            {
                Handles.DrawLine(waypoints[i], waypoints[i - 1]);
            }
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed AI Settings");
            t.DetectionRadius = detectionRadius;
            t.CombatRange = combatRange;

            for (int i = 0; i < t.waypoints.Length; i++)
            {
                t.waypoints[i] = waypoints[i];
            }
        }
    }
}