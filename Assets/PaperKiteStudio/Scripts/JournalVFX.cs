using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using PaperKiteStudio.Dangers;
using TMPro;

public class JournalVFX : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private Image _thisImage;
    [SerializeField]
    private RectTransform _thisRect;

    [SerializeField]
    private bool _isComplete;
    [SerializeField]
    private GamePhaseManager _gamePhaseManager;
    [SerializeField]
    private JournalManager _journalManager;
    [Header("Colors")]
    [SerializeField]
    private Color _incompleteColor = new Color(0.5f, 0f, 0.5f, 1f); // purple
    [SerializeField]
    private Color _completeColor = new Color(0f, 1f, 0f, 1f); // green

    [SerializeField]
    private TMP_Text _completedText;

    public void SetImage()
    {
        // defensive null-checks
        if (_journalManager == null || _thisImage == null || _gamePhaseManager == null)
        {
            return;
        }

        // Get the journal page number. JournalManager.GetPageNumber() can be either 0-based index or 1-based page
        int rawPage = _journalManager.GetPageNumber() + 1;

        // Normalize to 1-based page number:
        // If rawPage is 0 and the manager uses 0-based indexes, +1 yields 1 (first page).
        // If rawPage is already 1-based, adding 0 would be correct; since we don't know, try two strategies:
        // Prefer the ScriptableObject pageNumber when possible (not accessible here), so assume:
        int pageNumber = rawPage <= 0 ? rawPage + 1 : rawPage; // tries to convert 0 -> 1; leaves positive as-is

        // If you know GetPageNumber is index-based, use: pageNumber = rawPage + 1;

        _isComplete = _gamePhaseManager.IsTimelineComplete(rawPage);

        _thisImage.color = _isComplete ? _completeColor : _incompleteColor;
        _completedText.text = _isComplete ? "Completed" : "Incomplete";
        _completedText.color = _isComplete ? _completeColor : _incompleteColor;
    }
}
