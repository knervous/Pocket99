using UnityEngine;
using SpriterDotNetUnity;
using SpriterDotNet;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Assets.Scripts.Data_Models;

public class Animation : MonoBehaviour
{

    [HideInInspector]
    public float AnimatorSpeed = 1.0f;
    [HideInInspector]
    public float MaxSpeed = 5.0f;
    [HideInInspector]
    public float DeltaSpeed = 0.2f;
    [HideInInspector]
    public float TransitionTime = 1.0f;

    private ClickMove clickMove;
    private bool idleOn = true;

    private UnityAnimator animator;
    private Spriter spriter;
    public string charModel = "Barbarian";
    public string direction = "Down";
    public string animateString = "Idle Down";

    public GameObject Player;

    private float Timer = .05f;

    void Start()
    {
        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        while (animator == null || Player == null)
        {
            yield return new WaitForSeconds(waitTime);
            animator = GetComponent<SpriterDotNetBehaviour>().Animator;
            clickMove = Player.GetComponent<ClickMove>();
            spriter = animator.Entity.Spriter;

            EquipmentTextureSwapper.Instance.SetCharacterModel(charModel, animator, new Inventory());
        }
    }

void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
        {
            EquipmentTextureSwapper.Instance.SetCharacterModel("Barbarian", animator, new Inventory());

            var g = EquipmentTextureSwapper.Instance;
            var x = 0;
        }

        if (clickMove != null)
        {
            if (clickMove.XMag < 0 && (Math.Abs(clickMove.XMag) > Math.Abs(clickMove.YMag)))
            {

                if (idleOn || direction != "Left")
                {
                    Timer = 0;
                }
                idleOn = false;
                direction = "Left";
                animateString = "Walk Left";
            }
            else if (clickMove.XMag > 0 && (Math.Abs(clickMove.XMag) > Math.Abs(clickMove.YMag)))
            {
                if (idleOn || direction != "Right")
                {
                    Timer = 0;
                }
                idleOn = false;
                direction = "Right";
                animateString = "Walk Right";
            }
            else if (clickMove.YMag < 0 && (Math.Abs(clickMove.YMag) > Math.Abs(clickMove.XMag)))
            {
                if (idleOn || direction != "Down")
                {
                    Timer = 0;
                }
                idleOn = false;
                direction = "Down";
                animateString = "Walk Down";
            }
            else if (clickMove.YMag > 0 && (Math.Abs(clickMove.YMag) > Math.Abs(clickMove.XMag)))
            {
                if (idleOn || direction != "Up")
                {
                    Timer = 0;
                }
                idleOn = false;
                direction = "Up";
                animateString = "Walk Up";
            }
            else if (!clickMove.isMoving)
            {
                if (!idleOn)
                {
                    Timer = 0;
                    idleOn = true;
                }
                switch (direction)
                {
                    case "Left":
                        animateString = "Idle Left";
                        break;
                    case "Right":
                        animateString = "Idle Right";
                        break;
                    case "Up":
                        animateString = "Idle Up";
                        break;
                    case "Down":
                        animateString = "Idle Down";
                        break;
                }
            }
        }

        if (animator != null)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                animator.Play(animateString);
                Timer = animator.CurrentAnimation.Length / 1000;
            }
        }
    }


    private void SwitchAnimation(int offset)
    {
        animator.Play(GetAnimation(animator, offset));
    }

    private void Transition(int offset)
    {
        animator.Transition(GetAnimation(animator, offset), TransitionTime * 1000.0f);
    }

    private void AnimationSpeed(float delta)
    {
        var speed = animator.Speed + delta;
        speed = Math.Abs(speed) < MaxSpeed ? speed : MaxSpeed * Math.Sign(speed);
        AnimatorSpeed = (float)Math.Round(speed, 1, MidpointRounding.AwayFromZero);
    }

    private void ReverseAnimation()
    {
        AnimatorSpeed *= -1;
    }

    private void PushCharacterMap()
    {
        SpriterCharacterMap[] maps = animator.Entity.CharacterMaps;
        if (maps == null || maps.Length == 0) return;
        SpriterCharacterMap charMap = animator.SpriteProvider.CharacterMap;
        if (charMap == null) charMap = maps[0];
        else
        {
            int index = charMap.Id + 1;
            if (index >= maps.Length) charMap = null;
            else charMap = maps[index];
        }

        if (charMap != null) animator.SpriteProvider.PushCharMap(charMap);
    }

    private string GetVarValues()
    {
        StringBuilder sb = new StringBuilder();

        FrameData frameData = animator.FrameData;

        foreach (var entry in frameData.AnimationVars)
        {
            object value = GetValue(entry.Value);
            sb.Append(entry.Key).Append(" = ").AppendLine(value.ToString());
        }
        foreach (var objectEntry in frameData.ObjectVars)
        {
            foreach (var varEntry in objectEntry.Value)
            {
                object value = GetValue(varEntry.Value);
                sb.Append(objectEntry.Key).Append(".").Append(varEntry.Key).Append(" = ").AppendLine((value ?? string.Empty).ToString());
            }
        }

        return sb.ToString();
    }

    private object GetValue(SpriterVarValue varValue)
    {
        object value;
        switch (varValue.Type)
        {
            case SpriterVarType.Float:
                value = varValue.FloatValue;
                break;
            case SpriterVarType.Int:
                value = varValue.IntValue;
                break;
            default:
                value = varValue.StringValue;
                break;
        }
        return value;
    }

    private string GetTagValues()
    {
        FrameData fd = animator.FrameData;

        StringBuilder sb = new StringBuilder();
        foreach (string tag in fd.AnimationTags) sb.AppendLine(tag);
        foreach (var objectEntry in fd.ObjectTags)
        {
            foreach (string tag in objectEntry.Value) sb.Append(objectEntry.Key).Append(".").AppendLine(tag);
        }

        return sb.ToString();
    }

    private static bool GetAxisDownPositive(string axisName)
    {
        return Input.GetButtonDown(axisName) && Input.GetAxis(axisName) > 0;
    }

    private static bool GetAxisDownNegative(string axisName)
    {
        return Input.GetButtonDown(axisName) && Input.GetAxis(axisName) < 0;
    }

    private static string GetAnimation(UnityAnimator animator, int offset)
    {
        List<string> animations = animator.GetAnimations().ToList();
        int index = animations.IndexOf(animator.CurrentAnimation.Name);
        index += offset;
        if (index >= animations.Count) index = 0;
        if (index < 0) index = animations.Count - 1;
        return animations[index];
    }


    private enum AnimationDirection
    {
        Base = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    private enum AnimationEnum
    {
        Walk = 0,
        Idle = 1,
        Attack = 2,
        Hurt = 3
    }
}
