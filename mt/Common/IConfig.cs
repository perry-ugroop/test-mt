namespace Common
{
    public interface IConfig
    {
       string Get(string keyName); 
       int GetInt(string keyName); 
    }
}