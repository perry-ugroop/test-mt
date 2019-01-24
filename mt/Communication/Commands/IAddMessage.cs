namespace Mt.Communication.Commands
{
  public interface IAddMessage
  {
    int Id { get; }
    string Text { get; }
    string Author { get; }
  }
}
