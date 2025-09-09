using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly TourBroker _tourBroker;

    public IndexModel(TourBroker tourBroker)
    {
        _tourBroker = tourBroker;
    }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Action { get; set; }

    [BindProperty]
    public string City { get; set; }

    public string? StatusMessage { get; set; }

    public void OnGet() { }

    public async Task OnPost()
    {
        if (!string.IsNullOrWhiteSpace(Action) && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email))
        {
            switch (Action)
            {
                case "book":
                    Action = "booked";
                    break;
                case "cancel":
                    Action = "cancelled";
                    break;
                default:
                    Action = "fuck";
                    break;
            }

            await _tourBroker.Send(Action, City, Name, Email);
            StatusMessage = $"Message sent: {Action.ToUpper()} {City}";
        }
        else
        {
            StatusMessage = "Missing required fields.";
        }
    }
}