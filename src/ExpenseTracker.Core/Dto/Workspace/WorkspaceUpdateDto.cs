namespace ExpenseTracker.Core.Dto.Workspace
{
    public class WorkspaceUpdateDto : WorkspaceCreateDto
    {
        public int WorkspaceId { get; set; }
        public string Description { get; set; }
    }
}