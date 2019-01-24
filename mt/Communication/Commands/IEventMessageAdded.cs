namespace Mt.Communication.Commands
{
  public interface IEventMessageAdded
  {
    int Id { get; }
    string Text { get; }
    string Author { get; }
  }
}
