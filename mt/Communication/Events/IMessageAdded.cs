namespace Mt.Communication.Events
{
  public interface IMessageAdded
  {
    int Id { get; }
    string Text { get; }
    string Author { get; }
  }
}
