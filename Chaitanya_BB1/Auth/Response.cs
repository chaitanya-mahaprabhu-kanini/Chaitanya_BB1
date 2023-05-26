public class Response
{
	//Response contains the status (100, 200, 300, 400, 500)
	//100 range information.
	//200 range success.
	//300 range redirection.
	//400 range user error.
	//500 range server error.
	public string? Status { get; set; }
	public string? Message { get; set; }
}