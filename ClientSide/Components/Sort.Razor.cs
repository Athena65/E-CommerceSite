using Microsoft.AspNetCore.Components;

namespace ClientSide.Components
{
    public partial class Sort
    {
        [Parameter]
        public EventCallback<string> OnSort { get; set; }
        private async Task ApplySort(ChangeEventArgs eventArgs)
        {
            if (eventArgs.Value.ToString() == "-1")
                return;
            await OnSort.InvokeAsync(eventArgs.Value.ToString());

        }
    }
}
