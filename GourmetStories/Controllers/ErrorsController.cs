using Microsoft.AspNetCore.Mvc;

namespace GourmetStories.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/errors")]
    public IActionResult Error()
    {
        return Problem();
    }

}