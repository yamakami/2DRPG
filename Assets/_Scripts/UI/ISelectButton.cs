using UnityEngine.UIElements;
using UnityEngine;

public interface ISelectButton
{
    Button[] SelectButtons { get; set; }
    void ShowButton(int index, bool show) => SelectButtons[index].style.visibility = (show) ? Visibility.Visible : Visibility.Hidden;
    void InitialButtons(VisualElement parentElement, string elementName)
    {
        var index = 0;
        parentElement.Query<Button>().Name(elementName).ForEach( bt => {
            SelectButtons[index] = bt;
            SelectButtons[index].RegisterCallback<ClickEvent>( ev => ClickSound() );
            SelectButtons[index].RegisterCallback<MouseEnterEvent>( ev => HoverSound() );
            index++;
        });
    }

    void BindButtonEvent();

    void ClickAndHover(int[] indexes)
    {
        var buttonIndex = indexes[0];
        var itemIndex   = indexes[1];
        SelectButtons[buttonIndex].UnregisterCallback<ClickEvent, int>(ClickAction);
        SelectButtons[buttonIndex].UnregisterCallback<MouseEnterEvent, int>(HoverAction);

        SelectButtons[buttonIndex].RegisterCallback<ClickEvent, int>(ClickAction, itemIndex);
        SelectButtons[buttonIndex].RegisterCallback<MouseEnterEvent, int>(HoverAction, itemIndex);
    }

    void ClickAction(ClickEvent ev, int index);
    void HoverAction(MouseEnterEvent ev, int index) { return; }

    void UnregisterCallback()
    {
        for(var i=0; i < SelectButtons.Length; i++)
        {
            SelectButtons[i].UnregisterCallback<ClickEvent, int>(ClickAction);
            SelectButtons[i].UnregisterCallback<MouseEnterEvent, int>(HoverAction);
            SelectButtons[i].UnregisterCallback<ClickEvent>( ev => ClickSound() );
            SelectButtons[i].UnregisterCallback<MouseEnterEvent>( ev => HoverSound() );
        }
    }
 
    void ClickSound() => SystemManager.SoundManager.PlayButtonClick();
    void HoverSound() => SystemManager.SoundManager.PlayButtonHover();
}
