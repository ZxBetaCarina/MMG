using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationDirection
{
    Up,
    Down,
    Left,
    Right
}

public class AppearanceManager : MonoBehaviour
{
    public static AppearanceManager Singleton;

    Dictionary<int, Vector3> _original_poisition_dictionary = new();

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    internal void FadeIn(GameObject p_object, float p_fade_duration, AnimationDirection p_direction, Action p_post_complete_action = null)
    {
        Vector3 moveVector = Get_move_vector(p_direction, 50f);

        // Set initial alpha to 0 and move the object
        Set_alpha_recursive(p_object, 0f);
        Vector3 originalPosition = p_object.transform.position;
        p_object.transform.position = originalPosition - moveVector;

        // Create the sequence
        Sequence sequence = DOTween.Sequence();

        // Fade in and move
        sequence.Append(p_object.transform.DOMove(originalPosition, p_fade_duration));
        Fade_object_alpha_recursive(p_object, 1, p_fade_duration, sequence);

        // Invoke the completion action
        sequence.OnComplete(() => p_post_complete_action?.Invoke());

        // Play the sequence
        sequence.Play();
    }

    internal void FadeOut(GameObject p_object, float p_fade_duration, AnimationDirection p_direction, Action p_post_complete_action = null)
    {
        Vector3 moveVector = Get_move_vector(p_direction, 50f);

        _original_poisition_dictionary[p_object.GetInstanceID()] = p_object.transform.position;
        Vector3 targetPosition = p_object.transform.position + moveVector;

        // Create the sequence
        Sequence sequence = DOTween.Sequence();

        // Fade out and move
        sequence.Append(p_object.transform.DOMove(targetPosition, p_fade_duration));
        Fade_object_alpha_recursive(p_object, 0f, p_fade_duration, sequence);

        // Reset position and invoke the completion action
        sequence.OnComplete(() =>
        {
            p_object.transform.position = _original_poisition_dictionary[p_object.GetInstanceID()];
            p_post_complete_action?.Invoke();
        });

        // Play the sequence
        sequence.Play();
    }

    Vector3 Get_move_vector(AnimationDirection p_direction, float p_distance)
    {
        switch (p_direction)
        {
            case AnimationDirection.Up:
                return new Vector3(0, p_distance, 0);
            case AnimationDirection.Down:
                return new Vector3(0, -p_distance, 0);
            case AnimationDirection.Left:
                return new Vector3(-p_distance, 0, 0);
            case AnimationDirection.Right:
                return new Vector3(p_distance, 0, 0);
            default:
                return Vector3.zero;
        }
    }

    void Set_alpha_recursive(GameObject p_object, float p_alpha)
    {
        Set_alpha(p_object, p_alpha);
        foreach (Transform child in p_object.transform)
        {
            Set_alpha_recursive(child.gameObject, p_alpha);
        }
    }

    void Set_alpha(GameObject p_object, float p_target_alpha)
    {
        if (p_object.TryGetComponent(out Renderer t_renderer))
        { 
            t_renderer.material.color = new Color(t_renderer.material.color.r, t_renderer.material.color.g, t_renderer.material.color.b, p_target_alpha);
        }

        if (p_object.TryGetComponent(out Image t_image))
        {
            t_image.color = new Color(t_image.color.r, t_image.color.g, t_image.color.b, p_target_alpha);
        }

        if (p_object.TryGetComponent(out CanvasGroup t_canvas_group))
        {
            t_canvas_group.alpha = p_target_alpha;
        }
    }

    
    void Fade_object_alpha_recursive(GameObject p_object, float p_target_alpha, float p_duration, Sequence p_sequence)
    {
        Fade_object_alpha(p_object, p_target_alpha, p_duration, p_sequence);
        foreach (Transform child in p_object.transform)
        {
            Fade_object_alpha_recursive(child.gameObject, p_target_alpha, p_duration, p_sequence);
        }
    }

    void Fade_object_alpha(GameObject p_object, float p_target_alpha, float p_duration, Sequence p_sequence)
    {
        if (p_object.TryGetComponent(out Renderer t_renderer))
        {
            p_sequence.Join(t_renderer.material.DOFade(p_target_alpha, p_duration));
        }

        if (p_object.TryGetComponent(out Image t_image))
        {
            p_sequence.Join(t_image.DOFade(p_target_alpha, p_duration));
        }

        if (p_object.TryGetComponent(out CanvasGroup t_canvas_group))
        {
            p_sequence.Join(t_canvas_group.DOFade(p_target_alpha, p_duration));
        }
    }
}
