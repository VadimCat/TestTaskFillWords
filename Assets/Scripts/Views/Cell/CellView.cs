using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Views.Cell
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image background;
        [SerializeField] private Canvas sortingCanvas;
        

        public async Task SetCellLetterAsync(char letter)
        {
            text.text = letter.ToString();
            sortingCanvas.overrideSorting = true;
            var sequence=  DOTween.Sequence()
                .Insert(0, transform.DOPunchScale(Vector3.one * 1.05f, .5f, 0))
                .Insert(0, text.DOColor(Color.black, .5f))
                .SetLink(gameObject);
            sequence.Play();
            await sequence.AsyncWaitForCompletion();

            sortingCanvas.overrideSorting = false;

            sortingCanvas.sortingOrder = 1000;
        }

        public class Factory : PlaceholderFactory<CellView>
        {
            
        }
    }
}
