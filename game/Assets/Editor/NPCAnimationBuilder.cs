//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEditor.Animations;
//using UnityEngine;
//
//public class NPCAnimationBuilder : EditorWindow {
//
//	public GameObject target;
//	public Sprite idle_up_sprite;
//
//	[MenuItem("Window/NPC Animation Builder")]
//	static void OpenWindow() {
//		GetWindow<NPCAnimationBuilder> ();
//	}
//
//	void OnGUI() {
//		target = EditorGUILayout.ObjectField("Target Object", target, typeof(GameObject), true) as GameObject;
//		idle_up_sprite = EditorGUILayout.ObjectField("Idle Up Sprite", idle_up_sprite, typeof(Sprite), false) as Sprite;
//
//		if(GUILayout.Button ("Create")) {
//			Create ();
//		}
//	}
//
//	void Create() {
//
//		AnimationClip idle_up = new AnimationClip ();
//
//		EditorCurveBinding curveBinding = new EditorCurveBinding ();
//		curveBinding.type = typeof(SpriteRenderer);
//		curveBinding.path = "";
//		curveBinding.propertyName = "m_Sprite";
//
//		// An array to hold the object keyframes
//		ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[10];
//		
//		for (int i = 0; i < 1; i++)
//		{
//			keyFrames[i] = new ObjectReferenceKeyframe();
//			// set the time
//			keyFrames[i].time = i*2;
//			// set reference for the sprite you want
//			keyFrames[i].value = idle_up_sprite;
//			
//		}
//		
//		AnimationUtility.SetObjectReferenceCurve(idle_up, curveBinding, keyFrames);
//
//		AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath ("Assets/Animations/" + target.name + ".controller");
//
//		controller.AddParameter ("input_x", AnimatorControllerParameterType.Float);
//		controller.AddParameter ("input_y", AnimatorControllerParameterType.Float);
//		controller.AddParameter ("is_walking", AnimatorControllerParameterType.Bool);
//
//		AnimatorState idleState = controller.layers [0].stateMachine.AddState ("Idle");
//		idleState.motion = idle_up;
//
//		BlendTree blendTree;
//		AnimatorState walkingState = controller.CreateBlendTreeInController ("Walking", out blendTree);
//		walkingState.motion = idle_up;
//
//		blendTree.blendType = BlendTreeType.FreeformCartesian2D;
//		blendTree.blendParameter = "input_x";
//		blendTree.blendParameterY = "input_y";
//		blendTree.AddChild (idle_up);
//		blendTree.AddChild (idle_up);
//
//		AnimatorStateTransition leaveIdle = idleState.AddTransition (walkingState);
//		AnimatorStateTransition leaveWalking = walkingState.AddTransition (idleState);
//
//		leaveIdle.AddCondition (AnimatorConditionMode.If, 0, "is_walking");
//
//		target.GetComponent<Animator> ().runtimeAnimatorController = controller;
//
//	}
//}
//#endif
