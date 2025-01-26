using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // For Pointer Event Handling
using Oculus.Interaction;
using Oculus.Haptics; // Use Oculus.Haptics namespace

[RequireComponent(typeof(AudioSource))]
public class InteractionHandler : MonoBehaviour, IPointerEnterHandler
{
    [Header("Interaction Settings")]
    public HapticClip hoverHapticClip; // Assign a HapticClip for hover in the Unity Editor
    public HapticClip clickHapticClip; // Assign a HapticClip for click in the Unity Editor
    public AudioClip hoverAudioClip;  // Assign an AudioClip for hover in the Unity Editor
    public AudioClip clickAudioClip;  // Assign an AudioClip for click in the Unity Editor

    private HapticClipPlayer hoverHapticPlayer;
    private HapticClipPlayer clickHapticPlayer;
    private AudioSource audioSource;

    void Start()
    {
        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();

        // Initialize HapticClipPlayers
        if (hoverHapticClip)
        {
            hoverHapticPlayer = new HapticClipPlayer(hoverHapticClip);
        }

        if (clickHapticClip)
        {
            clickHapticPlayer = new HapticClipPlayer(clickHapticClip);
        }
    }

    public void OnTriggerInteraction()
    {
        PlayHoverEffects();
    }

    public void OnButtonClick()
    {
        PlayClickEffects();
    }

    private void PlayHoverEffects()
    {
        // Play hover haptics
        if (hoverHapticPlayer != null)
        {
            hoverHapticPlayer.Play(Controller.Right);
        }

        // Play hover audio
        if (audioSource && hoverAudioClip)
        {
            audioSource.PlayOneShot(hoverAudioClip);
        }
    }

    private void PlayClickEffects()
    {
        // Play click haptics
        if (clickHapticPlayer != null)
        {
            clickHapticPlayer.Play(Controller.Right);
        }

        // Play click audio
        if (audioSource && clickAudioClip)
        {
            audioSource.PlayOneShot(clickAudioClip);
        }
    }

    // Handle hover events for UI buttons
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverEffects();
    }

    // Trigger interaction for 3D objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust tag to your use case
        {
            OnTriggerInteraction();
        }
    }

    // Button setup in UI
    public void AssignToButton(Button button)
    {
        button.onClick.AddListener(OnButtonClick);
    }
}
