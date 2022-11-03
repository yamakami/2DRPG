using UnityEngine.UIElements;
using UnityEngine;

interface ISelectButton
{
    Button[] SelectButtons { get; set; }
    void ShowButton(int index, bool show) => SelectButtons[index].style.visibility = (show) ? Visibility.Visible : Visibility.Hidden;
    void InitialButtons(VisualElement parentElement, string elementName)
    {
        var index = 0;
        parentElement.Query<Button>().Name(elementName).ForEach( bt => {
            SelectButtons[index] = bt;
            index++;
        });
    }

    void BindButtonEvent();

    void ClickAndHover(int index)
    {
        SelectButtons[index].RegisterCallback<ClickEvent, int>(ClickAction, index);
        SelectButtons[index].RegisterCallback<ClickEvent>( ev => ClickSound() );
        SelectButtons[index].RegisterCallback<MouseEnterEvent>( ev => HoverSound() );
    }

    void ClickAction(ClickEvent ev, int index);

    void UnregisterCallback()
    {
        for(var i=0; i < SelectButtons.Length; i++)
        {
            SelectButtons[i].UnregisterCallback<ClickEvent, int>(ClickAction);
            SelectButtons[i].RegisterCallback<ClickEvent>( ev => ClickSound() );
            SelectButtons[i].UnregisterCallback<MouseEnterEvent>( ev => HoverSound() );
        }
    }

    void ClickSound() => SystemManager.SoundManager.PlayButtonClick();
    void HoverSound() => SystemManager.SoundManager.PlayButtonHover();
}
