using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
namespace PaperKiteStudio.Dangers
{
    public class JournalManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _titleText;
        [SerializeField]
        private TMP_Text _pageNumberText;
        [SerializeField]
        private TMP_Text[] _updateTexts;
        [SerializeField]
        TMP_Text _instructionText;
        [SerializeField]
        private Initializer _initializer;
        [SerializeField]
        GamePhaseManager _gamePhaseManager;

        [SerializeField]
        private Page[] allPages;
        [SerializeField]
        private Page _currentPage;
        [SerializeField]
        private int _currentPageNumber;

        [SerializeField]
        private GameObject _pageSelectUI;

        private void Start()
        {
            _pageSelectUI.SetActive(false);
            _currentPageNumber = 0;
            //set page to page 1 every time. 
            _currentPage = allPages[_currentPageNumber];
          //  PopulatePageData(_currentPage);
        }

        public void TurnOnJournal()
        {
            PopulatePageData(_currentPage);
        }

        public void TurnPage(int forwardBack)
        {
            ///check if back or forward
            if (forwardBack == 1)
            {
                _currentPageNumber++;
                if (_currentPageNumber >= allPages.Length) 
                {
                    _currentPageNumber = allPages.Length -1;
                }
            }
            else if (forwardBack == -1)
            {
                _currentPageNumber--;
                if(_currentPageNumber <= 0)
                {
                    _currentPageNumber = 0;
                }
            }

            _currentPage = allPages[_currentPageNumber];
            PopulatePageData(_currentPage);
        }
        private void PopulatePageData(Page newPage)
        {
            if (_gamePhaseManager.GetGamePhase() == 0)
            {
                _titleText.text = "???";
                _pageNumberText.text = "???";
                for (int i = 0; i < _updateTexts.Length; i++)
                {
                    _updateTexts[i].text = "???";
                }
                return; // Skip the rest of the method
            }

            _titleText.text = _initializer.GetText(_currentPage.titleKey);
            _pageNumberText.text = _currentPage.pageNumber.ToString();            //check if the update text has actually been enabled (if that phase step was complete or not.) 
      
            if (_currentPage.associatedGamePhase < _gamePhaseManager.GetGamePhase()) // if the associated game phase value is less than the current game phase enable all the entries because they wouldnt be there otherwise. 
            {
                for (int i = 0; i < _updateTexts.Length; i++)
                {
                    _updateTexts[i].text = _initializer.GetText(newPage.updateTextKeys[i]);
                }
            }
            else if (_currentPage.associatedGamePhase == _gamePhaseManager.GetGamePhase())
            {
                int currentPhaseStep = _gamePhaseManager.GetPhaseStep(); // check the phase step

                for (int i = 0; i < _updateTexts.Length; i++)
                {
                    if (i < currentPhaseStep -1) 
                    {
                        _updateTexts[i].text = _initializer.GetText(newPage.updateTextKeys[i]);
                        //pdateTexts[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _updateTexts[i].text = "???";
                       //updateTexts[i].gameObject.SetActive(false); // Optional: hide incomplete entries
                    }
                }
            }

            //grab the data from the currentpage. currentPage should = the page number..
        }
        public void SelectPage(int pageNumber)
        {
            _currentPageNumber = pageNumber;
            _currentPage = allPages[_currentPageNumber -1];
            PopulatePageData(_currentPage);
            _pageSelectUI.SetActive(false);
        }
    }
}