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

public class ChangeAnimation : MonoBehaviour
{

    // Use this for initialization


    public Text Text;

    public float MaxSpeed = 5.0f;
    public float DeltaSpeed = 0.2f;
    public float TransitionTime = 1.0f;

    private GameObject player;
    private ClickMove clickMove;

    


    [HideInInspector]
    public float AnimatorSpeed = 1.0f;

    private UnityAnimator animator;
    private Spriter spriter;
    private string direction = "Left";
    private SpriterAnimation anim;

    private float Timer = .05f;


    void Start()
    {
        player = this.gameObject;
        clickMove = this.GetComponent<ClickMove>();
    }

    void Update()
    {

        // var  renderers = new SpriteRenderer[GetComponent<SpriterDotNetBehaviour>().ChildData.Sprites.Length];
        //for (int i = 0; i < GetComponent<SpriterDotNetBehaviour>().ChildData.Sprites.Length; ++i)
        //{
        //    renderers[i] = GetComponent<SpriterDotNetBehaviour>().ChildData.Sprites[i].GetComponent<SpriteRenderer>();
        //}

        //renderers[0].sortingOrder = 3;
    
        if (animator == null)
        {
            animator = GetComponent<SpriterDotNetBehaviour>().Animator;
            spriter = animator.Entity.Spriter;
            anim = new SpriterAnimation();
        }

        if(Input.GetKeyDown(KeyCode.F2))

        {
            animator = GetComponent<SpriterDotNetBehaviour>().Animator;
          
          anim = animator.Entity.Spriter.Entities[4].Animations[0];
            //animator.Play(x);
            animator.Play(anim);

        }

        if (Input.GetKeyDown(KeyCode.F3))

        {
            animator = GetComponent<SpriterDotNetBehaviour>().Animator;
            anim= animator.Entity.Spriter.Entities[4].Animations[1];
            animator.Play(anim);
            // animator.Play(x);
        }

        if (clickMove.XMag < 0 && (Math.Abs(clickMove.XMag) > Math.Abs(clickMove.YMag)))
        {
            if(direction != "Left")
            {
                Timer = 0;
            }

            anim = spriter.Entities[(int)AnimationDirection.Left].Animations[(int)Animation.Walk];
            direction = "Left";
        }
        else if(clickMove.XMag > 0 && (Math.Abs(clickMove.XMag) > Math.Abs(clickMove.YMag)))
        {
            if(direction != "Right")
            {
                Timer = 0;
            }

            anim = spriter.Entities[(int)AnimationDirection.Right].Animations[(int)Animation.Walk];
            direction = "Right";
        }
        //else if(clickMove.YMag < 0 && (Math.Abs(clickMove.YMag) > Math.Abs(clickMove.XMag)))
        //{
        //    if (direction != "Down")
        //    {
        //        Timer = 0;
        //    }

        //    anim = spriter.Entities[(int)AnimationDirection.Down].Animations[(int)Animation.Idle];
        //    direction = "Down";
        //}
        else
        {
            switch (direction)
            {
                case "Left":
                    anim = spriter.Entities[(int)AnimationDirection.Left].Animations[(int)Animation.Idle];
                    break;
                case "Right":
                    anim = spriter.Entities[(int)AnimationDirection.Right].Animations[(int)Animation.Idle];
                    break;
                case "Up":
                    break;
                case "Down":
                    anim = spriter.Entities[(int)AnimationDirection.Down].Animations[(int)Animation.Idle];
                    break;
            }
        }



        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            animator.Play(anim);
            Timer = anim.Length / 1000;
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

    private void ChangeAnimationSpeed(float delta)
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

    private enum Animation
    {
        Walk = 0,
        Idle = 1,
        Attack = 2,
        Hurt = 3
    }
}
