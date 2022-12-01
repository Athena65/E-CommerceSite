using Microsoft.AspNetCore.Components;

namespace ClientSide.Components
{
    public  partial class Search
    {
        private Timer _timer;
        public string SearchTerm { get; set; }
        [Parameter]
        public EventCallback<string> OnSearch { get; set; }
        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();

            _timer = new Timer(OnTimerElapsed, null, 500, 0);
        }
        private void OnTimerElapsed(object sender)
        {
            OnSearch.InvokeAsync(SearchTerm);
            _timer.Dispose();

        }
    }
}
